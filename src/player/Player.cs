using Godot;
using System;
using System.Numerics;

public partial class Player : CharacterBody2D
{
	[Export] public int MaxHealth { get; set; } = 100;
	public int CurrentHealth { get; set; }
	[Export] public int Speed { get; set; } = 310;
	[Export] public int Damage { get; set; } = 10;
	[Export] public double AttackCooldown { get; set; } = 0.5;

    [Signal] public delegate void PlayerAttackedEventHandler(Area2D weaponHitbox, int damage);
    [Signal] public delegate void PlayerTookDamageEventHandler(int currentHealth);
    [Signal] public delegate void PlayerHealthDepletedEventHandler();

	private Area2D _weaponHitbox;
	private AnimatedSprite2D _sprite;
	private AnimatedSprite2D _weaponAnimation;
	private AudioStreamPlayer _attackSFX;
    private bool _canAttack = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_weaponHitbox = GetNode<WeaponHitbox>("WeaponHitbox");
		_weaponAnimation = _weaponHitbox.GetNode("CollisionShape2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_attackSFX = GetNode<AudioStreamPlayer>("AttackSFX");
		CurrentHealth = MaxHealth;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CalcMovementDirection();
		MoveAndSlide();
		if(CurrentHealth > 100) CurrentHealth = 100;
		if(Input.IsActionJustPressed("attack")) Attack(Damage);
		if(CurrentHealth < 0) EmitSignal(SignalName.PlayerHealthDepleted);
    }

	//Berechnen in welche Richtung der Spieler laeuft
	public void CalcMovementDirection() {
		Godot.Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = inputDirection * Speed;
		if(inputDirection.X < 0)
		{
			_sprite.FlipH = true;
		}
		if(inputDirection.X > 0)
		{
			_sprite.FlipH = false;
		}
		if(Velocity.Length() > 0) 
		{
			_sprite.Play();
		}
		else
		{
			_sprite.Stop();	
		}
	}

	//Funktion zum Angreifen
	public void Attack(int damage)
	{
		if(_canAttack)
		{
			_canAttack = false;
			_attackSFX.Play();
			_weaponAnimation.Play();
			EmitSignal(SignalName.PlayerAttacked, _weaponHitbox, damage);
			GetTree().CreateTimer(AttackCooldown).Timeout += () => _canAttack = true;
		}
	}

	//Entsenden eines Signals wenn ein Gegner den Spieler attackiert
	public void EnemyAttacked(int damage)
	{
		CurrentHealth -= damage;
        EmitSignal(SignalName.PlayerTookDamage, CurrentHealth);
	}
}
