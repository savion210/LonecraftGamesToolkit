using EventSystem;
using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;
using UnityEngine.UI;

namespace LonecraftGames.Toolkit.UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LonecraftGames/UI/UIPanelButton")]
    public class UIPanelButton : MonoBehaviour
    {
        [Tooltip("Event channel to raise when the button is clicked.")]
        [Header("Event Channels")] [SerializeField]
        private PanelEnumEvent onPanelChange;

        [Tooltip("The type of panel to switch to when the button is clicked.")]
        [Header("Panel Type")] [SerializeField]
        private Enums.UIPanelType panelType;

        [Tooltip("The button component to attach the click event to.")]
        [Header("Button")] [SerializeField] private Button button;

        void Start()
        {
            if (button == null)
                button = GetComponent<Button>();

            button.onClick.AddListener(OnButtonClick);
        }

        /// <summary>
        ///   Handles the button click event to change the UI panel.
        /// </summary>
        private void OnButtonClick()
        {
            onPanelChange.Raise(GameStateManager.Instance.CurrentGameState == Enums.GameState.Pause
                ? Enums.UIPanelType.Pause
                : panelType);
        }
    }
}