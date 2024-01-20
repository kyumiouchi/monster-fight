using System;
using Game.Generic;
using Game.Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{

    public class GameManager : MonoBehaviourSingletonPersistence<GameManager>
    {
        [Serializable]
        public enum GameStates
        {
            /// <summary>
            /// Initial setup.
            /// </summary>
            Init,

            /// <summary>
            /// Player is in the main menu.
            /// </summary>
            PrepareGame,

            /// <summary>
            /// Player is in the difficulty menu.
            /// </summary>
            StartRound,

            /// <summary>
            /// Player is playing the game.
            /// </summary>
            EndRound,

            /// <summary>
            /// Player is playing the game.
            /// </summary>
            Restart,
        }

        [FormerlySerializedAs("prepareGameManager")] [FormerlySerializedAs("_prepareGame")] [SerializeField] private PrepareGameManager _prepareGameManager = null;
        /// <summary>
        /// MainMenu component on the main menu GameObject.
        /// </summary>
        [SerializeField] private RoundManager _roundManager = null;


        /// <summary>
        /// The current state of the game.
        /// </summary>
        [SerializeField] private GameStates state = GameStates.Init;

        public static event Action<GameStates> OnGameStateChanged;
        
        private void Start()
        {
            State = GameStates.PrepareGame;
            _prepareGameManager.OnStartRound += StartRound;
        }
        
        private GameStates State
        {

            get => state;

            set
            {
                // Cannot return to init.
                if (value == GameStates.Init)
                {
                    throw new ArgumentException("Cannot return to init state.");
                }

                state = value;
                

                switch (state)
                {
                    case GameStates.PrepareGame:
                        _prepareGameManager.StartPrepareGame();
                        _roundManager.PrepareRound();
                        break;

                    case GameStates.StartRound:
                        _roundManager.StartRound();
                        break;

                    case GameStates.EndRound:
                        break;

                    case GameStates.Restart:
                        break;

                    case GameStates.Init:
                    // Intentional fallthrough. Init isn't supported but will be explicitly handled above.
                    default:
                        throw new ArgumentOutOfRangeException($"State machine doesn't support state {state}.");
                }
                
                OnGameStateChanged?.Invoke(state);
            }
        }

        public void StartRound()
        {
            State = GameStates.StartRound;
        }

        /// <summary>
        /// Triggered when the player completes a game.
        /// </summary>
        private void OnRoundComplete()
        {
            State = GameStates.EndRound;
        }
    }
}