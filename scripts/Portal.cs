using Godot;
using System;

public partial class Portal : Area2D
{
	public void _on_body_entered(Node2D body)
	{
		if (body is MainCharacter)
		{
			string currentScene = GetTree().CurrentScene.SceneFilePath;
			if (currentScene == "res://scenes/juegoDemo.tscn")
			{
				GetTree().CallDeferred("change_scene_to_file", "res://scenes/nivel_2.tscn");
			}
			else if (currentScene == "res://scenes/nivel_2.tscn")
			{
				GetTree().CallDeferred("change_scene_to_file", "res://scenes/menu.tscn");
			}
		}
	}
}
