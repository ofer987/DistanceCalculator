<script lang="ts">
	export let time: number[];

	console.log(`there are ${time.length} objects`);
</script>

<div class="arrivals" class:is-displayed={time.length > 0}>
	<h3>Schedule</h3>

	{#each time as item}
		<div>Arriving in {item} minutes</div>
	{/each}
</div>

<style lang="scss">
	.arrivals {
		display: none;

		&.is-displayed {
			display: block;
		}
	}
</style>
