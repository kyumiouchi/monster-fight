using Game.Generic;
using Game.Manager;
using UnityEngine;

namespace Game.Player
{    
    /// <summary>
    /// Player has all control of the player.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private FloatReference _timerToDestroy;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Renderer _renderer;
        
        private PlayerPool _pool;
        private float _endPlayerPosition;
        private float _currentRemainToDestroy;
        private bool _canDestroyAfterEndPosition;
        private bool _canRelease;

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

        private void GameStateChanged(GameStates gameStates)
        {
            if (gameStates == GameStates.StartRound)
            {
                _playerMovement.StartRun();
            }
        }
        public void Init(Vector3 startPosition, float speed, PlayerPool pool, float endPlayerPosition)
        {
            Clear();
            _endPlayerPosition = endPlayerPosition;
            transform.position = startPosition;
            _playerMovement.SetRunSpeed(speed);
            _pool = pool;
        }

        private void Clear()
        {
            _playerMovement.StopRun();
            _canRelease = false;
            _currentRemainToDestroy = 0;
        }

        private void Update()
        {
            if (!_playerMovement.IsRunning) return;
            if (IsPlayerFarToFinish()) return;
            if (!_canRelease) CanRelease();
            if (IsCanDestroy())
            {
                Destroy();
            }
        }

        private void CanRelease()
        {
            _canRelease = true;
            _pool.CanRelease();
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
            Clear();
            _pool.Release(this);
        }

        public float GetPlayerWidth()
        {
            return _renderer.bounds.size.x / 2;
        }

        public float GetPlayerHeight()
        {
            return _renderer.bounds.size.y / 2;
        }
    }
}
