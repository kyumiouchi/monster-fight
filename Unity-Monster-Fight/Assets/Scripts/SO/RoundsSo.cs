using System.Collections.Generic;
using UnityEngine;
using BigInteger = System.Numerics.BigInteger;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Rounds Data", fileName = "Rounds_SO")]
public class RoundsSo : ScriptableObject
{
    [Header("Round Data")] 
    
    [SerializeField] private int _currentRound = 1;

    [SerializeField] private BigInteger _numberPlayers = 1;

    public int CurrentRound => _currentRound;

    public BigInteger NumberPlayers => _numberPlayers;

    public void NextRound()
    {
        _currentRound++;
        _numberPlayers = UpdateNumberOfPlayer(_currentRound);
        Debug.Log($"round {_currentRound} PLayers {_numberPlayers}");
    }

    private List<BigInteger> _memorizeSequence = null;

    private BigInteger UpdateNumberOfPlayer(int round)
    {
        if (_memorizeSequence == null)
            _memorizeSequence = new List<BigInteger>();

        return CalculationFibonacci(round);
    }

    #region Fibonacci
    private BigInteger CalculationFibonacci(int value)
    {
        if (value <= 0)
            return 1;
        
        BigInteger result = 0;
        if (_memorizeSequence.Count < value)
        {
            for (int i = _memorizeSequence.Count + 1; i <= value; i++)
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
    
    private BigInteger GetFibonacciValueOfMemorize(int position)
    {
        if (_memorizeSequence.Count >= position)
        {
            return _memorizeSequence[position - 1];
        }

        BigInteger result = 0;
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
