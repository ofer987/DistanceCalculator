import { TimeTableModel } from './timeTable';

class TripModel {
  id: number;
	name: string;
	schedule: TimeTableModel;

	get isAvailable(): boolean {
		return this.schedule.isAvailable;
		// for (const timeTable of this.schedule) {
		// 	if (!timeTable.isAvailable) {
		// 		return false;
		// 	}
		// }
		//
		// return true;
	}

	constructor(id: number, name: string, schedule: string[]) {
    this.id = id;
		this.name = name;
		this.schedule = new TimeTableModel(schedule);
	}
}

export { TripModel };
