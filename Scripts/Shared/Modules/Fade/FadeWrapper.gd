extends Node2D

signal finished

func fade_out(time := 1.0, color := Color.BLACK, pattern := "", reverse := false, smooth := false):
	await Fade.fade_out(time, color, pattern, reverse, smooth).finished
	finished.emit()

## Fades in the screen, so it's visible again. Use the parameters to customize it.
func fade_in(time := 1.0, color := Color.BLACK, pattern := "", reverse := true, smooth := false):
	await Fade.fade_in(time, color, pattern, reverse, smooth).finished
	finished.emit()
