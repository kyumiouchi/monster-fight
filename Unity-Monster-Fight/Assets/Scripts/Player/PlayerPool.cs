using System;
using Game.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    public class PlayerPool : ICustomObjectPool<Player>
    {
        private ICustomPool<Player> _pool;
        public ICustomPool<Player> Pool => _pool;

        private Func<GameObject> OnCreateInstance;
        private UnityAction<Player> OnUpdatePlayerInfo;

        public PlayerPool(Func<GameObject> onCreateInstance, UnityAction<Player> onUpdatePlayerInfo)
        {
            OnCreateInstance = onCreateInstance;
            OnUpdatePlayerInfo = onUpdatePlayerInfo;
            _pool = new CustomPool<Player>(this);
        }
        
        #region PoolSettings

        public void TakeObject(Player objPool)
        {
            OnUpdatePlayerInfo?.Invoke(objPool);
            objPool.gameObject.SetActive(true);
        }

        public Player CreateObject()
        {
            GameObject go = OnCreateInstance?.Invoke();

            if (go == null) return null;

            var newObject = go.GetComponent<Player>();

            newObject.gameObject.SetActive(false);
            return newObject;
        }

        public void ReleaseObject(Player objToReturn)
        {
            objToReturn.gameObject.SetActive(false);
        }

        #endregion
    }
}