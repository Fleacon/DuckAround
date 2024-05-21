using Godot;
using System;

public partial class Score : Label
{
	public int ScoreCount { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = "Score: 0";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = $"Score: {ScoreCount}";
	}

	public void UpdateScore(int points)
	{
		ScoreCount += points;
	}
}
