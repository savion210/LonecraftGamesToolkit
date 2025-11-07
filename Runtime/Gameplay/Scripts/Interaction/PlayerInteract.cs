using System.Collections.Generic;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.Interaction
{
    [AddComponentMenu("LonecraftGames/Interaction/PlayerInteract")]
    public class PlayerInteract : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private float interactRange = 2f;
        public bool currentlyInteracting = false;

        [Header("Filtering Settings")]
        [Tooltip("The camera transform to use for aiming and visibility checks.")]
        [SerializeField] private Transform mainCameraTransform;

        [Tooltip("How wide the 'forgiving' cone check is, in degrees.")]
        [SerializeField] private float viewAngle = 60f;

        [Header("Layer Masks")]
        [Tooltip("Layers that will block the interaction raycast (e.g., 'Walls', 'Default').")]
        [SerializeField] private LayerMask obstacleLayerMask;

        [Tooltip("Layers that contain interactable objects. This makes the sphere check much faster.")]
        [SerializeField] private LayerMask interactableLayerMask;

        private IInteractable _currentInteractable;

        private readonly Collider[] _nearbyColliderBuffer = new Collider[10];



        public void Interaction()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.Deselect();
                _currentInteractable = null;
            }

            IInteractable interactable = GetInteractable();

            if (interactable != null)
            {
                interactable.Interact(transform);
                currentlyInteracting = true; // Set this flag
                _currentInteractable = interactable;
            }
        }

        public void DeselectCurrentInteractable()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.Deselect();
                _currentInteractable = null;
            }
            currentlyInteracting = false; // Clear this flag
        }


        /// <summary>
        ///  Finds the best interactable object using a two-step process.
        ///  1. Priotizes the object directly in the center of the screen.
        ///  2. Falls back to the closest visible object within a cone.
        /// </summary>
        /// <returns> The best interactable object, or null if none found.</returns>
        public IInteractable GetInteractable()
        {
            if (mainCameraTransform == null) return null;

            // 1. PRIORITY 1: Try the "Crosshair Aim" (Your idea)
            if (TryGetAimedInteractable(out IInteractable aimedInteractable))
            {
                return aimedInteractable;
            }

            // 2. PRIORITY 2: Try the "Nearby & Visible" Fallback
            if (TryGetClosestVisibleInteractable(out IInteractable nearbyInteractable))
            {
                return nearbyInteractable;
            }

            return null;
        }

        /// <summary>
        ///  Checks for an interactable directly in the center of the screen.
        /// </summary>
        private bool TryGetAimedInteractable(out IInteractable interactable)
        {
            interactable = null;

            // Note: We use the obstacleLayerMask here to block the ray
            if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward,
                out RaycastHit hit, interactRange, obstacleLayerMask | interactableLayerMask)) // Check against both
            {
                // We hit something. Now, check if it's on the interactable layer.
                if (((1 << hit.collider.gameObject.layer) & interactableLayerMask) != 0)
                {
                    // It's on the right layer, so it *must* have the component (or setup is wrong)
                    if (hit.collider.TryGetComponent(out interactable))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///  Finds the closest interactable object within a sphere that is also visible.
        /// </summary>
        private bool TryGetClosestVisibleInteractable(out IInteractable interactable)
        {
            interactable = null;
            float closestDistance = float.MaxValue;

            Vector3 cameraPosition = mainCameraTransform.position;
            Vector3 cameraForward = mainCameraTransform.forward;

            // Use the dedicated interactableLayerMask for a massive performance boost
            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position, interactRange, _nearbyColliderBuffer, interactableLayerMask);

            for (int i = 0; i < hitCount; i++)
            {
                Collider col = _nearbyColliderBuffer[i];
                if (!col.TryGetComponent(out IInteractable nearbyInteractable))
                {
                    continue;
                }

                Vector3 targetPosition = col.transform.position;
                float distanceToTarget = Vector3.Distance(cameraPosition, targetPosition);

                // 1. Check Field of View (FOV)
                if (!MathUtils.IsWithinViewAngle(cameraPosition, cameraForward, targetPosition, viewAngle))
                {
                    continue; // Not in view cone
                }

                // 2. Check Line of Sight (LOS)
                Vector3 directionToTarget = (targetPosition - cameraPosition).normalized;
                if (Physics.Raycast(cameraPosition, directionToTarget, distanceToTarget, obstacleLayerMask))
                {
                    continue; // Something is blocking the view
                }

                // 3. Check if it's the closest one so far
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    interactable = nearbyInteractable;
                }
            }

            return interactable != null;
        }

    }
}