using Godot;
using System;

public partial class Score : Label
{
	private GameData _gameData;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _gameData = GetNode<GameData>("/root/GameData");
        Text = "Score: 0";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = $"{_gameData.Score} :Score";
	}

	// Funktion um den Score zu updaten
	public void UpdateScore(int points)
	{
		_gameData.Score += points;
	}
}
