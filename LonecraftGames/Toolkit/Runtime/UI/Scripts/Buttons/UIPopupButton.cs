using EventSystem;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LonecraftGames/UI/UIPopupButton")]
    public class UIPopupButton : MonoBehaviour
    {
        [Tooltip("Event channel to raise when the button is clicked.")] [Header("Event Channels")] [SerializeField]
        private PopupEnumEvent onPopupChange;

        [Tooltip("The type of panel to switch to when the button is clicked.")] [Header("Panel Type")] [SerializeField]
        private Enums.UIPopupType panelType;

        [Tooltip("The button component to attach the click event to.")] [Header("Button")] [SerializeField]
        private Button button;

        void Start()
        {
            if (button == null)
                button = GetComponent<Button>();

            button.onClick.AddListener(OnButtonClick);
        }

        /// <summary>
        ///   Handles the button click event to change the UI popup.
        /// </summary>
        private void OnButtonClick()
        {
            onPopupChange.Raise(panelType);
        }
    }
}