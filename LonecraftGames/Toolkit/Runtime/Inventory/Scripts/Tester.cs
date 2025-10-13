using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Gameplay.Interaction;
using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    public class Tester : MonoBehaviour, IInteractable
    {
        public EventChannel<int> onHeal;
        public ConsumableItem healthPotion;
        public void Interact(Transform interactTransfrom)
        {
         
        }

        public void Deselect()
        {
            
        }

        public string GetInterActText()
        {
            return "Test";
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public float Yoffset { get; }
    }
}