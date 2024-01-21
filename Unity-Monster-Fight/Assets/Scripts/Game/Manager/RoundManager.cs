using System;
using Game.Player;
using Game.Ui;
using UnityEngine;

namespace Game.Manager
{
    /// <summary>
    /// RoundManager controls the start of the round until the end of the player behavior.
    /// </summary>
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] private RoundUi _roundUi;
        [SerializeField] private PlayerGenerator _playerGenerator;
    
        [SerializeField] private RoundsSo _roundsSo;
        
        [SerializeField] private Camera _mainCamera;
        
        private float _leftEndWorldPosition;
        
        public Action OnEndRound = delegate {};
        public Action OnPreparedRound = delegate {};

        #region Callback

        private void OnEnable()
        {
            _playerGenerator.OnAllPlayerReady += PreparedRound;
            _playerGenerator.OnAllPlayerEnded += EndRound;
        }

        private void OnDisable()
        {
            _playerGenerator.OnAllPlayerReady -= PreparedRound;
            _playerGenerator.OnAllPlayerEnded -= EndRound;
        }

        #endregion
        
        public void InitializeRound()
        {
            _playerGenerator.InitializePlayers(_leftEndWorldPosition);
        }
        
        public void StartRound()
        {
            _roundUi.StartUi(_roundsSo.NumberPlayers, _roundsSo.CurrentRound);
        }
        
        private void Start()
        {
            Vector3 leftBottomWorldPosition = _mainCamera.ScreenToWorldPoint(Vector3.zero);
            _leftEndWorldPosition = leftBottomWorldPosition.x;
        }

        private void PreparedRound()
        {
            OnPreparedRound?.Invoke();
        }

        private void EndRound()
        {
            _roundsSo.SetLastRoundTimer(_roundUi.EndRound());
            OnEndRound?.Invoke();
        }
    }
}