using System;
using TMPro;
using UnityEngine;

namespace Game.Ui
{
    public class RoundUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textRound;
        [SerializeField] private TextMeshProUGUI _textTimer;
        
        public Action uiComplete = delegate { };
        private float _currentTimer;
        
        private bool _canStartTimer = false;

        private void Start()
        {
            gameObject.SetActive(false);
            Reset();
        }
        
        private void Update()
        {
            if(!_canStartTimer) return;
            _currentTimer += Time.deltaTime;
            _textTimer.text = $"Timer {TimeSpan.FromSeconds(_currentTimer):ss\\.ff}";
        }
        
        public void StartUi(RoundsSo roundsSo)
        {
            _textRound.text = $"Round {roundsSo.CurrentRound}";
            _textRound.enabled = true;
            _textTimer.enabled = true;
            gameObject.SetActive(true);
            _canStartTimer = true;
        }
        
        private void Reset()
        {
            _textRound.enabled = false;
            _textTimer.enabled = false;
            _currentTimer = 0;
        }
    }
}
