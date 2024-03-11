<script lang="ts">
	import { getTimeTable } from '../models/agency';
	import Line from './Line.svelte';

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
	<div class="centre loading">
		<div class="text">Loading</div>
		<div class="loading-item" id="first" />
		<div class="loading-item" id="second" />
		<div class="loading-item" id="third" />
	</div>
{:else if !geolocationCoordinatesFound || !latitude || !longitude}
	<div class="centre error">
		<div class="text">{errorMessage}</div>
	</div>
{:else}
	{#await getTimeTable(latitude, longitude)}
		<div class="centre loading">
			<div class="text">Loading</div>
			<div class="loading-item" id="first" />
			<div class="loading-item" id="second" />
			<div class="loading-item" id="third" />
		</div>
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
		<div class="centre error">
			<div class="text">Unable to reach server</div>
		</div>
	{/await}
{/if}

<style lang="scss">
	.centre {
		position: fixed;
		top: 50%;
		width: 100%;
		display: flex;
		flex-direction: row;
		justify-content: center;

		.text {
			display: flex;
			flex-direction: column;
			justify-content: center;
		}

		.loading-item {
			width: 2em;
			height: 2em;
			color: black;
			background-color: blue;
			margin: 0 1em;

			animation-play-state: running;
			animation-fill-mode: none;
			animation-name: blink;
			animation-duration: 1s;
			animation-iteration-count: infinite;

			@keyframes blink {
				0% {
					opacity: 0;
				}

				50% {
					opacity: 1;
				}

				100% {
					opacity: 0;
				}
			}

			&#first {
				animation-timing-function: steps(4, jump-end);
			}

			&#second {
				animation-timing-function: steps(2, jump-end);
			}

			&#third {
				animation-timing-function: steps(1, jump-end);
			}
		}
	}

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
