<script lang="ts">
	export let arrival: number;
</script>

<div class="arrival" class:displayed={arrival > 0}>Arriving in {arrival} minutes</div>

<style lang="scss">
	.arrival {
		display: none;

		&.displayed {
			display: block;
		}
	}
</style>
