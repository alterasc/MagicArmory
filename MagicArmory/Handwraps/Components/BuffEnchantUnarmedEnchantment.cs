using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using UnityEngine;

namespace MagicArmory.Handwraps.Components;

/// <summary>
/// Item enchantment that applies referenced weapon enchantment to unarmed strikes
/// This enchantment goes on items, not hands themselves
/// </summary>
[TypeId("a8673f4e242a44bc9dc4bb4390384a3c")]
public class BuffEnchantUnarmedEnchantment :
    ItemEnchantmentComponentDelegate<ItemEntity, BuffEnchantUnarmedEnchantmentData>,
    IUnitEmptyHandWeaponHandler,
    IUnitActiveEquipmentSetHandler
{
    [SerializeField]
    public BlueprintItemEnchantmentReference m_EnchantmentBlueprint;

    public override void OnTurnOn()
    {
        AddEnchantment();
    }

    public override void OnTurnOff()
    {
        Data.Enchantment?.Owner?.RemoveEnchantment(Data.Enchantment);
        Data.Enchantment = null;
    }

    private void AddEnchantment()
    {
        ItemEntityWeapon maybeWeapon = Owner?.Wielder?.Body?.PrimaryHand?.MaybeWeapon;
        if (maybeWeapon != null && maybeWeapon.Blueprint.IsUnarmed)
            Data.Enchantment = maybeWeapon.AddEnchantment(m_EnchantmentBlueprint?.Get(), Context);
    }

    private void Refresh()
    {
        if (Data.Enchantment == null)
        {
            AddEnchantment();
        }
        else
        {
            ItemEntityWeapon maybeWeapon = Owner?.Wielder?.Body?.PrimaryHand?.MaybeWeapon;
            if (maybeWeapon != null && maybeWeapon.Blueprint.IsUnarmed)
            {
                if (!maybeWeapon.m_Enchantments.HasFact(Data.Enchantment))
                {
                    Data.Enchantment = maybeWeapon.AddEnchantment(m_EnchantmentBlueprint?.Get(), Context);
                }
            }
            else
            {
                Data.Enchantment?.Owner?.RemoveEnchantment(Data.Enchantment);
                Data.Enchantment = null;
            }
        }
    }

    void IUnitEmptyHandWeaponHandler.HandleUnitEmptyHandWeaponUpdated()
    {
        Refresh();
    }

    void IUnitActiveEquipmentSetHandler.HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
    {
        if (unit != Owner.Wielder) return;
        Refresh();
    }
}

public class BuffEnchantUnarmedEnchantmentData
{
    [JsonProperty]
    public ItemEnchantment Enchantment;
}