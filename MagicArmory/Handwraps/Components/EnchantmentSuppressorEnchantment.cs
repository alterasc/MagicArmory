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
public class EnchantmentSuppressorEnchantment : ItemEnchantmentComponentDelegate<ItemEntity, EnchantmentSuppressorEnchantment.SuppressedEffectData>, IUnitEquipmentHandler
{
    public BlueprintEquipmentEnchantmentReference[] m_EnchantmentsToSuppress = [];
    private ReferenceArrayProxy<BlueprintEquipmentEnchantment, BlueprintEquipmentEnchantmentReference> EnchantmentsToSuppress => m_EnchantmentsToSuppress;

    public override void OnTurnOn()
    {
        if (Data.HasDisabled)
        {
            UpdateDisabled();
            return;
        }

        DisableEnchantments();
        Data.HasDisabled = true;
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

        Owner.Enchantments
            .Where(e => e?.SourceItem?.HoldingSlot == slot)
            .Where(e => e.Blueprint != OwnerBlueprint)
            .Where(e => EnchantmentsToSuppress.Contains(e.Blueprint))
            .Where(e => !Data.Enchantments.Any(x => x.FactId == e.UniqueId))
            .ForEach(e =>
            {
                e.Deactivate();
                Data.Enchantments.Add(e);
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
                e.Fact.TurnOff();
            }
            else
            {
                Data.Enchantments.RemoveAt(i);
            }
        }
    }

    private void RemoveStale()
    {
        for (int i = Data.Enchantments.Count - 1; i >= 0; i--)
        {
            if (Data.Enchantments[i].Fact == null)
            {
                Data.Enchantments.RemoveAt(i);
            }
        }
    }

    private void DisableEnchantments()
    {
        Owner.Enchantments
            .Where(e => e.Blueprint != OwnerBlueprint)
            .Where(e => EnchantmentsToSuppress.Contains(e.Blueprint))
            .Where(e => !Data.Enchantments.Any(x => x.FactId == e.UniqueId))
            .ForEach(e =>
            {
                e.Deactivate();
                e.TurnOff();
                Data.Enchantments.Add(e);
            });
    }
    public class SuppressedEffectData
    {
        public bool HasDisabled = false;
        public List<EntityFactRef<ItemEnchantment>> Enchantments = [];
    }
}
