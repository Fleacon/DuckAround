using Godot;
using System;

public partial class Frownie : CharacterBody2D
{
	[Export]
	public int Health { get; set; } = 100;

	ProgressBar healthbar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var healthbar = GetNode<ProgressBar>("Healthbar");
		healthbar.Value = (double)Health;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
