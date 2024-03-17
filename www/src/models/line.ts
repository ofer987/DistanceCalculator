type LineTypes = 'Subway' | 'Bus' | 'Streetcar';

import { StopModel } from './stop';

class LineModel {
	type: number;
	agencyName: string;
	id: number;
	stops: StopModel[] = [];

	get isAvailable(): boolean {
		for (const stop of this.stops) {
			if (stop.isAvailable) {
				return true;
			}
		}

		return false;
	}

	get lineType(): LineTypes {
		switch (this.type) {
			case 0:
				return 'Subway';
			case 1:
				return 'Bus';
			case 2:
			default:
				return 'Streetcar';
		}
	}

	constructor(type: number, agencyName: string, id: number) {
		this.type = type;
		this.agencyName = agencyName;
		this.id = id;
	}
}

export { LineModel };
