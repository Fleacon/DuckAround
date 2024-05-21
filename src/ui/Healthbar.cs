using Godot;
using System;

public partial class Healthbar : TextureProgressBar
{
	private Player _player;
	public void ChangePlayerHealth(int currentHealth)
	{
		Value = currentHealth;
	}
}
