using System.Collections;
using System.Collections.Generic;
using Game.Generic;
using UnityEngine;

public class PlayerPool : ICustomObjectPool<Player>
{
    private int _startSize;
    private ICustomPool<Player> _pool;

    Transform _parent;
    
    public PlayerPool(Transform parent, int startSize)
    {
        _parent = parent;
        _startSize = startSize;
        _pool = new CustomPool<Player>(this);
    }
    
    #region PoolSettings
    public void TakeObject(Player objPool)
    {
        objPool.gameObject.SetActive(true);
    }

    public Player CreateObject()
    {
        GameObject gameObject = new GameObject(nameof(Player));
        var newObject = gameObject.AddComponent<Player>();

        newObject.transform.SetParent(_parent);
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    public void ReleaseObject(Player objToReturn)
    {
        objToReturn.gameObject.SetActive(false);
    }

    #endregion
}
