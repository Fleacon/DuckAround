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

	private int _health;
	private AnimatedSprite2D _sprite;
	private Timer _clock;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_clock = GetNode<Timer>("Timer");

		_clock.Timeout += Attack;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();
	}

	//Movement logic
	private void GetInput() {
		Godot.Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = inputDirection * Speed;
		if(Velocity.Length() > 0) 
		{
			_sprite.Play();
		}
		else
		{
			_sprite.Stop();	
		}
	}

	private void Attack() {
		
	}
}
