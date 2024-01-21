using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Rounds Data", fileName = "Rounds_SO")]
public class RoundsSo : ScriptableObject
{
    [Header("Round Data")] 
    
    [SerializeField] private int _currentRound = 1;

    [SerializeField] private RoundInfo _lastRoundData = new RoundInfo();
    public int CurrentRound => _currentRound;

    public int NumberPlayers => GetNumberOfPlayer(_currentRound);
    public RoundInfo LastRoundData => _lastRoundData;
    
    public void NextRound()
    {
        ClearRoundData();
        _currentRound++;
    }

    public void SetLastRoundTimer(float timer)
    {
        _lastRoundData.RoundTimer = timer;
    }

    public void SetLastLoadingTimer(float timer)
    {
        _lastRoundData.LoadingTimer = timer;
    }

    public void SetSpawnedPlayers(int currentPlayers)
    {
        _lastRoundData.SpawnedPlayers = currentPlayers;
    }

    public void AddSpawnedPlayerDelegate(UnityAction<int> action)
    {
        _lastRoundData.OnUpdateSpawnedPlayers += action;
    }

    public void RemoveSpawnedPlayerDelegate(UnityAction<int> action)
    {
        _lastRoundData.OnUpdateSpawnedPlayers -= action;
    }
    
    public void RestartRound()
    {
        _currentRound = 1;
        ClearRoundData();
    }

    private void ClearRoundData()
    {
        _lastRoundData.RoundTimer = 0;
        _lastRoundData.LoadingTimer = 0;
        _lastRoundData.SpawnedPlayers = 0;
    }
    
    #region Fibonacci
    private List<int> _memorizeSequence = null;
    
    private int GetNumberOfPlayer(int round)
    {
        _memorizeSequence ??= new List<int>();

        return CalculationFibonacci(round);
    }
    private int CalculationFibonacci(int value)
    {
        if (value <= 0)
            return 1;
        
        int result = 0;
        if (_memorizeSequence.Count < value)
        {
            for (var i = _memorizeSequence.Count + 1; i <= value; i++)
            {
                result = GetFibonacciValueOfMemorize(i);
            }
        }
        else
        {
            result = GetFibonacciValueOfMemorize(value);
        }

        return result;
    }
    
    private int GetFibonacciValueOfMemorize(int position)
    {
        if (_memorizeSequence.Count >= position)
        {
            return _memorizeSequence[position - 1];
        }

        int result = 0;
        if (position is 1 or 2)
        {
            _memorizeSequence.Add(1);
            result = _memorizeSequence[position - 1];
        }
        else
        {
            _memorizeSequence.Add(GetFibonacciValueOfMemorize(position - 1) 
                                  + GetFibonacciValueOfMemorize(position - 2));
            result = _memorizeSequence[position - 1];
        }

        return result;
    }
    #endregion
}