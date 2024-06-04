using Godot;
using System;

public partial class World : Node2D
{
	[Export] public PackedScene FrownieScene {get; set;}
	[Export] public PackedScene PlayerScene {get; set;}
	[Export] public PackedScene GameOverScene { get; set; }
	[Export] public double MobSpawnCooldown = 2.7;

	private Player _player;
	private WeaponHitbox _weaponHitbox;
	private UI _ui;
	private Timer _mobSpawnTimer;

	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_player.PlayerHealthDepleted += GameOver;
		_mobSpawnTimer = GetNode<Timer>("MobSpawnTimer");

        _mobSpawnTimer.Timeout += SpawnFrownie;
        InitializeUI();
    }

	public override void _Process(double delta)
	{
		
    }
    public void InitializeUI()
    {
        _ui = _player.GetNode<UI>("Ui");
        _ui.Initialize(_player);
    }

    public void SpawnFrownie()
	{
        Random rng = new Random();
        for (int i = 0; i < rng.Next(1,5); i++)
        {
            var _frownie = FrownieScene.Instantiate<Frownie>();
            var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");

            mobSpawnLocation.ProgressRatio = GD.Randf();
            _frownie.Initialize(mobSpawnLocation.GlobalPosition, _player);

            _frownie.PlayerInAttackRange += _player.EnemyAttacked;
            _ui.InitScore(_frownie);

            AddChild(_frownie);
        }
    }
	public void GameOver()
	{
		GetTree().ChangeSceneToPacked(GameOverScene);
    }
}
