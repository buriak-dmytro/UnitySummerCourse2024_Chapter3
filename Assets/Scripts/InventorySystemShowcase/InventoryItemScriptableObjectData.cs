using UnityEngine;

namespace InventorySystemShowcase
{
    [CreateAssetMenu(
        fileName = "InventoryItemData",
        menuName = "Inventory/Item",
        order = 1)]
    public class InventoryItemScriptableObjectData : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
    }
}
