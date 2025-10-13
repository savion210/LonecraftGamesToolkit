using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(menuName = "LonecraftGames/Events/UI/PopupEnumEvent")]
    public class PopupEnumEvent : EventChannel<Enums.UIPopupType>
    {
        // This class is empty because it only needs to inherit from EventChannel.
    }
}