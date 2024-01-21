using System;
using TMPro;
using UnityEngine;

namespace Game.Ui
{
    public class PrepareRoundUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textInstruction;
        [SerializeField] private TextMeshProUGUI _textCount;
        [SerializeField] private TextMeshProUGUI _textRound;

        [SerializeField] private float _timeIntervalCount = 2f;

        private readonly string[] _counterValues = { "3", "2", "1", "GO!" };

        private int _currentCounter = 0;
        private float _currentTimer = 0;
        private bool _canStartCounter = false;
        private bool _canStartMouseClick = false;

        public Action UiComplete = delegate { };

        private void Start()
        {
            Clear();
        }

        private void Update()
        {
            TapOnScreen();
            UpdateCounter();
        }

        public void StartUi(RoundsSo roundsSo)
        {
            _textInstruction.enabled = true;
            _textRound.enabled = true;
            _textRound.text = $"Round {roundsSo.CurrentRound}";
            gameObject.SetActive(true);
            _canStartMouseClick = true;
        }

        private void TapOnScreen()
        {
            if (!_canStartMouseClick) return;

            if (Input.GetMouseButtonDown(0))
            {
                StartCounter();
            }
        }

        private void StartCounter()
        {
            _canStartMouseClick = false;
            _textInstruction.enabled = false;
            _textCount.enabled = true;
            _textCount.text = _counterValues[_currentCounter++];

            _canStartCounter = true;
        }

        private void UpdateCounter()
        {
            if (!_canStartCounter) return;

            _currentTimer += Time.deltaTime;
            if (_currentTimer >= _timeIntervalCount)
            {
                if (_currentCounter == _counterValues.Length)
                {
                    Clear();
                    UiComplete?.Invoke();
                    return;
                }
                _textCount.text = _counterValues[_currentCounter++];
                _currentTimer = 0;
            }
        }

        private void Clear()
        {
            gameObject.SetActive(false);
            _textInstruction.enabled = false;
            _textCount.enabled = false;
            _textRound.enabled = false;
            
            _currentTimer = 0;
            _currentCounter = 0;
            _canStartMouseClick = false;
            _canStartCounter = false;
        }
    }
}
