class StopModel {
	id: number;
	lineId: number;
	lineFullName: string;
	latitude: number;
	longitude: number;
	timetable: number[] = [];

	get isAvailable(): boolean {
		return this.timetable.length > 0;
	}

	constructor(
		id: number,
		lineId: number,
		lineFullName: string,
		latitude: number,
		longitude: number
	) {
		this.id = id;
		this.lineId = lineId;
		this.lineFullName = lineFullName;
		this.latitude = latitude;
		this.longitude = longitude;
	}
}

export { StopModel };
