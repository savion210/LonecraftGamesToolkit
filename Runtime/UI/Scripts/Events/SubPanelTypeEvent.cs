using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.UI;
using UI;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(menuName = "LonecraftGames/Events/UI/SubPanelTypeEvent")]
    public class SubPanelTypeEvent : EventChannel<UISubPanel>
    {
        // This class is empty because it only needs to inherit from EventChannel.
    }
}