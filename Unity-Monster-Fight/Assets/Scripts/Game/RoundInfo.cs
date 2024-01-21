using System;
using UnityEngine.Events;

[Serializable]
public class RoundInfo
{
    public float RoundTimer;
    public float LoadingTimer;
    private int _spawnedPlayers;
    public int SpawnedPlayers
    {
        get => _spawnedPlayers;
        set
        {
            _spawnedPlayers = value;
            OnUpdateSpawnedPlayers?.Invoke(value);
        }
    }

    public UnityAction<int> OnUpdateSpawnedPlayers = delegate { };
}