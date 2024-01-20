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
        private UnityAction OnWillRelease;
        private UnityAction OnReleased;

        public PlayerPool(Func<Player> onCreateInstance, UnityAction<Player> onUpdatePlayerInfo, UnityAction onReleased, UnityAction onWillRelease)
        {
            OnCreateInstance = onCreateInstance;
            OnUpdatePlayerInfo = onUpdatePlayerInfo;
            OnWillRelease = onWillRelease;
            OnReleased = onReleased;
            _pool = new CustomPool<Player>(this);
        }
        public void Get()
        {
            _pool.Get();
        }

        public void Release(Player objToReturn)
        {
            _pool.Return(objToReturn);
            OnReleased?.Invoke();
        }

        public int Count()
        {
            return _pool.Count();
        }

        public void CanRelease()
        {
            OnWillRelease?.Invoke();
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