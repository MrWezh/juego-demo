extends Sprite2D

func _process(delta):
	rotation_degrees -= delta * 720;
	rotation_degrees = fmod(rotation_degrees, 360);
