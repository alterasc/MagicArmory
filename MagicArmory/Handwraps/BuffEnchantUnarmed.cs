using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using Newtonsoft.Json;
using UnityEngine;

namespace MagicArmory.Handwraps;

[TypeId("6e4604cb24034a2bb4934c495d55b59d")]
public class BuffEnchantUnarmed : UnitBuffComponentDelegate<BuffEnchantUnarmedData>,
    IUnitEquipmentHandler,
    IUnitActiveEquipmentSetHandler
{
    [SerializeField]
    public BlueprintItemEnchantmentReference m_EnchantmentBlueprint;

    public override void OnActivate()
    {
        AddEnchantment();
    }

    private void AddEnchantment()
    {
        ItemEntityWeapon maybeWeapon = this.Owner?.Body?.PrimaryHand?.MaybeWeapon;
        if (maybeWeapon != null && maybeWeapon.Blueprint.IsUnarmed)
            this.Data.Enchantment = maybeWeapon.AddEnchantment(m_EnchantmentBlueprint?.Get(), this.Context);
    }

    private void Refresh()
    {
        if (this.Data.Enchantment == null)
        {
            AddEnchantment();
        }
        else
        {
            ItemEntityWeapon maybeWeapon = this.Owner?.Body?.PrimaryHand?.MaybeWeapon;
            if (maybeWeapon != null && maybeWeapon.Blueprint.IsUnarmed && !maybeWeapon.m_Enchantments.HasFact(this.Data.Enchantment))
            {
                this.Data.Enchantment = maybeWeapon.AddEnchantment(m_EnchantmentBlueprint?.Get(), this.Context);
            }
            else
            {

                this.Data.Enchantment?.Owner?.RemoveEnchantment(this.Data.Enchantment);
                this.Data.Enchantment = null;
            }
        }
    }

    public override void OnDeactivate()
    {
        this.Data.Enchantment?.Owner?.RemoveEnchantment(this.Data.Enchantment);
        this.Data.Enchantment = null;
    }

    void IUnitEquipmentHandler.HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
        if (slot.Owner != this.Owner) return;
        if (slot != this.Owner.Body.CurrentHandsEquipmentSet.PrimaryHand) return;
        Refresh();
    }

    void IUnitActiveEquipmentSetHandler.HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
    {
        if (unit != this.Owner) return;
        Refresh();
    }
}

public class BuffEnchantUnarmedData
{
    [JsonProperty]
    public ItemEnchantment Enchantment;
}
