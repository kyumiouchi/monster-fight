using System;
using Game.Ui;
using UnityEngine;

namespace Game.Manager
{    
    /// <summary>
    /// PrepareRoundManager controls the ready round to start the round.
    /// </summary>
    public class PrepareRoundManager : MonoBehaviour
    {
        [SerializeField] private PrepareRoundUi prepareRoundUi;
        
        [SerializeField] private RoundsSo _roundsSo;

        public Action OnStartRound = delegate { };
        
        #region Callback
        private void OnEnable()
        {
            prepareRoundUi.UiComplete += PrepareGameUiComplete;
        }

        private void OnDisable()
        {
            prepareRoundUi.UiComplete -= PrepareGameUiComplete;
        }
        #endregion
        
        private void PrepareGameUiComplete()
        {
            OnStartRound?.Invoke();
        }

        public void StartPrepareRound()
        {
            prepareRoundUi.StartUi(_roundsSo);
        }
    }
}
