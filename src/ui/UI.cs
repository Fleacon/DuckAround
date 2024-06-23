using Godot;
using System;

public partial class UI : CanvasLayer
{
	private Healthbar _healthbar;
	private Score _score;
	private GameData _gameData;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _healthbar = GetNode<Healthbar>("Healthbar");
        _score = GetNode<Score>("Score");
        _gameData = GetNode<GameData>("/root/GameData");
    }

	// Verbinden der Healthbar mit dem Spieler Leben
	public void InitHealthbar()
	{
        _healthbar.Value = _gameData.Player.MaxHealth;
        _healthbar.MaxValue = _gameData.Player.MaxHealth;
    }

	// Verbinden dem ScoreLable mit dem Gegner
	public void InitScore(Frownie frownie)
	{
		frownie.EnemyHealthDepleted += _score.UpdateScore;
	}
}
