extends Node2D

signal finished

func fade_out(time := 1.0, color := Color.BLACK, pattern := "", reverse := false, smooth := false):
	if Fade:
		await Fade.fade_out(time, color, pattern, reverse, smooth).finished
		finished.emit()
	else:
		print("\"Fade\" was not available to fade out.")

## Fades in the screen, so it's visible again. Use the parameters to customize it.
func fade_in(time := 1.0, color := Color.BLACK, pattern := "", reverse := true, smooth := false):
	if Fade:
		await Fade.fade_in(time, color, pattern, reverse, smooth).finished
		finished.emit()
	else:
		print("\"Fade\" was not available to fade in.")
