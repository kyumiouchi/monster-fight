using Game.Player;
using Game.Ui;
using UnityEngine;

namespace Game.Manager
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] private RoundUi _roundUi;
        [SerializeField] private PlayerGenerator _playerGenerator;
    
        [SerializeField] private RoundsSo _roundsSo;

        public void PrepareRound()
        {
            _playerGenerator.PreparePlayers(_roundsSo.NumberPlayers);
        }
        public void StartRound()
        {
            _roundUi.StartUi(_roundsSo);
        }
    }
}