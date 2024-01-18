using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class PlayerGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Rect _rectAreaInstanciate;
        private PlayerPool _playerPool;
        private int _playerCounter = 0;
        private Coroutine _coroutine;

        private void Start()
        {
            _playerCounter = GetCounter();
            _playerPool = new PlayerPool(CreateInstance);

            _coroutine = StartCoroutine(nameof(SpawnObjects));
        }

        private int GetCounter()
        {
            return 2;
        }

        private IEnumerator SpawnObjects()
        {
            for (int i = 0; i < _playerCounter; i++)
            {
                _playerPool.Pool.Get();
                yield return null;
            }
        }

        private GameObject CreateInstance()
        {
            Vector3 position = SetPlayerPosition();
            GameObject go = Instantiate(_playerPrefab, position, Quaternion.identity);

            return go;
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