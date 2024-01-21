using System;
using Game.Ui;
using UnityEngine;

namespace Game.Manager
{
    public class InitializeRoundManager : MonoBehaviour
    {
        [SerializeField] private InitializeRoundUi _initializeRoundUi;
        [SerializeField] private RoundsSo _roundsSo;
        
        public Action OnChargedRound = delegate { };

        #region Callback

        private void OnEnable()
        {
            _initializeRoundUi.OnLoadingFinish += OnLoadingFinish;
        }

        private void OnDisable()
        {
            _initializeRoundUi.OnLoadingFinish -= OnLoadingFinish;
        }

        #endregion
        
        public void InitializeRound()
        {
            _initializeRoundUi.StartLoading(_roundsSo.CurrentRound, _roundsSo.NumberPlayers);
            _roundsSo.AddSpawnedPlayerDelegate(UpdateLoading);
        }

        private void UpdateLoading(int value)
        {
            _initializeRoundUi.UpdateLoading(value);
        }

        private void OnLoadingFinish(float timer)
        {
            _roundsSo.SetLastLoadingTimer(timer);
            _roundsSo.RemoveSpawnedPlayerDelegate(UpdateLoading);
            OnChargedRound?.Invoke();
        }
    }
}