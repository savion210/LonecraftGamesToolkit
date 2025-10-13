using System;

namespace LonecraftGames.Toolkit.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public ItemBase Item;
        public int Quantity;

        public bool IsEmpty => Item == null || Quantity <= 0;
        public bool IsFull => Item != null && Quantity >= Item.MaxStackSize;

        public void Clear() { Item = null; Quantity = 0; }

        public int Add(ItemBase item, int amount)
        {
            if (amount <= 0) return 0;

            if (IsEmpty)
            {
                Item = item.Clone();
                int add = Math.Min(amount, Item.MaxStackSize);
                Quantity = add;
                return amount - add;
            }

            if (Item.Id != item.Id) return amount;

            int space = Item.MaxStackSize - Quantity;
            int add2 = Math.Min(space, amount);
            Quantity += add2;
            return amount - add2;
        }

        public int Remove(int amount)
        {
            if (IsEmpty || amount <= 0) return 0;
            int removed = Math.Min(amount, Quantity);
            Quantity -= removed;
            if (Quantity <= 0) Clear();
            return removed;
        }
    }
}