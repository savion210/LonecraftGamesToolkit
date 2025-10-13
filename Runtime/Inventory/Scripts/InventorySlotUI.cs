using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LonecraftGames.Toolkit.Inventory
{
    public class InventorySlotUI :
        MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
    {
        public Image Icon;
        public TMP_Text QuantityText;

        public int SlotIndex { get; private set; }
        public Inventory Inventory { get; private set; }
        InventoryUI _ui;
        private GameObject _target;

        static InventorySlotUI _dragSource;
        static GameObject _dragIcon;
        static Canvas _rootCanvas;
        

        /// <summary>
        ///  Binds this slot view to the given inventory and index.
        /// </summary>
        /// <param name="ui"> so that the slot can request a refresh after an action </param>
        /// <param name="inv"> the inventory this slot belongs to </param>
        /// <param name="index"> the index of this slot in the inventory </param>
        /// <param name="target"> optional target for using items (e.g. player, chest, etc.) </param>
        public void Bind(InventoryUI ui, Inventory inv, int index, GameObject target = null)
        {
            _ui = ui;
            Inventory = inv;
            SlotIndex = index;
            _target = target;
        }

        public void SetVisual(Sprite icon, int qty)
        {
            Icon.enabled = icon != null;
            Icon.sprite = icon;
            QuantityText.text = (icon != null && qty > 1) ? qty.ToString() : "";
        }

        public void OnPointerClick(PointerEventData e)
        {
            if (e.button == PointerEventData.InputButton.Left && e.clickCount == 1)
            {
                if (_target == null)
                    return;
                if (Inventory.UseAt(SlotIndex, _target)) _ui.Refresh(SlotIndex);
            }
            else if (e.button == PointerEventData.InputButton.Right)
            {
                Inventory.RemoveAt(SlotIndex, 1);
            }
        }

        public void OnBeginDrag(PointerEventData e)
        {
            var slot = Inventory.GetSlot(SlotIndex);
            if (slot.IsEmpty) return;

            _dragSource = this;
            if (_rootCanvas == null) _rootCanvas = GetComponentInParent<Canvas>();
            _dragIcon = new GameObject("DraggingIcon", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            _dragIcon.transform.SetParent(_rootCanvas.transform, false);
            _dragIcon.GetComponent<Image>().sprite = slot.Item.Icon;
            _dragIcon.GetComponent<Image>().raycastTarget = false;
            (_dragIcon.transform as RectTransform).sizeDelta = new Vector2(48, 48);
            _dragIcon.transform.position = e.position;
        }

        public void OnDrag(PointerEventData e)
        {
            if (_dragIcon != null) _dragIcon.transform.position = e.position;
        }

        public void OnEndDrag(PointerEventData e)
        {
            if (_dragIcon != null) Destroy(_dragIcon);
            _dragIcon = null;
            _dragSource = null;
        }

        public void OnDrop(PointerEventData e)
        {
            if (_dragSource == null) return;

            if (_dragSource.Inventory == Inventory)
                Inventory.Move(_dragSource.SlotIndex, SlotIndex);
            else
                _dragSource.Inventory.TransferAll(_dragSource.SlotIndex, Inventory);

            _ui.Refresh(SlotIndex);
        }

        // Optional buttons
        private void OnClickUse()
        {
            Inventory.UseAt(SlotIndex, _ui.gameObject);
        }

        private void OnClickRemoveOne()
        {
            Inventory.RemoveAt(SlotIndex, 1);
        }

        private void OnClickRemoveAll()
        {
            var s = Inventory.GetSlot(SlotIndex);
            if (!s.IsEmpty) Inventory.RemoveAt(SlotIndex, s.Quantity);
        }
    }
}