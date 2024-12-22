using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.Utility;
using System.Collections.Generic;
using System.Linq;

namespace MagicArmory.Handwraps.Components;

/// <summary>
/// Item enchantment that supresses listed enchantments on other items that user wields
/// </summary>
[TypeId("1d7f206e31e54744974db567f8b70529")]
public class EnchantmentSuppressorEnchantment : ItemEnchantmentComponentDelegate<ItemEntity, EnchantmentSuppressorEnchantment.SuppressedEffectData>,
    IUnitEquipmentHandler,
    IUnitEmptyHandWeaponHandler
{
    public BlueprintEquipmentEnchantmentReference[] m_EnchantmentsToSuppress = [];
    private ReferenceArrayProxy<BlueprintEquipmentEnchantment, BlueprintEquipmentEnchantmentReference> EnchantmentsToSuppress => m_EnchantmentsToSuppress;

    public override void OnTurnOn()
    {
        Main.log.Log("OnTurnOn called");
        Refresh();
    }

    private void Refresh()
    {
        if (Data.HasDisabled)
        {
            UpdateDisabled();
            return;
        }

        DisableEnchantments();
        Data.HasDisabled = true;
        RemoveStale();
    }

    public override void OnTurnOff()
    {
        Data.Enchantments.ForEach(e =>
        {
            e.Fact?.Activate();
            e.Fact?.TurnOn();
        });
        Data.Enchantments.Clear();
    }

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
        if (slot.Owner != Owner.Wielder) return;

        // collect all unarmed-buffing enchantments from all other items
        // and suppress them
        Owner.Wielder.Body.AllSlots
            .Where(slot => slot.HasItem)
            .Select(slot => slot.Item)
            .Where(item => item != null && item != Owner)
            .Distinct()
            .SelectMany(item => item.Enchantments)
            .Where(e => EnchantmentsToSuppress.Contains(e.Blueprint))
            .Where(e => !Data.Enchantments.Any(x => x.FactId == e.UniqueId))
            .ForEach(e =>
            {
                if (e.IsTurnedOn)
                {
                    e.Deactivate();
                    Data.Enchantments.Add(e);
                }
            });

        RemoveStale();
    }

    private void UpdateDisabled()
    {
        for (int i = Data.Enchantments.Count - 1; i >= 0; i--)
        {
            var e = Data.Enchantments[i];
            if (e.Fact != null)
            {
                e.Fact.Deactivate();
            }
            else
            {
                Data.Enchantments.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Removes data entries that point at null fact
    /// </summary>
    private void RemoveStale()
    {
        Data.Enchantments.RemoveAll(x => x.Fact == null);
    }

    private void DisableEnchantments()
    {
        Owner.Wielder.Body.AllSlots
            .Where(slot => slot.HasItem)
            .Select(slot => slot.Item)
            .Where(item => item != null && item != Owner)
            .Distinct()
            .SelectMany(item => item.Enchantments)
            .Where(e => EnchantmentsToSuppress.Contains(e.Blueprint))
            .Where(e => !Data.Enchantments.Any(x => x.FactId == e.UniqueId))
            .ForEach(e =>
            {
                if (e.IsTurnedOn)
                {
                    e.Deactivate();
                    Data.Enchantments.Add(e);
                }
            });
    }

    void IUnitEmptyHandWeaponHandler.HandleUnitEmptyHandWeaponUpdated()
    {

        Refresh();
    }


    public class SuppressedEffectData
    {
        public bool HasDisabled = false;
        public List<EntityFactRef<ItemEnchantment>> Enchantments = [];
    }
}
