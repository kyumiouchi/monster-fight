using UnityEngine;
using UnityEngine.UI;
using Game.Utils;

namespace Game.Menu
{
    /// <summary>
    /// MenuUi controls the Menu Ui behavior.
    /// </summary>
    public class MenuUi : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonExit;

        private void Start()
        {
            _buttonStart.onClick.AddListener(ClickStartButton);
            _buttonExit.onClick.AddListener(ExitGame);
        }

        private void ClickStartButton()
        {
            Loader.Load(Loader.Scene.SCN_Game);
        }
        
        private void ExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
    }
}
