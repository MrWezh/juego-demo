using Godot;
using System;

public partial class Menu : Control
{
	public void _on_start_pressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/juegoDemo.tscn"); 
    }
	public void _on_settings_pressed()
    {
        
    }
	public void _on_exit_pressed()
    {
        GetTree().Quit();
    }

}
