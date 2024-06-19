using Godot;
using System;

public partial class Healthbar : TextureProgressBar
{
	private Player _player;

	// Funktion um die Healthbar zu updaten
	public void ChangePlayerHealth(int currentHealth)
	{
		Value = currentHealth;
	}
}
