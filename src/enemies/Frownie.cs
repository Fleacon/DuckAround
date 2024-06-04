using Godot;
using System;
using System.Linq;

public partial class Frownie : CharacterBody2D
{
	[Export] public int Health { get; set; } = 100;
	[Export] public int AttackDamage { get; set; } = 15;
	[Export] public int Points { get; set; } = 50;
    [Export] public double AttackCooldown { get; set; } = 0.5;
    [Export] public int Speed { get; set; } = 350;
	[Export] public int Acceleration { get; set; } = 7;

    [Signal] public delegate void EnemyHealthDepletedEventHandler(int points);
    [Signal] public delegate void PlayerInAttackRangeEventHandler(int damage);

    private ProgressBar _healthbar;
	private Area2D _hurtbox;
	private Player _player;
	private Timer _pathfindingTmer;
	private NavigationAgent2D _navigation;
	private Timer _navTimer;
	private AnimatedSprite2D _sprite;
	private bool _canAttack = true;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_healthbar = GetNode<ProgressBar>("Healthbar");
		_hurtbox = GetNode<Area2D>("Hurtbox");
		_navigation = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_navTimer = GetNode<Timer>("NavTimer");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		_navTimer.Timeout += SetTarget;
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
		Vector2 direction = Vector2.Zero;
		direction = _navigation.GetNextPathPosition() - GlobalPosition;
		direction = direction.Normalized();
		Velocity = Velocity.Lerp(direction*Speed, (float)(Acceleration * delta));
		if(direction.X < 0)
		{
			_sprite.FlipH = true;
		}
		else
		{
			_sprite.FlipH = false;
		}
		MoveAndSlide();
	}

	public void Initialize(Vector2 startPosition, Player player)
	{	
		_player = player;
		GlobalPosition = startPosition;
	}

	public void OnPlayerAttacked(Area2D weaponHitbox, int damage)
	{
        GD.Print("Ouch");
        if (weaponHitbox.OverlapsBody(this))
		{
			Health -= damage;
		}
	}

	public void SetTarget()
	{
		GD.Print("YO");
		_navigation.TargetPosition = _player.GlobalPosition;
	}
}
