using System;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Renderer _renderer;

        #region Callback
        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged += GameStateChanged;
        }
        #endregion

        private void GameStateChanged(GameManager.GameStates gameStates)
        {
            if (gameStates == GameManager.GameStates.StartRound)
            {
                _playerMovement.StartRun();
            }
        }

        private void Update()
        {
            Debug.Log("VIsible " + _renderer.isVisible);
        }

        public void SetRunSpeed(float speed)
        {
            _playerMovement.SetRunSpeed(speed);
        }
    }
}
