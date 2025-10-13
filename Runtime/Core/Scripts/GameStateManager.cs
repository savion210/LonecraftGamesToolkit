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
        [Header("Event Channels")] [SerializeField]
        private GameStateEvent gameStateEvent;

        [SerializeField] private Enums.GameState currentGameState;
        public Enums.GameState CurrentGameState => currentGameState;

        private void Start()
        {
            DontDestroyOnLoad(this);
            gameStateEvent.RegisterListener(SetGameState);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private void OnDestroy()
        {
            gameStateEvent.UnregisterListener(SetGameState);
        }

        public void Pause()
        {
            SetGameState(Enums.GameState.Pause);
            gameStateEvent.Raise(currentGameState);
        }

        public void GameOver()
        {
            SetGameState(Enums.GameState.GameOver);
            gameStateEvent.Raise(currentGameState);
        }

        private void SetGameState(Enums.GameState state)
        {
            currentGameState = state;
        }
    }
}