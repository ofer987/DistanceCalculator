import { TripModel } from './trip';

class StopModel {
	id: number;
	name: string;
	lineId: number;
	lineFullName: string;
	latitude: number;
	longitude: number;
	timetable: number[] = [];
	trips: TripModel[] = [];

	get isAvailable(): boolean {
		for (const trip of this.trips) {
			if (trip.isAvailable) {
				return true;
			}
		}

		return false;
	}

	pushTrip(id: number, name: string, arrivals: string[]): void {
		if (arrivals.length == 0) {
			return;
		}

		const trip = new TripModel(id, name, arrivals);
		this.trips.push(trip);
	}

	constructor(
		id: number,
		name: string,
		lineId: number,
		lineFullName: string,
		latitude: number,
		longitude: number
	) {
		this.id = id;
		this.name = name;
		this.lineId = lineId;
		this.lineFullName = lineFullName;
		this.latitude = latitude;
		this.longitude = longitude;
	}
}

export { StopModel };
