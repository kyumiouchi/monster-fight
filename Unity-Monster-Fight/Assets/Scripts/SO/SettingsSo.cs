using System;
using UnityEngine;

[Serializable]
public enum Settings
{
    NoLoop,
    CoroutineAndLoop,
}

[CreateAssetMenu(menuName = "ScriptableObject/Data/Settings Data", fileName = "Settings_SO")]
public class SettingsSo : ScriptableObject
{
    [SerializeField] private Settings _typeInstanciate = Settings.NoLoop;

    public Settings TypeInstanciate => _typeInstanciate;
}