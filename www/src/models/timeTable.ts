class TimeTableModel {
	arrivals: string[] = [];

	get isAvailable(): boolean {
		return this.arrivals.length != 0;
	}

	constructor() {}
}

export { TimeTableModel };
