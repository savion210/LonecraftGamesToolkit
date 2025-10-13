using LonecraftGames.Toolkit.Core.Events;
using TMPro;
using UnityEngine;

namespace LonecraftGames.Toolkit.UI
{
    [CreateAssetMenu(menuName = "LonecraftGames/Events/UI/FontChangedEvent")]
    public class FontChangedEvent : EventChannel<TMP_FontAsset>
    {
        // This class is empty because it only needs to inherit from EventChannel.
        
    }
}