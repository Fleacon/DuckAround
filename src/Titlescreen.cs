using Godot;
using System;

public partial class Titlescreen : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export] PackedScene World { get; set; }
	public override void _Ready()
	{
		GetNode<Button>("StartButton").ButtonDown += () => GetTree().ChangeSceneToPacked(World);
	}
}
