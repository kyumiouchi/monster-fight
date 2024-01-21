using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] private TilesSo _backgroundSo;
        [SerializeField] private SpriteRenderer _backgroundRenderer;
        [SerializeField] private RoundsSo _roundsSo;
        
        #region Callback
        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameStateChanged;
        }
        #endregion

        private void GameStateChanged(GameManager.GameStates states)
        {
            if (states == GameManager.GameStates.PrepareGame)
                _backgroundRenderer.sprite = _backgroundSo[_roundsSo.CurrentRound % _backgroundSo.Count];
        }
    }
}
