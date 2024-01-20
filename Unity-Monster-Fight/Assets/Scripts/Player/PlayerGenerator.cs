using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Game.Player
{
    public class PlayerGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Rect _rectAreaInstanciate;
        [SerializeField] private CharacterSo _characterSo;
        
        private PlayerPool _playerPool;
        private BigInteger _playerCounter = 0;
        private Coroutine _coroutine;

        private void Start()
        {
            _playerPool = new PlayerPool(CreateInstance, UpdatePlayerInfo);
        }
        
        public void PreparePlayers(BigInteger players)
        {
            _playerCounter = players;
        }

        private void Update()
        {
            if (_playerCounter <= 0) return;
            
            _playerPool.Pool.Get();
            _playerCounter--;
        }

        private GameObject CreateInstance()
        {
            GameObject go = Instantiate(_playerPrefab, transform, true);
            return go;
        }

        private void UpdatePlayerInfo(Player player)
        {
            player.gameObject.transform.position = SetPlayerPosition();
            player.SetRunSpeed(_characterSo.GetRandomRunSpeed());
        }

        private Vector3 SetPlayerPosition()
        {
            var randomPosition = new Vector3(
                Random.Range(_rectAreaInstanciate.x, _rectAreaInstanciate.x + _rectAreaInstanciate.width),
                Random.Range(_rectAreaInstanciate.y, _rectAreaInstanciate.y + _rectAreaInstanciate.height),
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