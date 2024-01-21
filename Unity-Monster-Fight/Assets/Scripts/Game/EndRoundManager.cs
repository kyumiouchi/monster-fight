using System;
using Game.Utils;
using UnityEngine;

public class EndRoundManager : MonoBehaviour
{
    [SerializeField] private EndRoundUi _endRoundUi;
    [SerializeField] private RoundsSo _roundsSo;

    public Action OnNextRound = delegate {};
    
    #region Callback

    private void OnEnable()
    {
        _endRoundUi.OnNextRound += StartNextRound;
        _endRoundUi.OnExitGame += ExitGame;
    }

    private void OnDisable()
    {
        _endRoundUi.OnNextRound -= StartNextRound;
        _endRoundUi.OnExitGame -= ExitGame;
    }

    #endregion

    public void EndRound()
    {
        _endRoundUi.StartEndRound(_roundsSo.CurrentRound, _roundsSo.NumberPlayers, _roundsSo.CurrentRoundTimer);
    }
    
    private void StartNextRound()
    {
        _roundsSo.NextRound();
        OnNextRound?.Invoke();
    }
    
    private void ExitGame()
    {
        _roundsSo.RestartRound();
        Loader.Load(Loader.Scene.SCN_Menu);
    }
}
