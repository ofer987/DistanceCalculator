interface Schedule {
	id: number;
	routeStopTimes: string[];
}

class TimeModel {
	stopTimeRegex = /^(\d{2}):(\d{2}):(\d{2})$/;
	arrivals: number[];

	public static now() {
		const today = new Date();

		return today.getHours() * 60 + today.getMinutes();
	}

	constructor(now: number, routeStopTimes: string[]) {
		this.arrivals = routeStopTimes
			.map((item) => {
				const arrival = this.getMinutesFromStopTime(item);

				return arrival - now;
			})
			.filter((item) => item > 0);
	}

	private getMinutesFromStopTime(input: string): number {
		const matches = input.match(this.stopTimeRegex);
		if (!matches || matches.length < 4) {
			return 0;
		}

		const hours = parseInt(matches[1]) || 0;
		const minutes = parseInt(matches[2]) || 0;

		return hours * 60 + minutes;
	}
}

class ScheduleModel implements Schedule {
	id: number;
	routeStopTimes: string[];

	constructor(routeStopTimes: string[], id: number) {
		this.id = id;
		this.routeStopTimes = routeStopTimes;
	}
}

export { ScheduleModel, TimeModel };
