using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        public State CurrentState => _currentState;
        private State _currentState;
    
        /// <summary>
        ///  Switches the current state to a new state.
        /// </summary>
        /// <param name="newState"> The new state to switch to.</param>
        public void SwitchState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
        private void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (_currentState != null)
            {
                _currentState.OnLateUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (_currentState != null)
            {
                _currentState.OnFixedUpdate(Time.fixedDeltaTime);
            }
        }
    }
}