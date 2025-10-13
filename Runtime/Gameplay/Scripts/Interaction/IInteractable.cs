using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.Interaction
{
    public interface IInteractable
    {
        void Interact(Transform interactTransfrom);
        void Deselect();
        string GetInterActText();
        Transform GetTransform();
        
        /// <summary>
        ///  Y offset for UI placement above the interactable object.
        /// </summary>
        float Yoffset { get;  }
    }
}