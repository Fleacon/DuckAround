using Godot;
using System;

public partial class World : Node2D
{
	[Export] public PackedScene FrownieScene {get; set;}
	[Export] public PackedScene PlayerScene {get; set;}
	[Export] public PackedScene GameOverScene { get; set; }
	[Export] public double MobSpawnCooldown = 2.7;

    [Signal] public delegate void WaveBeatenEventHandler();

	private WeaponHitbox _weaponHitbox;
	private Timer _mobSpawnTimer;
    private GameData _gameData;
    private WaveLogic _waveLogic;

    public override void _Ready()
	{
		_mobSpawnTimer = GetNode<Timer>("MobSpawnTimer");
        _gameData = GetNode<GameData>("/root/GameData");
        _gameData.Player = GetNode<Player>("Player");
        _gameData.UI = _gameData.Player.GetNode<UI>("Ui");

        _gameData.UI.InitHealthbar();

        ConnectSignals();

        _gameData.Score = 0;
        _gameData.CurrentWave = 1;
        _gameData.SpawnedMobs = 0;
        _gameData.CurrentMobCount = 0;
        _gameData.CurrentMobLimit = _gameData.CurrentWave * 5;
    }

    public override void _Process(double delta)
    {
        _gameData.CurrentMobCount = GetTree().GetNodesInGroup("Enemies").Count;
        
        if (_gameData.SpawnedMobs <= _gameData.CurrentMobLimit)
        {
            _mobSpawnTimer.Paused = false;
        }
        else
        {
            _mobSpawnTimer.Paused = true;
        }
    }

    public void SpawnFrownie()
	{
        Random rng = new();
        int spawncount = rng.Next(1,5);
        if (spawncount + _gameData.SpawnedMobs > _gameData.CurrentMobLimit)
        {
            spawncount = _gameData.CurrentMobLimit - _gameData.SpawnedMobs;
        }
        for (int i = 0; i < spawncount; i++)
        {
            var frownie = FrownieScene.Instantiate<Frownie>();
            var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
            bool isSpecial = false;

            if (_gameData.CurrentWave % 10 == 0)
            {
                if(rng.Next(1, 101) < 10)
                {
                    isSpecial = true;
                }
            }
            mobSpawnLocation.ProgressRatio = GD.Randf();
            frownie.Initialize(mobSpawnLocation.GlobalPosition, _gameData, isSpecial);

            AddChild(frownie);
        }
    }
	public void GameOver()
	{
		GetTree().ChangeSceneToPacked(GameOverScene);
    }

    public void ConnectSignals()
    {
        _gameData.Player.PlayerHealthDepleted += GameOver;
        _mobSpawnTimer.Timeout += SpawnFrownie;
    }
}
