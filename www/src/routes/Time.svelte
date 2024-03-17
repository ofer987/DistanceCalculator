<script lang="ts">
	import { TripModel } from '../models/trip';

	export let trip: TripModel;

	console.log(`there are ${trip.schedule.arrivals.length} objects`);
</script>

<div class="arrivals" class:disabled={!trip.schedule.isAvailable}>
	<h3>Schedule</h3>

	<div class="id">{trip.id}</div>
	<div class="name">{trip.name}</div>

	{#each trip.schedule.arrivals as arrival}
		<div>Arriving in {arrival} minutes</div>
	{/each}
</div>

<style lang="scss">
	.arrivals {
		display: block;

		&.disabled {
			display: none;
		}
	}
</style>
