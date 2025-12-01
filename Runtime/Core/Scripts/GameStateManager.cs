using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core
{
    /// <summary>
    /// Local offline Game Manager for handling game states for the player
    /// </summary>
    public class GameStateManager : Singleton<GameStateManager>
    {
        [Header("Event Channels")]
        [SerializeField]
        private GameStateEvent gameStateEvent;

        [Header("Runtime State")]
        [SerializeField]
        private Enums.GameState currentGameState;

        public Enums.GameState CurrentGameState => currentGameState;

        [Header("Settings")]
        [Tooltip("Whether to use Time.timeScale to pause the game.")]
        public bool useTimeScale = true;

        [Tooltip("Whether to control the mouse cursor state based on game state.")]
        public bool controlMouseState = true;

        private void Start()
        {
            gameStateEvent.RegisterListener(SetGameState);

            SetGameState(Enums.GameState.MainMenu);
            gameStateEvent.Raise(currentGameState);
        }

        private void OnDestroy()
        {
            if (gameStateEvent != null)
            {
                gameStateEvent.UnregisterListener(SetGameState);
            }
        }

        /// <summary>
        /// Updates the current state and manages related global properties (like time).
        /// </summary>
        /// <param name="state">The new game state.</param>
        private void SetGameState(Enums.GameState state)
        {
            currentGameState = state;

            if (controlMouseState)
                HandleMouseState(state);

            if (useTimeScale)
            {
                // Handle Time Scale (Global Game Speed)
                if (state == Enums.GameState.Pause)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }
        }

        /// <summary>
        /// Controls the cursor lock state and visibility based on the current game state.
        /// </summary>
        /// <param name="state">The current game state.</param>
        private void HandleMouseState(Enums.GameState state)
        {
            switch (state)
            {
                case Enums.GameState.MainMenu:
                case Enums.GameState.Pause:
                case Enums.GameState.GameOver:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;

                case Enums.GameState.Game:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;

                default:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }
        }
    }
}