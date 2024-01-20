using System;
using Game.Player;
using Game.Ui;
using UnityEngine;

namespace Game.Manager
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] private RoundUi _roundUi;
        [SerializeField] private PlayerGenerator _playerGenerator;
    
        [SerializeField] private RoundsSo _roundsSo;
        
        [SerializeField] private Camera _mainCamera;
        private float _leftEndWorldPosition;

        private void Start()
        {
            Vector3 leftBottomWorldPosition = _mainCamera.ScreenToWorldPoint(Vector3.zero);
            _leftEndWorldPosition = leftBottomWorldPosition.x;
        }

        public void PrepareRound()
        {
            _playerGenerator.PreparePlayers(_roundsSo.NumberPlayers, _leftEndWorldPosition);
        }
        public void StartRound()
        {
            _roundUi.StartUi(_roundsSo);
        }
    }
}