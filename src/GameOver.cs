using Godot;
using System;

public partial class GameOver : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetNode<Button>("StartButton").ButtonDown += () => GetTree().ChangeSceneToFile("res://src/World.tscn");
    }
}
