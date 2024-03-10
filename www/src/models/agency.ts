import { LineModel } from './line';
import { DirectionModel } from './direction';
import { StopModel } from './stop';
import { TimeModel } from './schedule';

interface Schedule {
	id: number;
	routeStopTimes: string[];
}

interface Line {
	type: number;
	agencyName: string;
	id: number;
	name: string;
	fullName: string;
}

interface Stop {
	line: Line;
	id: number;
	latitude: number;
	longitude: number;
}

interface routeBranchesWithStops {
	routeBranchStops: Schedule;
}

const getFailureMessage = (url: string) => {
	return `Failed to retrieve for URL, ${url}`;
};

const getNearestStops = async (latitude: number, longitude: number): Promise<Stop[]> => {
	const url = `http://localhost:5292/nearest-stops?latitude=${latitude}&longitude=${longitude}`;
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
		if (item.line.name == 'ALL BRANCHES') {
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

		const direction = new DirectionModel(item.line.name);

		const stop = new StopModel(
			item.id,
			item.line.id,
			item.line.fullName,
			item.latitude,
			item.longitude
		);

		direction.stops.push(stop);
		line.directions.push(direction);
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
		for (const direction of line.directions) {
			for (const stop of direction.stops) {
				const schedule = await getSchedules(line.id, stop.id);

				stop.timetable = getArrivals(now, schedule.routeStopTimes);
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

const getSchedules = async (lineId: number, stopId: number): Promise<Schedule> => {
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

	return schedules.filter((item) => item.id == stopId)[0];
};

export { getNearestStops, getSchedules, getTimeTable };