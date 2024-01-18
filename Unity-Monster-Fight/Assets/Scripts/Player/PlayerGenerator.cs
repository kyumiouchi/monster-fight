using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private Rect _rectAreaInstanciate;
    [SerializeField] private PlayerPool _playerPool;
    private int _playerCounter = 0;
    
    private void Start()
    {
        _playerCounter = GetCounter();
        InvokeRepeating(nameof(Instance),0, 0);
    }

    private int GetCounter()
    {
        return 1;
    }

    private void Instance()
    {
        if (--_playerCounter == 0)
        {
            // if (_playerPool.HasEnemy())
            // {
                // var enemy = _enemyPool.GetEnemy();
                // enemy.GetComponent<FollowPlayer>().SetTarget(_target);
                // enemy.GetComponent<Scoreable>().SetScore(_score);
                // enemy.GetComponent<ObjectToEnemyPool>().SetEnemyPool(_enemyPool);
                // SetEnemyPosition(enemy);
            // }
        }
    }
    
    private void SetPlayerPosition(GameObject enemy)
    {
        var randomPosition = new Vector3(
            Random.Range(_rectAreaInstanciate.x, _rectAreaInstanciate.x + _rectAreaInstanciate.width),
            Random.Range(_rectAreaInstanciate.y, _rectAreaInstanciate.y + _rectAreaInstanciate.height),
            0);

        var enemyPosition = this.transform.position + randomPosition;
        enemy.transform.position = enemyPosition;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = _rectAreaInstanciate.position + (Vector2) transform.position + _rectAreaInstanciate.size/2;
        Gizmos.DrawWireCube(position, _rectAreaInstanciate.size);
    }
}
