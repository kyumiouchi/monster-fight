using UnityEngine;

namespace Game.Manager
{    
    /// <summary>
    /// ScenarioManager controls the environment behavior.
    /// </summary>
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
            if (states == GameStates.InitializeRound)
                _backgroundRenderer.sprite = _backgroundSo[_roundsSo.CurrentRound % _backgroundSo.Count];
        }
    }
}
