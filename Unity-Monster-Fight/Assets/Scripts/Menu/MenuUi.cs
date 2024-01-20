using UnityEngine;
using UnityEngine.UI;
using Game.Utils;

namespace Game.Menu
{
    public class MenuUi : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;

        #region Callback
        private void OnEnable()
        {
            _buttonStart.onClick.AddListener(ClickStartButton);
        }

        private void OnDisable()
        {
            _buttonStart.onClick.RemoveListener(ClickStartButton);
        }
        #endregion

        private void ClickStartButton()
        {
            Loader.Load(Loader.Scene.SCN_Game);
        }
    }
}
