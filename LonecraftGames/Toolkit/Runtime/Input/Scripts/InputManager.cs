using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LonecraftGames.Toolkit.Input
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LonecraftGames/Input/Input Manager")]
    public class InputManager : MonoBehaviour, InputSystem_Actions.IPlayerActions
    {
        public event Action InteractEvent;
        public event Action DeselectEvent;
        public event Action JumpEvent;
        public event Action ProneEvent;
        public event Action CrouchEvent;
        public event Action<bool> RadialMenuEvent;

        [HideInInspector] public bool IsSprinting;
        public Vector2 MovementValue { get; private set; }
        public bool IsInitialized => _isInitialized;

        private bool _isInitialized;
        private InputSystem_Actions _controls;


        private void OnDestroy()
        {
            UnInit();
        }

        public void Init()
        {
            _controls = new InputSystem_Actions();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
            _isInitialized = true;
        }

        public void UnInit()
        {
            if (_controls != null)
            {
                _controls.Player.Disable();
                _controls.Player.SetCallbacks(this);
                _controls.Dispose();
                _controls = null;
            }

            _isInitialized = false;
        }

        #region Input Callbacks

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
                {
                    ProneEvent?.Invoke();
                }
                else
                {
                    CrouchEvent?.Invoke();
                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            JumpEvent?.Invoke();
        }


        public void OnSprint(InputAction.CallbackContext context)
        {
            IsSprinting = context.ReadValueAsButton();
        }

        public void OnDeselect(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            DeselectEvent?.Invoke();
        }

        public void OnRadialWheel(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                RadialMenuEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                RadialMenuEvent?.Invoke(false);
            }
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
        }

        public void OnNext(InputAction.CallbackContext context)
        {
        }

        #endregion

        #region Helper Functions
        public string GetInteractText()
        {
            return $"Press {GetKeyBinding("JTAC/Interact")} to interact";
        }

        private string GetKeyBinding(string actionName)
        {
            if (_controls == null)
                return string.Empty;

            var action = _controls.FindAction(actionName);
            if (action == null)
                return string.Empty;

            // Return the display string of the first keyboard binding
            foreach (var binding in action.bindings)
            {
                if (binding.isPartOfComposite || binding.path.Contains("Keyboard"))
                    return binding.ToDisplayString();
            }

            return string.Empty;
        }
        #endregion
    }
}