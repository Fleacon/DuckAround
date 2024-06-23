using Godot;
using System;
using static System.Formats.Asn1.AsnWriter;

public partial class Healthbar : TextureProgressBar
{
    private GameData _gameData;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        _gameData = GetNode<GameData>("/root/GameData");
    }

    // Funktion um die Healthbar zu updaten
    public override void _Process(double delta)
	{
        Value = _gameData.Player.CurrentHealth;
	}
}
