<script lang="ts">
	import { StopModel } from '../models/stop';
	import Time from './Time.svelte';

	export let stop: StopModel;

	const mapLocation = `https://www.google.com/maps/@${stop.latitude},${stop.longitude},13z`;
</script>

<a class="map-location-anchor" target="_blank" href={mapLocation}>
	<div class="stop" class:disabled={!stop.isAvailable}>
		<div id="name">{stop.name}</div>

		<Time time={stop.timetable} />
	</div>
</a>

<style lang="scss">
	.map-location-anchor {
		color: unset;
		font-style: normal;
		text-decoration: none;

		.stop {
			display: flex;
			flex-direction: column;

			&.disabled {
				display: none;
			}
		}
	}
</style>
