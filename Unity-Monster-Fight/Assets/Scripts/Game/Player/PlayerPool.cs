using System;
using Game.Generic;
using UnityEngine.Events;

namespace Game.Player
{
    /// <summary>
    /// PlayerGenerator has responsibility to control of the players pool.
    /// </summary>
    public class PlayerPool : ICustomObjectPool<Player>
    {
        private ICustomPool<Player> _pool;

        private Func<Player> OnCreateInstance;
        private UnityAction<Player> OnUpdatePlayerInfo;
        private UnityAction OnWillRelease;
        private UnityAction OnReleased;
        private UnityAction OnActivatedObjets;
        private int _activatedObj = 0;

        public PlayerPool(Func<Player> onCreateInstance, UnityAction<Player> onUpdatePlayerInfo, UnityAction onReleased, 
            UnityAction onWillRelease, UnityAction onActivatedObjets)
        {
            OnCreateInstance = onCreateInstance;
            OnUpdatePlayerInfo = onUpdatePlayerInfo;
            OnWillRelease = onWillRelease;
            OnReleased = onReleased;
            OnActivatedObjets = onActivatedObjets;
            
            _activatedObj = 0;
            _pool = new CustomPool<Player>(this);
        }
        public void Get()
        {
            _pool.Get();
            _activatedObj++;
            OnActivatedObjets?.Invoke();
        }

        public void Release(Player objToReturn)
        {
            _pool.Return(objToReturn);
            _activatedObj--;
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

        public int ActivatedObj()
        {
            return _activatedObj;
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