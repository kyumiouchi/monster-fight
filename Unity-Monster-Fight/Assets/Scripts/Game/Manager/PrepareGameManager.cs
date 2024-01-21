using System;
using Game.Ui;
using UnityEngine;

namespace Game.Manager
{
    public class PrepareGameManager : MonoBehaviour
    {
        [SerializeField] private PrepareGameUi _prepareGameUi;
        
        [SerializeField] private RoundsSo _roundsSo;

        public Action OnStartRound = delegate { };
        
        #region Callback
        private void OnEnable()
        {
            _prepareGameUi.UiComplete += PrepareGameUiComplete;
        }

        private void OnDisable()
        {
            _prepareGameUi.UiComplete -= PrepareGameUiComplete;
        }
        #endregion
        
        private void PrepareGameUiComplete()
        {
            OnStartRound?.Invoke();
        }

        public void StartPrepareGame()
        {
            _prepareGameUi.StartUi(_roundsSo);
        }
    }
}
