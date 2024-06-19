using Godot;
using System;
using System.Linq;
using System.Numerics;

public partial class Frownie : CharacterBody2D
{
	[Export] public int Health { get; set; } = 100;
	[Export] public int AttackDamage { get; set; } = 15;
	[Export] public int Points { get; set; } = 50;
    [Export] public double AttackCooldown { get; set; } = 0.5;
    [Export] public int Speed { get; set; } = 330;
	[Export] public int Acceleration { get; set; } = 7;

    [Signal] public delegate void EnemyHealthDepletedEventHandler(int points);
    [Signal] public delegate void PlayerInAttackRangeEventHandler(int damage);

    private ProgressBar _healthbar;
	private Area2D _hurtbox;
	private Timer _pathfindingTmer;
	private NavigationAgent2D _navigation;
	private Timer _navTimer;
	private AnimatedSprite2D _sprite;
	private GameData _gameData;
	private bool _canAttack = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_healthbar = GetNode<ProgressBar>("Healthbar");
		_hurtbox = GetNode<Area2D>("Hurtbox");
		_navigation = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_navTimer = GetNode<Timer>("NavTimer");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        ConnectSignals();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_healthbar.Value = Health;

		if (Health <= 0) // Entsenden eines Signals wenn der Gegner kein Leben mehr hat
		{
			_gameData.Player.PlayerAttacked -= OnPlayerAttacked;
            QueueFree();
            EmitSignal(SignalName.EnemyHealthDepleted, Points);
		}
		if (_hurtbox.OverlapsBody(_gameData.Player) && _canAttack) // Entsenden eines Signals wenn der Gegner in der naehe des Spielers ist
		{
			_canAttack = false;
			EmitSignal(SignalName.PlayerInAttackRange, AttackDamage);
            GetTree().CreateTimer(AttackCooldown).Timeout += () => _canAttack = true;
        }
		
		CalcMovementDirection(delta);
		MoveAndSlide();
	}

	// Funktion um sich Richtung Spieler zu bewegen
	public void CalcMovementDirection(double delta)
	{
        Godot.Vector2 direction = Godot.Vector2.Zero;
        direction = _navigation.GetNextPathPosition() - GlobalPosition;
        direction = direction.Normalized();
        Velocity = Velocity.Lerp(direction * Speed, (float)(Acceleration * delta));
        if (direction.X < 0)
        {
            _sprite.FlipH = true;
        }
        if (direction.X > 0)
        {
            _sprite.FlipH = false;
        }
    }

	// Funktion um die Position und Eigenschaften des Gegners zu bestimmen, und die referenz GameData zu setzen
	public void Initialize(Godot.Vector2 startPosition, GameData gameData, bool isSpecial)
	{
		_gameData = gameData;
		GlobalPosition = startPosition;
        _gameData.UI.InitScore(this);
		AddToGroup("Enemies");
        _gameData.SpawnedMobs += 1;
		if (isSpecial)
		{
			_healthbar.MaxValue = 250;
			Health = 250;
			Points = 100;
			Speed = 360;
			Scale = new Godot.Vector2((float)1.2,(float)1.2);
		}
    }

	// Funktion, wenn der Spieler attackiert
	public void OnPlayerAttacked(Area2D weaponHitbox, int damage)
	{
        if (weaponHitbox.OverlapsBody(this))
		{
			Health -= damage;
		}
	}

	// Funktion um die Position des Spielers als Navigationstarget zu setzen
	public void SetTarget()
	{
		_navigation.TargetPosition = _gameData.Player.GlobalPosition;
	}

    // Verbinden mit Signalen
    public void ConnectSignals()
	{
		_gameData.Player.PlayerAttacked += OnPlayerAttacked;
		PlayerInAttackRange += _gameData.Player.EnemyAttacked;
        _navTimer.Timeout += SetTarget;
    }
}
