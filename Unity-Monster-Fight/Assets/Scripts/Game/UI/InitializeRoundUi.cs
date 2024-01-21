using System;
using Game.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Ui
{
    public class InitializeRoundUi : MonoBehaviour
    {
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private TextMeshProUGUI _textRound;
        [SerializeField] private TextMeshProUGUI _textTimer;
        
        private int _totalPlayers = 0;        
        private float _currentTimer = 0;
        private bool _canStartTimer = false;
        
        public UnityAction<float> OnLoadingFinish = delegate { };

        private void Start()
        {
            if (!_canStartTimer) gameObject.SetActive(false);
        }

        public void StartLoading(int round, int totalPlayers)
        {
            _textRound.text = $"Round {round}";
            _currentTimer = 0;
            _canStartTimer = true;
            _loadingSlider.value = 0;
            _loadingSlider.maxValue = totalPlayers;
            _totalPlayers = totalPlayers;
            gameObject.SetActive(true);
        }

        public void UpdateLoading(int currentSpawnedPlayers)
        {
            _loadingSlider.value = currentSpawnedPlayers;

            if (_totalPlayers == currentSpawnedPlayers)
            {
                _canStartTimer = false;
                gameObject.SetActive(false);
                OnLoadingFinish?.Invoke(_currentTimer);
            }
        }

        private void Update()
        {
            if(!_canStartTimer) return;
            _currentTimer += Time.deltaTime;
            _textTimer.text = $"Timer {StringFormat.TimerToString(_currentTimer)}";
        }
    }
}