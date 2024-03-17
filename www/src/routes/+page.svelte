<script lang="ts">
	import { getTimeTable } from '../models/agency';
	import Line from './Line.svelte';
	import Loading from './Loading.svelte';
	import Error from './Error.svelte';

	if (typeof navigator.geolocation == 'undefined') {
		alert('Browser does not support geolocation');
	}

	const errorMessage = 'Browser does not support geolocation';
	let latitude: number | null;
	let longitude: number | null;
	let geolocationCoordinatesFound: boolean | null;

	if (window?.navigator?.geolocation) {
		navigator.geolocation.getCurrentPosition(
			(position) => {
				latitude = position.coords.latitude;
				longitude = position.coords.longitude;

				console.log(`latitude is ${latitude}`);
				console.log(`longitude is ${longitude}`);

				geolocationCoordinatesFound = true;
			},
			() => {
				geolocationCoordinatesFound = false;
			},
			{ timeout: 5000 }
		);
	}
</script>

{#if geolocationCoordinatesFound == null}
	<Loading />
{:else if !geolocationCoordinatesFound || !latitude || !longitude}
	<Error message={errorMessage} />
{:else}
	{#await getTimeTable(latitude, longitude)}
		<Loading />
	{:then lines}
		<div class="timetable">
			{#each lines as line}
				<Line {line} />
			{/each}
		</div>
	{:catch}
		<Error message="Unable to reach server" />
	{/await}
{/if}

<style lang="scss">
	.timetable {
		display: flex;
		flex-direction: column;
		gap: 2em;
	}
</style>
