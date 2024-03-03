import { StopModel } from './stop';

class DirectionModel {
	name: string;
	stops: StopModel[] = [];

	get isAvailable(): boolean {
		for (const stop of this.stops) {
			if (!stop.isAvailable) {
				return false;
			}
		}

		return true;
	}

	constructor(name: string) {
		this.name = name;
	}
}

export { DirectionModel };
