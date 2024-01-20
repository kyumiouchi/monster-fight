using System;
using UnityEngine;

namespace Game.Ui
{
    public class GameUi : MonoBehaviour
    {
        [SerializeField] private PrepareGameUi _prepareGameUi;
        [SerializeField] private RoundUi _roundUi;

        [SerializeField] private RoundsSo _roundsSo;

        #region Callback

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameStateChanged;
            _prepareGameUi.UiComplete += PrepareGameUiComplete;
            _roundUi.uiComplete += PrepareGameUiComplete;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameStateChanged;
            _prepareGameUi.UiComplete -= PrepareGameUiComplete;
            _roundUi.uiComplete -= PrepareGameUiComplete;
        }

        #endregion

        private void GameStateChanged(GameManager.GameStates state)
        {
            switch (state)
            {
                case GameManager.GameStates.PrepareGame:
                    _prepareGameUi.StartUi(_roundsSo);
                    break;

                case GameManager.GameStates.StartRound:
                    _roundUi.StartUi(_roundsSo);
                    break;

                case GameManager.GameStates.EndRound:
                    break;

                case GameManager.GameStates.Restart:
                    break;

                case GameManager.GameStates.Init:
                // Intentional fallthrough. Init isn't supported but will be explicitly handled above.
                default:
                    throw new ArgumentOutOfRangeException($"State machine doesn't support state {state}.");
            }
        }

        private void PrepareGameUiComplete()
        {
            GameManager.Instance.StartRound();
        }
    }
}
