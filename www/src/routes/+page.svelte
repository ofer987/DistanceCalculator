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
				<div
					class="line"
					class:subway-type={line.lineType == 'Subway'}
					class:streetcar-type={line.lineType == 'Streetcar'}
					class:bus-type={line.lineType == 'Bus'}
					class:disabled={!line.isAvailable}
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
	{:catch}
		<Error message="Unable to reach server" />
	{/await}
{/if}

<style lang="scss">
	.timetable {
		display: flex;
		flex-direction: column;
		gap: 2em;

		.line {
			border-width: 0.1em;
			border-style: solid;

			&.disabled {
				display: none;
			}

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
