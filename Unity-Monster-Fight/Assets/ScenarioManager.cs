using Game.Manager;
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

        private void GameStateChanged(GameStates states)
        {
            if (states == GameStates.PrepareGame)
                _backgroundRenderer.sprite = _backgroundSo[_roundsSo.CurrentRound % _backgroundSo.Count];
        }
    }
}
