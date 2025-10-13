using System;
using System.Collections.Generic;
using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    [Serializable]
    public class Inventory : IInventory
    {
        [Serializable]
        public class SlotData { public string Id; public int Quantity; }
        [Serializable]
        public class InventoryData { public List<SlotData> Slots = new(); }

        public List<InventorySlot> Slots;

         public event Action<int, InventorySlot> SlotChanged;

        public int Size => Slots.Count;
        public InventorySlot GetSlot(int index) => Slots[index];

        public Inventory(int size)
        {
            Slots = new List<InventorySlot>(size);
            for (int i = 0; i < size; i++) Slots.Add(new InventorySlot());
        }

        void Raise(int i) => SlotChanged?.Invoke(i, Slots[i]);

        public int Add(ItemBase item, int quantity)
        {
            if (item == null || quantity <= 0) return quantity;

            for (int i = 0; i < Slots.Count && quantity > 0; i++)
                if (!Slots[i].IsEmpty && Slots[i].Item.Id == item.Id && !Slots[i].IsFull)
                { quantity = Slots[i].Add(item, quantity); Raise(i); }

            for (int i = 0; i < Slots.Count && quantity > 0; i++)
                if (Slots[i].IsEmpty)
                { quantity = Slots[i].Add(item, quantity); Raise(i); }

            return quantity; // remainder
        }

        public bool Move(int fromSlot, int toSlot)
        {
            if (fromSlot == toSlot || fromSlot < 0 || toSlot < 0 ||
                fromSlot >= Slots.Count || toSlot >= Slots.Count) return false;

            var s = Slots[fromSlot];
            var t = Slots[toSlot];
            if (s.IsEmpty) return false;

            if (t.IsEmpty)
            {
                t.Item = s.Item; t.Quantity = s.Quantity; s.Clear();
                Raise(fromSlot); Raise(toSlot);
                return true;
            }

            if (t.Item.Id == s.Item.Id && !t.IsFull)
            {
                int rem = t.Add(s.Item, s.Quantity);
                s.Quantity = rem;
                if (s.Quantity <= 0) s.Clear();
                Raise(fromSlot); Raise(toSlot);
                return true;
            }

            (t.Item, s.Item) = (s.Item, t.Item);
            (t.Quantity, s.Quantity) = (s.Quantity, t.Quantity);
            Raise(fromSlot); Raise(toSlot);
            return true;
        }

        public int RemoveAt(int slotIndex, int quantity)
        {
            if (slotIndex < 0 || slotIndex >= Slots.Count || quantity <= 0) return 0;
            int removed = Slots[slotIndex].Remove(quantity);
            if (removed > 0) Raise(slotIndex);
            return removed;
        }

        public bool TransferAll(int fromSlot, IInventory target)
        {
            if (target == null || fromSlot < 0 || fromSlot >= Slots.Count) return false;
            var src = Slots[fromSlot];
            if (src.IsEmpty) return false;

            int rem = target.Add(src.Item, src.Quantity);
            int moved = src.Quantity - rem;
            if (moved > 0)
            {
                src.Remove(moved);
                Raise(fromSlot);
                return true;
            }
            return false;
        }

        public bool UseAt(int slotIndex, GameObject user)
        {
            if (slotIndex < 0 || slotIndex >= Slots.Count) return false;
            var s = Slots[slotIndex];
            if (s.IsEmpty) return false;

            if (s.Item.Use(user))
            {
                s.Remove(1);
                Raise(slotIndex);
                return true;
            }
            return false;
        }

        public InventoryData ToData()
        {
            var data = new InventoryData();
            for (int i = 0; i < Slots.Count; i++)
            {
                var slot = Slots[i];
                data.Slots.Add(new SlotData { Id = slot.Item?.Id, Quantity = slot.Quantity });
            }
            return data;
        }

        public void LoadFromData(InventoryData data, Func<string, ItemBase> resolver)
        {
            int n = Mathf.Min(Slots.Count, data.Slots.Count);
            for (int i = 0; i < n; i++)
            {
                var sd = data.Slots[i];
                if (string.IsNullOrEmpty(sd.Id) || sd.Quantity <= 0)
                {
                    Slots[i].Clear();
                }
                else
                {
                    var def = resolver(sd.Id);
                    Slots[i].Item = def?.Clone();
                    Slots[i].Quantity = Mathf.Max(0, sd.Quantity);
                }
                Raise(i);
            }
        }
    }
}
