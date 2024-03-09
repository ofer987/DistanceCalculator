<script lang="ts">
	import { DirectionModel } from '../models/direction';
	import Stop from './Stop.svelte';

	export let direction: DirectionModel;
</script>

<div class="line" class:disabled={!direction.isAvailable}>
	<div id="name">{direction.name}</div>

	{#each direction.stops as stop}
		<Stop {stop} />
	{/each}
</div>

<style lang="scss">
	.line {
		display: flex;
    flex-direction: column;
	}
</style>
