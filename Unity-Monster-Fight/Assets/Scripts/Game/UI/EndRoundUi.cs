using System;
using Game.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndRoundUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textRound;
    [SerializeField] private TextMeshProUGUI _textInfoRound;
    [SerializeField] private Button _buttonNextRound;
    [SerializeField] private Button _buttonExit;

    public Action OnNextRound = delegate { };
    public Action OnExitGame = delegate { };
    private void Start()
    {
        gameObject.SetActive(false);
        _buttonNextRound.onClick.AddListener(NextRound);
        _buttonExit.onClick.AddListener(ExitGame);
    }

    public void StartEndRound(int round, int totalPlayers, float timer)
    {
        _textRound.text = $"Round {round}";
        _textInfoRound.text = $"Total time {StringFormat.TimerToString(timer)} \nTotal Monster {totalPlayers}";

        gameObject.SetActive(true);        
    }

    private void NextRound()
    {
        gameObject.SetActive(false);
        OnNextRound?.Invoke();
    }

    private void ExitGame()
    {
        OnExitGame?.Invoke();
    }
}
