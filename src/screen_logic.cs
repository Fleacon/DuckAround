using Godot;
using System;

public partial class screen_logic : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("change_window"))
		{
			if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Windowed)
			{
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
				return;
			}
			if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen)
			{
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				return;
			}
        }
	}
}
