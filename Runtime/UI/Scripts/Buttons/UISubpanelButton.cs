using EventSystem;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LonecraftGames/UI/UISubpanelButton")]
    public class UISubpanelButton : MonoBehaviour
    {
        [Header("Event Channels")] [SerializeField]
        private SubPanelEnumEvent onSubPanelEnumEvent;

        [Header("Panel Type")] [SerializeField]
        private Enums.SubPanelType subPanelType;

        [Header("Button")] [SerializeField] private Button button;

        void Start()
        {
            if (button == null)
                button = GetComponent<Button>();

            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            onSubPanelEnumEvent.Raise(subPanelType);
        }
    }
}