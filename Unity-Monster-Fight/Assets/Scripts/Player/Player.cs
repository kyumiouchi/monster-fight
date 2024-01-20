using System;
using Game.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private FloatReference _timerToDestroy;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Renderer _renderer;
        
        private PlayerPool _pool;
        private float _endPlayerPosition;
        private float _currentRemainToDestroy;
        private bool _canDestroyAfterEndPosition;
        private bool _canStartRun;
        private float _widthPlayer;

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

        private void Start()
        {
            _widthPlayer = _renderer.bounds.size.x;
        }

        private void GameStateChanged(GameManager.GameStates gameStates)
        {
            if (gameStates == GameManager.GameStates.StartRound)
            {
                _playerMovement.StartRun();
                _canStartRun = true;
            }
        }
        public void Init(Vector3 startPosition, float speed, PlayerPool pool, float endPlayerPosition)
        {
            _endPlayerPosition = endPlayerPosition - _widthPlayer;
            transform.position = startPosition;
            _playerMovement.SetRunSpeed(speed);
            _pool = pool;
            Clear();
        }

        private void Clear()
        {
            _currentRemainToDestroy = 0;
        }

        private void Update()
        {
            if (!_canStartRun) return;
            if (IsPlayerFarToFinish()) return;
            if (IsCanDestroy())
            {
                Destroy();
            }
        }

        private bool IsCanDestroy()
        {
            _currentRemainToDestroy += Time.deltaTime;
            return _currentRemainToDestroy >= _timerToDestroy.Value;
        }

        private bool IsPlayerFarToFinish()
        {
            return transform.position.x > _endPlayerPosition;
        }

        private void Destroy()
        {
            _pool.Release(this);
            _canStartRun = false;
        }
    }
}
