using System;
using Game.Generic;
using UnityEngine.Events;

namespace Game.Player
{
    public class PlayerPool : ICustomObjectPool<Player>
    {
        private ICustomPool<Player> _pool;

        private Func<Player> OnCreateInstance;
        private UnityAction<Player> OnUpdatePlayerInfo;

        public PlayerPool(Func<Player> onCreateInstance, UnityAction<Player> onUpdatePlayerInfo)
        {
            OnCreateInstance = onCreateInstance;
            OnUpdatePlayerInfo = onUpdatePlayerInfo;
            _pool = new CustomPool<Player>(this);
        }
        public Player Get()
        {
            return _pool.Get();
        }

        public void Release(Player objToReturn)
        {
            _pool.Return(objToReturn);
        }
        
        #region PoolSettings

        public void TakeObject(Player objPool)
        {
            OnUpdatePlayerInfo?.Invoke(objPool);
            objPool.gameObject.SetActive(true);
        }

        public Player CreateObject()
        {
            var newObject = OnCreateInstance?.Invoke();

            if (newObject == null) return null;

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