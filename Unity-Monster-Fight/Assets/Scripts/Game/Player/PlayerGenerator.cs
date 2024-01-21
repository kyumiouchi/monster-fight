using System;
using System.Collections;
using System.Numerics;
using Game.Manager;
using MyNamespace;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Game.Player
{    
    /// <summary>
    /// PlayerGenerator has responsibility to control of the spawned players.
    /// </summary>
    public class PlayerGenerator : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Rect _rectAreaInstanciate;
        [SerializeField] private CharacterSo _characterSo;
        [SerializeField] private RoundsSo _roundsSo;
        
        private PlayerPool _playerPool;
        
        private int _playerToPool = 0;
        private int _totalEnabledPlayers = 0;
        private int _lastTotalEnabledPlayers = 0;
        private float _destroyPlayerPosition;
        private int _finishedPlayers = 0;
        private int _destroyedPlayers = 0;
        private float _widthPlayer = 0;
        private float _heightPlayer = 0;
        
        
        public Action OnAllPlayerReady = delegate { };
        public Action OnAllPlayerEnded = delegate { };

        private void Start()
        {
            _widthPlayer = _playerPrefab.GetPlayerWidth();
            _heightPlayer = _playerPrefab.GetPlayerHeight();
            _playerPool = new PlayerPool(CreateInstance, UpdatePlayerInfo, Released, WillRelease, ActivatedObjets);
        }

        public void InitializePlayers(float destroyPlayerPosition)
        {
            _totalEnabledPlayers = _roundsSo.NumberPlayers;
            if (_lastTotalEnabledPlayers == 0)
            {
                StartSpawnPlayers();
            }
                
            _destroyPlayerPosition = destroyPlayerPosition - _widthPlayer;
        }

        private void Update()
        {
            SpawnPlayers();
        }

        private void SpawnPlayers()
        {
            if (SettingsManager.Instance.SettingsSo.TypeInstanciate != Settings.NoLoop) return;
            
            if (_playerToPool <= 0) return;

            _playerPool.Get();
            _playerToPool--;
        }

        private void WillRelease()
        {
            _finishedPlayers++;
            if (_finishedPlayers == 1)
            {
                _lastTotalEnabledPlayers = _totalEnabledPlayers;
                _totalEnabledPlayers = 0;
            }
            if (_finishedPlayers == _lastTotalEnabledPlayers)
            {
                _finishedPlayers = 0;
                OnAllPlayerEnded?.Invoke();
            }
        }

        private void Released()
        {
            _destroyedPlayers++;
            if (_destroyedPlayers == _lastTotalEnabledPlayers)
            {
                _destroyedPlayers = 0;
                _lastTotalEnabledPlayers = 0;
                StartSpawnPlayers();
            }
        }

        private void ActivatedObjets()
        {
            int spawnedPlayers = _playerPool.ActivatedObj();
            _roundsSo.SetSpawnedPlayers(spawnedPlayers);
            if (spawnedPlayers == _totalEnabledPlayers)
            {
                OnAllPlayerReady?.Invoke();
            }
        }

        private Player CreateInstance()
        {
            var go = Instantiate(_playerPrefab, transform, true);
            return go;
        }

        private void UpdatePlayerInfo(Player player)
        {
            player.Init(SetPlayerPosition(), _characterSo.GetRandomRunSpeed(), _playerPool, _destroyPlayerPosition);
        }

        private Vector3 SetPlayerPosition()
        {
            var randomPosition = new Vector3(
                Random.Range(_rectAreaInstanciate.x, _rectAreaInstanciate.x + _rectAreaInstanciate.width - _widthPlayer),
                Random.Range(_rectAreaInstanciate.y + _heightPlayer, _rectAreaInstanciate.y + _rectAreaInstanciate.height - _heightPlayer),
                0);

            Vector3 position = transform.position + randomPosition;
            return position;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var position = _rectAreaInstanciate.position + (Vector2)transform.position + _rectAreaInstanciate.size / 2;
            Gizmos.DrawWireCube(position, _rectAreaInstanciate.size);
        }
#endif

        #region Settings

        private Coroutine _coroutine;
        private void StartSpawnPlayers()
        {
            _playerToPool = _totalEnabledPlayers;
            
            switch (SettingsManager.Instance.SettingsSo.TypeInstanciate)
            {
                case Settings.NoLoop:
                    // Look Update()
                    break;
                case Settings.CoroutineAndLoop:
                    _coroutine = StartCoroutine(SpawnPlayersCoroutine());
                    break;
            }
        }

        private IEnumerator SpawnPlayersCoroutine()
        {
            while (_playerToPool > 0)
            {
                _playerPool.Get();
                _playerToPool--;
                yield return null;
            }
        }

        #endregion
    }
}