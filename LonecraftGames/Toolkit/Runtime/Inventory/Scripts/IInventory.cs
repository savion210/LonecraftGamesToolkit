using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    public interface IInventory
    {
        int Add(ItemBase item, int quantity);               // returns remainder
        bool Move(int fromSlot, int toSlot);                // stack/swap
        int RemoveAt(int slotIndex, int quantity);          // returns removed
        bool TransferAll(int fromSlot, IInventory target);
        bool UseAt(int slotIndex, GameObject user);
        int Size { get; }
        InventorySlot GetSlot(int index);
    }
}