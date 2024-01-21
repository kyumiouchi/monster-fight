using MyNamespace;
using UnityEngine;

namespace Game.Manager
{
    public class SettingsManager : MonoBehaviour
    {
        private static SettingsManager _instance;
        public static SettingsManager Instance => _instance;
        
        [SerializeField] private SettingsSo _settingsSo;
        public SettingsSo SettingsSo => _settingsSo;
        
        private void Awake()
        {
            _instance = this;
        }

    }
}
