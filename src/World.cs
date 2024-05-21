using Godot;
using System;

public partial class World : Node2D
{
	[Export] public PackedScene FrownieScene {get; set;}
	[Export] public PackedScene PlayerScene {get; set;}

	private Player _player;
	private WeaponHitbox _weaponHitbox;
	private UI _ui;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(HasNode("Player"))
		{
			_player = GetNode<Player>("Player");
		} 
		else
		{
            _player = PlayerScene.Instantiate<Player>();
        }
		InitializeUI();
		SpawnFrownie();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
		var _frownie = FrownieScene.Instantiate<Frownie>();
		_frownie.Initialize(new Vector2(100,100), _player);
		_frownie.PlayerInAttackRange += _player.EnemyAttacked;
		_ui.InitScore(_frownie);
		AddChild(_frownie);
	}
}
