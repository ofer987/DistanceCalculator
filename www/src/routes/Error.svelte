<script lang="ts">
	export let message: string;
</script>

<div class="centre error">
	<div class="text">{message}</div>
</div>

<style lang="scss">
	.centre {
		position: fixed;
		top: 50%;
		width: 100%;
		display: flex;
		flex-direction: row;
		justify-content: center;

		&.error {
			color: red;
		}

		.text {
			display: flex;
			flex-direction: column;
			justify-content: center;
		}
	}
</style>
