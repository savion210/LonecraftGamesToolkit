using System.Collections.Generic;
using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public Inventory Inventory;
        public InventorySlotUI SlotPrefab;
        public Transform SlotsRoot;
        private GameObject _target;

        readonly List<InventorySlotUI> _views = new();

        private void Awake()
        {
            Build();
            Inventory.SlotChanged += OnSlotChanged;
        }

        private void OnDestroy()
        {
            if (Inventory != null) Inventory.SlotChanged -= OnSlotChanged;
        }

        private void Build()
        {
            for (int i = 0; i < Inventory.Size; i++)
            {
                var v = Instantiate(SlotPrefab, SlotsRoot);
                v.Bind(this, Inventory, i, _target);
                _views.Add(v);
                Refresh(i);
            }
        }

        private void OnSlotChanged(int index, InventorySlot slot) => Refresh(index);

        public void Refresh(int index)
        {
            var s = Inventory.GetSlot(index);
            _views[index].SetVisual(s.Item?.Icon, s.Quantity);
        }

        public void RefreshAll()
        {
            for (int i = 0; i < Inventory.Size; i++)
                Refresh(i);
        }
        
        public void SetTarget(GameObject target)
        {
            _target = target;
            foreach (var v in _views)
                v.Bind(this, Inventory, v.SlotIndex, _target);
        }
    }
}