using LonecraftGames.Toolkit.Core.Utilis;
using TMPro;
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.Interaction
{
    [AddComponentMenu("LonecraftGames/UI/Interaction/Player Interact UI")]
    public class PlayerInteractUi : Singleton<PlayerInteractUi>
    {
        [Header("References")] [SerializeField]
        private GameObject containerObject;

        [SerializeField] private PlayerInteract playerInteract;
        [SerializeField] private TextMeshProUGUI interactText;

        [Header("UI Settings")] [SerializeField]
        private Vector3 offsetFromTarget = new Vector3(0, 1.5f, 0);

        [SerializeField] private bool faceCamera = true;

        [SerializeField] private Camera playerCamera;


        private void Update()
        {
            if (playerInteract == null || playerInteract.currentlyInteracting)
            {
                Hide();
                return;
            }

            var interactable = playerInteract.GetInteractable();
            if (interactable != null)
            {
                Show(interactable);
            }
            else
            {
                Hide();
            }

            if (faceCamera && containerObject.activeSelf && playerCamera != null)
            {
                containerObject.transform.forward = playerCamera.transform.forward;
            }
        }

        /// <summary>
        ///  Shows the interaction UI with the provided IInteractable's information.
        /// </summary>
        /// <param name="iInteractable">The interactable object to display information for.</param>
        private void Show(IInteractable iInteractable)
        {
            containerObject.SetActive(true);
            interactText.text = iInteractable.GetInterActText();

            if (playerCamera != null && iInteractable.GetTransform() != null)
            {
                Vector3 targetPosition = iInteractable.GetTransform().position + offsetFromTarget;

                // Adjust the y position using the Yoffset from the IInteractable
                targetPosition.y += iInteractable.Yoffset;

                containerObject.transform.position = targetPosition;
            }
        }

        /// <summary>
        ///  Hides the interaction UI.
        /// </summary>
        private void Hide()
        {
            containerObject.SetActive(false);
        }
    }
}