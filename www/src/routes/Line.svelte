<script lang="ts">
	import { LineModel } from '../models/line';
	import Stop from './Stop.svelte';

	export let line: LineModel;
</script>

<div class="line" class:disabled={!line.isAvailable}>
	<div id="name">{line.id}</div>
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
			{#each line.stops as stop}
				<Stop {stop} />
			{/each}
		</div>
	</div>
</div>

<style lang="scss">
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
</style>
