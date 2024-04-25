using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Loot;

namespace MagicArmory;
public static class Extensions
{
    /// <summary>
    /// Adds item to the shared vendor table
    /// </summary>
    /// <param name="vendorTable">table to add to</param>
    /// <param name="item">item blueprint</param>
    /// <param name="count">amount (default = 1)</param>
    public static void AddItem(this BlueprintSharedVendorTable vendorTable, BlueprintItem item, int count = 1)
    {
        vendorTable.AddComponent<LootItemsPackFixed>(c =>
        {
            c.m_Item = new LootItem
            {
                m_Type = LootItemType.Item,
                m_Item = item.ToReference<BlueprintItemReference>()
            };
            c.m_Count = count;
        });
    }
}
