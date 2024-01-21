using System;

namespace Game.Manager
{
    /// <summary>
    /// GameStates has all game and round states.
    /// </summary>
    [Serializable]
    public enum GameStates
    {
        /// <summary>
        /// Initial setup.
        /// </summary>
        Init,

        /// <summary>
        /// Initialize the round data
        /// </summary>
        InitializeRound,

        /// <summary>
        /// Prepare the round before start
        /// </summary>
        PrepareRound,

        /// <summary>
        /// Start Round
        /// </summary>
        StartRound,

        /// <summary>
        /// Show the end of the round
        /// </summary>
        EndRound,
        
    }
}
