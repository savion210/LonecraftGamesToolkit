namespace LonecraftGames.Toolkit.Gameplay.StateMachine
{
    public abstract class State
    {
        /// <summary>
        ///     Enters the state. This method is called when the state is first entered.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        ///     Updates the state. This method is called every frame while the state is active.
        /// </summary>
        /// <param name="deltaTime"></param>
        public abstract void OnUpdate(float deltaTime);

        /// <summary>
        ///   Fixed updates the state. This method is called at a fixed interval while the state is active.
        /// </summary>
        /// <param name="fixedDeltaTime"> The fixed time step since the last call.</param>
        public abstract void OnFixedUpdate(float fixedDeltaTime);

        /// <summary>
        ///     Late updates the state. This method is called after all Update methods have been called.
        /// </summary>
        /// <param name="deltaTime"> The time elapsed since the last frame.</param>
        public abstract void OnLateUpdate(float deltaTime);

        /// <summary>
        ///     Exits the state. This method is called when the state is no longer active.
        /// </summary>
        public abstract void Exit();
    }
}