using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu (menuName = "LonecraftGames/Events/UI/LanguageChangedEvent")]
    public class LanguageChangedEvent : EventChannel<Enums.Language>
    {
      // this class is empty because it only needs to inherit from EventChannel.
    }
}
