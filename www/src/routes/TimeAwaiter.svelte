<script lang="ts">
	import { getSchedules } from '../models/agency';
	import Time from './Time.svelte';

	export let lineId: number;
	export let stopId: number;

	const getMinutes = (): number => {
		const now = new Date();

		return now.getHours() * 60 + now.getMinutes();
	};

	const stopTimeRegex = /^(\d{2}):(\d{2}):(\d{2})$/;
	const getMinutesFromStopTime = (input: string): number => {
		const matches = input.match(stopTimeRegex);
		console.log(matches?.length);
		if (!matches || matches.length < 4) {
			return 0;
		}

		const hours = parseInt(matches[1]) || 0;
		const minutes = parseInt(matches[2]) || 0;

		return hours * 60 + minutes;
	};

	const getArrival = (input: string): number => {
		const now = getMinutes();
		const arrival = getMinutesFromStopTime(input);

		return arrival - now;
	};
</script>

{#await getSchedules(lineId, stopId) then schedule}
	<div>Schedule</div>
	<Time {schedule} />
{/await}

<style lang="scss">
</style>
