import { PUBLIC_API_ORIGIN } from '$env/static/public';
import { LineModel } from './line';
import { TripModel } from './trip';
// import { DirectionModel } from './direction';
import { StopModel } from './stop';
import { TimeModel } from './schedule';

interface Schedule {
	id: number;
	routeStopTimes: string[];
	code: number;
}

interface NextBusStop {
	nextBusMinutes: number;
}

interface Stop {
	id: number;
	lineId: number;
	name: string;
	latitude: number;
	longitude: number;
  // TODO: rename to trips
	trip: Trip[];
	line: Line;
}

interface Trip {
	id: number;
	trip_headsign: string;
	stop_id: number;
	schedule: string[];
}

interface Line {
	type: number;
	agencyName: string;
	id: number;
	name: string;
	fullName: string;
}

interface routeBranchesWithStops {
	routeBranchStops: Schedule;
}

const getFailureMessage = (url: string) => {
	return `Failed to retrieve for URL, ${url}`;
};

const getNearestStops = async (latitude: number, longitude: number): Promise<Stop[]> => {
	const origin = PUBLIC_API_ORIGIN;
	if (!origin) {
		throw 'API_ORIGIN environment variable has not been defined!';
	}

	const url = `${origin}/nearest-stops?latitude=${latitude}&longitude=${longitude}`;
	console.log('hell');

	const response = await fetch(url, { headers: { 'Content-Type': 'application/json' } });
	if (!response.ok) {
		throw getFailureMessage(url);
	}

	const body = await response.text();
	return JSON.parse(body) as Stop[];
};

const getLines = async (latitude: number, longitude: number): Promise<LineModel[]> => {
	const stops = await getNearestStops(latitude, longitude);
	const linesHash: Record<number, LineModel> = {};

	stops.forEach((item) => {
		if (item.name == 'ALL BRANCHES') {
			return;
		}

		console.log(`Stop Id: ${item.id}`);
		let line: LineModel;
		if (typeof linesHash[item.line.id] !== 'undefined') {
			console.log('found');
			line = linesHash[item.line.id];
		} else {
			console.log('undefined');
			line = new LineModel(item.line.type, item.line.agencyName, item.line.id);
			linesHash[item.line.id] = line;
		}
		console.log(`Line Id: ${line.id}`);

		const stop = new StopModel(
			item.id,
			item.name,
			item.line.id,
			item.line.fullName,
			item.latitude,
			item.longitude
		);

    for (const trip of item.trip) {
      const tripModel = new TripModel(trip.id, trip.trip_headsign, trip.schedule);
      stop.trips.push(tripModel);
    }

		line.stops.push(stop);
	});

	const keys = Object.keys(linesHash);
	for (const item of keys) {
		console.log(`Item is ${item}`);
	}
	return keys.map((item: string) => linesHash[parseInt(item)]);
};

const getTimeTable = async (latitude: number, longitude: number): Promise<LineModel[]> => {
	const lines = await getLines(latitude, longitude);

	const now = TimeModel.now();
	for (const line of lines) {
		const schedules = await getSchedules(line.id);

		for (const stop of line.stops) {
			for (const trip of stop.trips) {
				const schedule = getSchedule(schedules, trip.id);
				let timeTable;

				switch (line.lineType) {
					case 'Subway':
						stop.timetable = getArrivals(now, schedule.routeStopTimes);

						break;
					case 'Bus':
					case 'Streetcar':
					default:
						timeTable = await getNextTimeTable(line.id, schedule.code);
						stop.timetable = timeTable.map((item) => item.nextBusMinutes);
				}
			}
		}
	}

	return lines;
};

const getArrivals = (now: number, routeStopTimes: string[]): number[] => {
	return routeStopTimes
		.map((item) => {
			const arrival = getMinutesFromStopTime(item);

			console.log(`Arriving in ${arrival - now}`);
			return arrival - now;
		})
		.filter((item) => item > 0);
};

const stopTimeRegex = /^(\d{2}):(\d{2}):(\d{2})$/;
const getMinutesFromStopTime = (input: string): number => {
	const matches = input.match(stopTimeRegex);
	if (!matches || matches.length < 4) {
		return 0;
	}

	const hours = parseInt(matches[1]) || 0;
	const minutes = parseInt(matches[2]) || 0;

	return hours * 60 + minutes;
};

const getSchedules = async (lineId: number): Promise<Schedule[]> => {
	const url = `https://www.ttc.ca/ttcapi/routedetail/get?id=${lineId}`;

	const response = await fetch(url);
	if (!response.ok) {
		throw getFailureMessage(url);
	}

	const body = await response.text();

	const json = JSON.parse(body)['routeBranchesWithStops'].flatMap(
		(item: routeBranchesWithStops) => item['routeBranchStops']
	);
	console.log(json);
	const schedules = json as Schedule[];

	return schedules;
};

const getSchedule = (schedules: Schedule[], stopId: number): Schedule => {
	return schedules.filter((item) => item.id == stopId)[0];
};

const getNextTimeTable = async (lineId: number, stopCode: number): Promise<NextBusStop[]> => {
	const url = `https://www.ttc.ca/ttcapi/routedetail/GetNextBuses?routeId=${lineId}&stopCode=${stopCode}`;

	const response = await fetch(url);
	if (!response.ok) {
		throw getFailureMessage(url);
	}

	const body = await response.text();

	const json = JSON.parse(body);
	console.log(json);
	const schedules = json as NextBusStop[];

	return schedules;
};

export { getNearestStops, getTimeTable };
