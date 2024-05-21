using Godot;
using System;
using System.Linq;

public partial class Frownie : CharacterBody2D
{
	[Export] public int Health { get; set; } = 100;
	[Export] public int AttackDamage { get; set; } = 15;
	[Export] public int Points { get; set; } = 50;

	private ProgressBar _healthbar;
	private Area2D _hurtbox;
	private Player _player;

	private bool _canAttack = true;

	[Signal] public delegate void EnemyHealthDepletedEventHandler(int points);
	[Signal] public delegate void PlayerInAttackRangeEventHandler(int damage);
    [Export] public double AttackCooldown { get; set; } = 0.5;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_healthbar = GetNode<ProgressBar>("Healthbar");
		_hurtbox = GetNode<Area2D>("Hurtbox");
		_player.PlayerAttacked += OnPlayerAttacked;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_healthbar.Value = Health;
		if (Health <= 0)
		{
			_player.PlayerAttacked -= OnPlayerAttacked;
            QueueFree();
            EmitSignal(SignalName.EnemyHealthDepleted, Points);
		}
		if (_hurtbox.OverlapsBody(_player) && _canAttack)
		{
			_canAttack = false;
			EmitSignal(SignalName.PlayerInAttackRange, AttackDamage);
            GetTree().CreateTimer(AttackCooldown).Timeout += () => _canAttack = true;
        }
	}

	public void Initialize(Vector2 startPosition, Player player)
	{	
		_player = player;
		Position = startPosition;
	}

	public void OnPlayerAttacked(Area2D weaponHitbox, int damage)
	{
        GD.Print("Ouch");
        if (weaponHitbox.OverlapsBody(this))
		{
			Health -= damage;
		}
	}
}
