class TimeTableModel {
	arrivals: string[];

	get isAvailable(): boolean {
		return this.arrivals.length != 0;
	}

	constructor(arrivals: string[]) {
		this.arrivals = arrivals;
	}
}

export { TimeTableModel };
