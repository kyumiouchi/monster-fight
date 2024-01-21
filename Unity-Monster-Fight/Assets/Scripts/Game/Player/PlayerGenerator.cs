using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Game.Player
{
    public class PlayerGenerator : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Rect _rectAreaInstanciate;
        [SerializeField] private CharacterSo _characterSo;
        
        private PlayerPool _playerPool;
        
        private int _playerToPool = 0;
        private int _totalEnabledPlayers = 0;
        private int _lastTotalEnabledPlayers = 0;
        private Coroutine _coroutine;
        private float _destroyPlayerPosition;
        private int _finishedPlayers = 0;
        private int _destroyedPlayers = 0;
        private float _widthPlayer = 0;
        private float _heightPlayer = 0;
        
        public Action OnAllPlayerEnded = delegate { };

        private void Start()
        {
            _widthPlayer = _playerPrefab.GetPlayerWidth();
            _heightPlayer = _playerPrefab.GetPlayerHeight();
            _playerPool = new PlayerPool(CreateInstance, UpdatePlayerInfo, Released, WillRelease);
        }

        public void PreparePlayers(int players, float destroyPlayerPosition)
        {
            if (_lastTotalEnabledPlayers == 0)
                _playerToPool = players;
            _totalEnabledPlayers = players;
            _destroyPlayerPosition = destroyPlayerPosition - _widthPlayer;
        }

        private void Update()
        {
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
                _playerToPool = _totalEnabledPlayers;
            }
        }

        private Player CreateInstance()
        {
            Player go = Instantiate(_playerPrefab, transform, true);
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
    }
}