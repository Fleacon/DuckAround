using Godot;
using System;

public partial class GameOver : Control
{
    GameData _gameData;
    public override void _Ready()
	{
        GetNode<Button>("StartButton").ButtonDown += () => GetTree().ChangeSceneToFile("res://src/World.tscn");
        _gameData = GetNode<GameData>("/root/GameData");
        GetNode<Label>("Wave").Text = $"Wave: {_gameData.CurrentWave}";
        GetNode<Label>("Score").Text = $"Score: {_gameData.Score}";
    }

    public void RestartGame()
    {
        GetTree().ChangeSceneToFile("res://src/World.tscn");
    }
}
