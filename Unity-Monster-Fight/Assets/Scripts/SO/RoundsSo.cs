using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Rounds Data", fileName = "Rounds_SO")]
public class RoundsSo : ScriptableObject
{
    [Header("Round Data")] 
    
    [SerializeField] private int _currentRound = 1;
    public int CurrentRound => _currentRound;

    public int NumberPlayers => GetNumberOfPlayer(_currentRound);

    public void NextRound()
    {
        _currentRound++;
    }

    private List<int> _memorizeSequence = null;

    private int GetNumberOfPlayer(int round)
    {
        _memorizeSequence ??= new List<int>();

        return CalculationFibonacci(round);
    }

    #region Fibonacci
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
