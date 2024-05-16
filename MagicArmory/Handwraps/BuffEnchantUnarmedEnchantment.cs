using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using UnityEngine;

namespace MagicArmory.Handwraps;

[TypeId("a8673f4e242a44bc9dc4bb4390384a3c")]
public class BuffEnchantUnarmedEnchantment :
    ItemEnchantmentComponentDelegate<ItemEntity, BuffEnchantUnarmedEnchantmentData>,
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
        ItemEntityWeapon maybeWeapon = this.Owner?.Wielder?.Body?.PrimaryHand?.MaybeWeapon;
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
            ItemEntityWeapon maybeWeapon = this.Owner?.Wielder?.Body?.PrimaryHand?.MaybeWeapon;
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
        if (slot.Owner != this.Owner.Wielder) return;
        if (slot != this.Owner.Wielder.Body.CurrentHandsEquipmentSet.PrimaryHand) return;
        Refresh();
    }

    void IUnitActiveEquipmentSetHandler.HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
    {
        if (unit != this.Owner.Wielder) return;
        Refresh();
    }
}

public class BuffEnchantUnarmedEnchantmentData
{
    [JsonProperty]
    public ItemEnchantment Enchantment;
}