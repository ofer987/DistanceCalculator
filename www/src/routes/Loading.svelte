<script lang="ts">
</script>

<div class="centre">
	<div class="text">Loading</div>
	<div class="loading-item" id="first" />
	<div class="loading-item" id="second" />
	<div class="loading-item" id="third" />
</div>

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
</style>
