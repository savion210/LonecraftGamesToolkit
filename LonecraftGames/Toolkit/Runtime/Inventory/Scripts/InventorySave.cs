using System;
using System.Collections.Generic;

namespace LonecraftGames.Toolkit.Inventory
{
    [Serializable]
    public class InventorySave
    {
        public Inventory.InventoryData Player;
        public Inventory.InventoryData Chest;
    }

    // If you have many inventories, use this instead:
    [Serializable]
    public class InventorySaveMap
    {
        public Dictionary<string, Inventory.InventoryData> Inventories = new();
    }
}