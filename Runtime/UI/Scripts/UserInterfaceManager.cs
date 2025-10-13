using System.Collections.Generic;
using EventSystem;
using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.UI
{
    /// <summary>
    /// Manages the user interface panels in the game.
    /// </summary>
    [AddComponentMenu("LonecraftGames/UI/User Interface Manager")]
    public class UserInterfaceManager : MonoBehaviour
    {
        [Header("Current Panel/ Previous Panel")] [SerializeField]
        private Enums.UIPanelType currentPanel;

        [SerializeField] private Enums.UIPanelType previousPanel;

        [Header("Current SubPanel/ Previous SubPanel")] [SerializeField]
        private Enums.SubPanelType currentSubPanel;

        [SerializeField] private Enums.SubPanelType previousSubPanel;

        [Header("Current Popup/ Previous Popup")] [SerializeField]
        private Enums.UIPopupType currentPopup;

        [SerializeField] private Enums.UIPopupType previousPopup;

        [Header("Panel Event Channels")] [SerializeField]
        private PanelTypeEvent onRegisterPanel;

        [SerializeField] private PanelEnumEvent onPanelChange;

        [Header("SubPanel Event Channels")] [SerializeField]
        private SubPanelTypeEvent onRegisterSubPanel;

        [SerializeField] private SubPanelEnumEvent onSubPanelChange;

        [Header("Popup Event Channels")] [SerializeField]
        private PopupTypeEvent onRegisterPopup;

        [SerializeField] private PopupEnumEvent onPopupChange;


        [Header("UI Panels/Popups/Subpanels")] [SerializeField]
        private List<UIPanel> panels = new List<UIPanel>();

        [SerializeField] private List<UISubPanel> subPanels = new List<UISubPanel>();

        [SerializeField] private List<UIPanelPopup> popups = new List<UIPanelPopup>();

        #region Unity

        private void Awake()
        {
            // Register the panel events
            onRegisterPanel.RegisterListener(OnRegisterPanel);
            onPanelChange.RegisterListener(OnPanelChange);

            // Register the subpanel events
            onRegisterSubPanel.RegisterListener(OnRegisterSubPanel);
            onSubPanelChange.RegisterListener(OnSubPanelChange);

            // Register the popup events
            onRegisterPopup.RegisterListener(OnRegisterPopup);
            onPopupChange.RegisterListener(OnPopupChange);

            if (SceneLoader.GetCurrentActiveScene() == SceneLoader.Scene.GameUI.ToString())
                onPanelChange.Raise(Enums.UIPanelType.MainMenu);
        }


        private void OnDestroy()
        {
            // Unregister the panel events
            onRegisterPanel.UnregisterListener(OnRegisterPanel);
            onPanelChange.UnregisterListener(OnPanelChange);

            // Unregister the subpanel events
            onRegisterSubPanel.UnregisterListener(OnRegisterSubPanel);
            onSubPanelChange.UnregisterListener(OnSubPanelChange);

            // Unregister the popup events
            onRegisterPopup.UnregisterListener(OnRegisterPopup);
            onPopupChange.UnregisterListener(OnPopupChange);
        }

        #endregion

        #region Event Handlers

        #region Panel

        /// <summary>
        /// Handles the event of changing the active panel.
        /// </summary>
        /// <param name="obj">The type of the new active panel.</param>
        private void OnPanelChange(Enums.UIPanelType obj)
        {
            previousPanel = currentPanel; // Set the previous panel to the current panel

            foreach (var panel in panels)
            {
                if (panel.panelType == obj)
                {
                    currentPanel = obj; // Set the current panel to the new panel
                    DisableAllSubPanels();
                    panel.Show();
                    CheckEnteringSettingsPanel(obj);
                }
                else
                {
                    panel.Hide();
                }
            }
        }

        /// <summary>
        /// Handles the event of registering a new panel.
        /// </summary>
        /// <param name="panel">The new panel to register.</param>
        private void OnRegisterPanel(UIPanel panel)
        {
            panels.Add(panel);
        }

        #endregion

        #region SubPanel

        /// <summary>
        ///  Handles the event of registering a new subpanel.
        /// </summary>
        /// <param name="subPanel">The new sub panel to register.</param>
        private void OnRegisterSubPanel(UISubPanel subPanel)
        {
            subPanels.Add(subPanel);
        }

        private void OnSubPanelChange(Enums.SubPanelType subPanelType)
        {
            previousSubPanel = currentSubPanel; // Set the previous subpanel to the current subpanel

            foreach (var subPanel in subPanels)
            {
                if (subPanel.subPanelType == subPanelType &&
                    currentPanel ==
                    Enums.UIPanelType.Settings) //TODO: Add more dynamic way to handle sub panels conditions
                {
                    currentSubPanel = subPanelType; // Set the current subpanel to the new subpanel
                    subPanel.Show();
                }
                else
                {
                    if (subPanelType == Enums.SubPanelType.None)
                        currentSubPanel = Enums.SubPanelType.None;

                    subPanel.Hide();
                }
            }
        }


        private void DisableAllSubPanels()
        {
            if (currentSubPanel == Enums.SubPanelType.None) return;

            foreach (var subPanel in subPanels)
            {
                subPanel.Hide();
                currentSubPanel = Enums.SubPanelType.None; // Set the current subpanel to none
            }
        }

        private void CheckEnteringSettingsPanel(Enums.UIPanelType obj)
        {
            if (obj == Enums.UIPanelType.Settings)
            {
                onSubPanelChange.Raise(Enums.SubPanelType.GeneralSettings);
                currentSubPanel = Enums.SubPanelType.GeneralSettings;
            }
        }

        #endregion

        #region Popup

        private void OnPopupChange(Enums.UIPopupType obj)
        {
            previousPopup = currentPopup; // Set the previous popup to the current popup

            foreach (var popup in popups)
            {
                if (popup.popupType == obj)
                {
                    currentPopup = obj; // Set the current popup to the new popup
                    popup.Show();
                }
                else
                {
                    if (obj == Enums.UIPopupType.None)
                        currentPopup = Enums.UIPopupType.None;

                    popup.Hide();
                }
            }
        }


        private void OnRegisterPopup(UIPanelPopup popup)
        {
            popups.Add(popup);
        }

        #endregion

        #region Getters

        /// <summary>
        ///  Gets the panel of the specified type.
        /// </summary>
        /// <param name="panelType">The type of the panel to get.</param>
        /// <returns> the panel of the specified type.</returns>
        public UIPanel GetPanel(Enums.UIPanelType panelType)
        {
            return panels.Find(panel => panel.panelType == panelType);
        }

        /// <summary>
        ///  Gets the subpanel of the specified type.
        /// </summary>
        /// <param name="subPanelType">The type of the subpanel to get.</param>
        /// <returns> the subpanel of the specified type.</returns>
        public UISubPanel GetSubPanel(Enums.SubPanelType subPanelType)
        {
            return subPanels.Find(subPanel => subPanel.subPanelType == subPanelType);
        }

        /// <summary>
        ///  Gets the popup of the specified type.
        /// </summary>
        /// <param name="popupType">The type of the popup to get.</param>
        /// <returns> the popup of the specified type.</returns>
        public UIPanelPopup GetPopup(Enums.UIPopupType popupType)
        {
            return popups.Find(popup => popup.popupType == popupType);
        }

        #endregion

        #endregion
    }
}