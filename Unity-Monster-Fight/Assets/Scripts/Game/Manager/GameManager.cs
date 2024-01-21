using System;
using UnityEngine;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        private void Awake()
        {
            _instance = this;
        }

        
        [SerializeField] private PrepareGameManager _prepareGameManager = null;
        /// <summary>
        /// MainMenu component on the main menu GameObject.
        /// </summary>
        [SerializeField] private RoundManager _roundManager = null;
        [SerializeField] private EndRoundManager _endRoundManager = null;
        [SerializeField] private InitializeRoundManager _initializeRoundManager = null;


        /// <summary>
        /// The current state of the game.
        /// </summary>
        [SerializeField] private GameStates _state = GameStates.Init;

        public static event Action<GameStates> OnGameStateChanged;
        
        private void Start()
        {
            State = GameStates.InitializeRound;
            _initializeRoundManager.OnChargedRound += OnChargedRound;
            _prepareGameManager.OnStartRound += OnStartRound;
            _roundManager.OnEndRound += OnRoundComplete;
            _endRoundManager.OnNextRound += OnNextRound;
        }
        
        private GameStates State
        {
            get => _state;
            set
            {
                // Cannot return to init.
                if (value == GameStates.Init)
                {
                    throw new ArgumentException("Cannot return to init state.");
                }

                _state = value;
                
                switch (_state)
                {
                    case GameStates.InitializeRound:
                        _initializeRoundManager.InitializeRound();
                        _roundManager.InitializeRound();
                        break;
                    
                    case GameStates.PrepareRound:
                        _prepareGameManager.StartPrepareRound();
                        break;

                    case GameStates.StartRound:
                        _roundManager.StartRound();
                        break;

                    case GameStates.EndRound:
                        _endRoundManager.EndRound();
                        break;

                    case GameStates.Init:
                    // Intentional fallthrough. Init isn't supported but will be explicitly handled above.
                    default:
                        throw new ArgumentOutOfRangeException($"State machine doesn't support state {_state}.");
                }
                
                OnGameStateChanged?.Invoke(_state);
            }
        }

        /// <summary>
        /// Triggered when the round is read to start.
        /// </summary>
        private void OnChargedRound()
        {
            State = GameStates.PrepareRound;
        }

        /// <summary>
        /// Triggered when the round start.
        /// </summary>
        private void OnStartRound()
        {
            State = GameStates.StartRound;
        }

        /// <summary>
        /// Triggered when the player completes a round.
        /// </summary>
        private void OnRoundComplete()
        {
            State = GameStates.EndRound;
        }
        
        /// <summary>
        /// Triggered when the round ended.
        /// </summary>
        private void OnNextRound()
        {
            State = GameStates.InitializeRound;
        }
    }
}
