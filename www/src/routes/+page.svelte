<script lang="ts">
	import { getTimeTable } from '../models/agency';
	import { onMount } from 'svelte';
	import Line from './Line.svelte';

	if (typeof navigator.geolocation == 'undefined') {
		alert('Browser does not support geolocation');
	}

	const errorMessage = 'Browser does not support geolocation';
	let latitude: number | null;
	let longitude: number | null;
	let geolocationCoordinatesFound: boolean | null;

	onMount(() => {
		if (
			typeof window != 'undefined' &&
			typeof window.navigator != 'undefined' &&
			typeof navigator.geolocation != 'undefined'
		) {
			navigator.geolocation.getCurrentPosition(
				(position) => {
					latitude = position.coords.latitude;
					longitude = position.coords.longitude;

					geolocationCoordinatesFound = true;
				},
				() => {
					geolocationCoordinatesFound = false;
				},
				{ timeout: 5000 }
			);
		}
	});
</script>

<div>
	{#if geolocationCoordinatesFound == null}
		<div>Loading</div>
	{:else if !geolocationCoordinatesFound || !latitude || !longitude}
		<div>{errorMessage}</div>
	{:else}
		{#await getTimeTable(latitude, longitude)}
			<div>Loading</div>
		{:then lines}
			<div class="timetable">
				{#each lines as line}
					<div
						class="line"
						class:subway-type={line.lineType == 'Subway'}
						class:streetcar-type={line.lineType == 'Streetcar'}
						class:bus-type={line.lineType == 'Bus'}
					>
						<div id="agency-name">{line.agencyName}</div>
						<div id="type">{line.lineType}</div>
						<div id="id">{line.id}</div>

						<div class="directions">
							{#each line.directions as direction}
								<Line {direction} />
							{/each}
						</div>
					</div>
				{/each}
			</div>
		{:catch error}
			<div>Oops ${error}</div>
		{/await}
	{/if}
</div>

<style lang="scss">
	.timetable {
		display: flex;
		flex-direction: column;
		gap: 2em;

		.line {
			border-width: 0.1em;
			border-style: solid;

			&.subway-type {
				border-color: blue;
			}

			&.streetcar-type {
				border-color: turquoise;
			}

			&.bus-type {
				border-color: green;
			}

			.directions {
				display: flex;
				flex-direction: row;
				justify-content: space-between;
			}
		}
	}
</style>
