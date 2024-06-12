using Godot;
using System;

public partial class GameData : Node
{
	public Player Player;
	public UI UI;
    public int Score { get; set; }
    public int CurrentWave { get; set; }
	public int CurrentMobLimit { get; set; }
	public int SpawnedMobs { get; set; }
	public int CurrentMobCount { get; set; }
}
