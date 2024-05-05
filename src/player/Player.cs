using Godot;
using System;
using System.Numerics;

public partial class Player : CharacterBody2D
{
	public int Health { 
		get => _health;
		set{
			if (value < 0) _health = 0;
			if (value > 100) _health = 100;
			else _health = value;
		}
	}
	public int Speed { get; set; } = 400;
	public int AttackDamage { get; set; } = 10;
	public int Defense { get; set; } = 10;
	public int CritChance { get; set; } = 0;
	public int Level { get; set; } = 0;
	public int XP { get; set; } = 0;
	public Weapon CurrentWeapon { get; set; } = null;

	private int _health;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Movement(delta);
	}

	private void Movement(double delta) {
		var velocity = Godot.Vector2.Zero;

		if(Input.IsActionPressed("move_up")) velocity.Y -= 1;
		if(Input.IsActionPressed("move_down")) velocity.Y += 1;
		if(Input.IsActionPressed("move_right")) velocity.X += 1;
		if(Input.IsActionPressed("move_left")) velocity.X -= 1;

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if(velocity.Length() > 0) {
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else {animatedSprite2D.Stop();}

		Position += velocity * (float)delta;
	}
}
