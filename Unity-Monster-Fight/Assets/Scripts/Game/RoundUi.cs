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
            Clear();
        }
        
        private void Update()
        {
            if(!_canStartTimer) return;
            _currentTimer += Time.deltaTime;
            _textTimer.text = $"Timer {TimeSpan.FromSeconds(_currentTimer):ss\\.ff}";
        }
        
        public void StartUi(int currentRound)
        {
            _textRound.text = $"Round {currentRound}";
            _textRound.enabled = true;
            _textTimer.enabled = true;
            gameObject.SetActive(true);
            _canStartTimer = true;
        }
        
        private void Clear()
        {
            gameObject.SetActive(false);
            _textRound.enabled = false;
            _textTimer.enabled = false;
            _currentTimer = 0;
        }

        public void EndUi()
        {
            _canStartTimer = false;
        }

        public void EndRound()
        {
            Clear();
        }
    }
}
