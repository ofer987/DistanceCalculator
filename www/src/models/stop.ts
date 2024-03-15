class StopModel {
	id: number;
	name: string;
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
