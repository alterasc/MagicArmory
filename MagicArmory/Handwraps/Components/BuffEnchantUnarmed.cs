using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using Newtonsoft.Json;
using UnityEngine;

namespace MagicArmory.Handwraps.Components;

/// <summary>
/// Component that applies enchantment to unarmed strikes
/// and reapplies it when it gets lost
/// </summary>
[TypeId("6e4604cb24034a2bb4934c495d55b59d")]
public class BuffEnchantUnarmed : UnitBuffComponentDelegate<BuffEnchantUnarmedData>,
    IUnitEmptyHandWeaponHandler,
    IUnitActiveEquipmentSetHandler
{
    /// <summary>
    /// Enchantment to apply
    /// </summary>
    [SerializeField]
    public BlueprintItemEnchantmentReference m_EnchantmentBlueprint;

    /// <summary>
    /// True = Apply only on if during first activation unit is unarmed. I.e. Crusader Edge has this component
    /// but if it was cast on unit wielding weapon, we don't want it applying to unarmed. 
    /// 
    /// False (default) = constant effect that should apply whenever unit switches to unarmed
    /// </summary>
    public bool OnlyOnFirstCast;

    public override void OnActivate()
    {
        if (Data != null && Data.NoReactivateOnRefresh && Data.Enchantment == null)
        {
            return;
        }
        AddEnchantment();
        if (Data.Enchantment == null && OnlyOnFirstCast)
        {
            Data.NoReactivateOnRefresh = true;
        }
    }

    private void AddEnchantment()
    {
        ItemEntityWeapon maybeWeapon = Owner?.Body?.PrimaryHand?.MaybeWeapon;
        if (maybeWeapon != null && maybeWeapon.Blueprint.IsUnarmed)
            Data.Enchantment = maybeWeapon.AddEnchantment(m_EnchantmentBlueprint?.Get(), Context);
    }

    private void Refresh()
    {
        if (Data.NoReactivateOnRefresh)
        {
            return;
        }
        if (Data.Enchantment == null)
        {
            AddEnchantment();
        }
        else
        {
            ItemEntityWeapon maybeWeapon = Owner?.Body?.PrimaryHand?.MaybeWeapon;
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

    public override void OnDeactivate()
    {
        Data.Enchantment?.Owner?.RemoveEnchantment(Data.Enchantment);
        Data.Enchantment = null;
    }

    void IUnitEmptyHandWeaponHandler.HandleUnitEmptyHandWeaponUpdated()
    {
        Refresh();
    }

    void IUnitActiveEquipmentSetHandler.HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
    {
        if (unit != Owner) return;
        Refresh();
    }
}

public class BuffEnchantUnarmedData
{
    /// <summary>
    /// Enchantment that being applied
    /// </summary>
    [JsonProperty]
    public ItemEnchantment Enchantment;

    /// <summary>
    /// Monitoring flag to check if should be re-applied
    /// </summary>
    [JsonProperty]
    public bool NoReactivateOnRefresh;
}
