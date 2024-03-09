<script lang="ts">
	import { StopModel } from '../models/stop';
	import Time from './Time.svelte';

	export let stop: StopModel;
</script>

<div class="stop" class:disabled={!stop.isAvailable}>
	<!-- <div id="id">{stop.id}</div> -->
	<!-- <div id="latitude">{stop.latitude}</div> -->
	<!-- <div id="longitude">{stop.longitude}</div> -->

	<Time time={stop.timetable} />
</div>

<style lang="scss">
	.stop {
		display: flex;

		&.disabled {
			display: none;
		}
	}
</style>
