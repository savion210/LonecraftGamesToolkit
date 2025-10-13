using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu (menuName = "LonecraftGames/Events/UI/SubPanelEnumEvent")]
    public class SubPanelEnumEvent : EventChannel<Enums.SubPanelType>
    {
        // This class is empty because it only needs to inherit from EventChannel.
    }
}