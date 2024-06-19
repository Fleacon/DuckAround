using Godot;
using System;

public partial class WaveLogic : Node
{
    private GameData _gameData;
    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
    }

    public override void _Process(double delta)
    {
        if ((_gameData.CurrentMobCount <= 0) && (_gameData.SpawnedMobs == _gameData.CurrentMobLimit))
        {
            CreateNewWave();
        }
    }

    // Erstellen einer neuen Welle
    public void CreateNewWave()
    {
        _gameData.SpawnedMobs = 0;
        _gameData.CurrentWave += 1;
        _gameData.CurrentMobLimit = _gameData.CurrentWave * 5;
        if(_gameData.CurrentWave % 5 == 0)
        {
            _gameData.Player.CurrentHealth += 10;
        }
    }
}
