using Godot;
using System;

public partial class UI : CanvasLayer
{
	private Healthbar _healthbar;
	private Score _score;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _healthbar = GetNode<Healthbar>("MarginContainer/VBoxContainer/MarginContainer/HBoxContainer/Healthbar");
		_score = GetNode<Score>("MarginContainer/VBoxContainer/MarginContainer/HBoxContainer/Score");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void Initialize(Player player)
	{
		InitHealthbar(player);
    }

	public void InitHealthbar(Player player)
	{
        player.PlayerTookDamage += _healthbar.ChangePlayerHealth;
        _healthbar.Value = player.MaxHealth;
        _healthbar.MaxValue = player.MaxHealth;
    }

	public void InitScore(Frownie frownie)
	{
		GD.Print(frownie);
		frownie.EnemyHealthDepleted += _score.UpdateScore;
	}
}
