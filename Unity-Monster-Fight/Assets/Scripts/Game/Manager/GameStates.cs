using System;

namespace Game.Manager
{
    [Serializable]
    public enum GameStates
    {
        /// <summary>
        /// Initial setup.
        /// </summary>
        Init,

        /// <summary>
        /// Prepare the round before start
        /// </summary>
        PrepareGame,

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
