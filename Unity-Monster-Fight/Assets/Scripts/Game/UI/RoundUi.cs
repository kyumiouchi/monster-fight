using System;
using Game.Utils;
using TMPro;
using UnityEngine;

namespace Game.Ui
{
    public class RoundUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textRound;
        [SerializeField] private TextMeshProUGUI _textTimer;
        [SerializeField] private TextMeshProUGUI _textMonsters;
        
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
            _textTimer.text = $"Timer {StringFormat.TimerToString(_currentTimer)}";
        }
        
        public void StartUi(int totalMonsters, int currentRound)
        {
            _textMonsters.text = $"Total {totalMonsters}";
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

        public float EndRound()
        {
            _canStartTimer = false;
            float timerRound = _currentTimer;
            Clear();
            return timerRound; 
        }
    }
}
