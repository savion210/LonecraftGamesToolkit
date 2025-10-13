using System.Collections.Generic;
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.Interaction
{
    [AddComponentMenu("LonecraftGames/Interaction/PlayerInteract")]
    public class PlayerInteract : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private float interactRange = 2f;
        public bool currentlyInteracting = false;

        private IInteractable _currentInteractable;
        private readonly Collider[] _colliders = new Collider[10]; // Pre-allocated array


        #region Functions

        /// <summary>
        ///  Handles player interaction with interactable objects.
        /// </summary>
        public void Interaction()
        {
            // Deselect the current one, no matter what
            if (_currentInteractable != null)
            {
                _currentInteractable.Deselect();
                _currentInteractable = null;
            }

            // Attempt to select a new interactable in range
            IInteractable interactable = GetInteractable();

            if (interactable != null)
            {
                interactable.Interact(transform);
                _currentInteractable = interactable;
            }
        }


        /// <summary>
        ///  Deselects the current interactable object if it exists.
        /// </summary>
        public void DeselectCurrentInteractable()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.Deselect();
                _currentInteractable = null;
            }
        }

        /// <summary>
        ///  Finds the closest interactable object within the specified range.
        /// </summary>
        /// <returns> The closest interactable object, or null if none found.</returns>
        public IInteractable GetInteractable()
        {
            List<IInteractable> interactablesList = new List<IInteractable>();

            int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, interactRange, _colliders);
            for (int i = 0; i < colliderCount; i++)
            {
                if (_colliders[i].TryGetComponent(out IInteractable interactable))
                {
                    interactablesList.Add(interactable);
                }
            }

            IInteractable closestInteractable = null;

            foreach (IInteractable interactables in interactablesList)
            {
                if (closestInteractable == null)
                {
                    closestInteractable = interactables;
                }
                else
                {
                    if (Vector3.Distance(transform.position, interactables.GetTransform().position)
                        < Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                    {
                        closestInteractable = interactables;
                    }
                }
            }

            return closestInteractable;
        }

        #endregion
        
    }
}