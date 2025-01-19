using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using MagicArmory.Handwraps.Components;
using MagicArmory.Handwraps.Uniques;
using System;
using System.Collections.Generic;

namespace MagicArmory.Handwraps;
internal static class HandwrapsAdder
{
    internal static HandwrapsData Data;
    internal static List<BlueprintEquipmentEnchantmentReference> CreateUnarmedOnlyEnhancements()
    {
        List<BlueprintWeaponEnchantment> weaponEnchantments = [
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("eb2faccc4c9487d43b3575d7e77ff3f5"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("80bb8a737579e35498177e1e3c75899b"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("783d7d496da6ac44f9511011fc5f1979"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("bdba267e951851449af552aa9f9e3992"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("0326d02d2e24d254a9ef626cc7a3850f")
        ];

        List<BlueprintEquipmentEnchantmentReference> enchantments = [];
        for (int i = 1; i <= 6; i++)
        {
            var ench = CreateUnarmedOnlyEnchantment($"MagicArmoryUnarmedEnhancement{i}", weaponEnchantments[i - 1]);
            enchantments.Add(ench.ToReference<BlueprintEquipmentEnchantmentReference>());
        }
        return enchantments;
    }

    internal static BlueprintEquipmentEnchantment CreateUnarmedOnlyEnchantment(string name, BlueprintItemEnchantment enchantment)
    {
        var newEnch = Utils.CreateBlueprint<BlueprintEquipmentEnchantment>(name, bp =>
        {
            bp.m_EnchantName = enchantment.m_EnchantName;
            bp.m_Description = enchantment.m_Description;
            bp.m_IdentifyDC = enchantment.m_IdentifyDC;
            bp.m_EnchantmentCost = enchantment.m_EnchantmentCost;
            bp.AddComponent<BuffEnchantUnarmedEnchantment>(c =>
            {
                c.m_EnchantmentBlueprint = enchantment.ToReference<BlueprintItemEnchantmentReference>();
            });
        });
        return newEnch;
    }

    internal static void CreateEnchantmentsAndHandwraps()
    {
        Data = new();
        Data.handwrapsTypeString = "Handwraps".ToLocalized();
        Data.glovesIcon = Utils.GetBlueprint<BlueprintItemEquipmentGloves>("c33129949c59ee044900e741691a64d3").m_Icon;
        Data.scabbard_ArodensWrathItem = Utils.GetBlueprint<BlueprintItemEquipmentUsable>("c5eb4864c764a9945809525dda77acc8");
        Data.enhancementEnchs = CreateUnarmedOnlyEnhancements();
        Data.coldIronEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsColdIron", Utils.GetBlueprint<BlueprintWeaponEnchantment>("e5990dc76d2a613409916071c898eee8")).ToReference<BlueprintEquipmentEnchantmentReference>();
        Data.ghostTouchEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsGhostTouch", Utils.GetBlueprint<BlueprintWeaponEnchantment>("47857e1a5a3ec1a46adf6491b1423b4f")).ToReference<BlueprintEquipmentEnchantmentReference>();
        Data.heartSeekerEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsHeartseeker", Utils.GetBlueprint<BlueprintWeaponEnchantment>("e252b26686ab66241afdf33f2adaead6")).ToReference<BlueprintEquipmentEnchantmentReference>();
        Data.finneanChapter3Ench = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsFinneanChapter3", Utils.GetBlueprint<BlueprintWeaponEnchantment>("b183bd491793d194c9e4c96cd11769b1")).ToReference<BlueprintEquipmentEnchantmentReference>();
        Data.brilliantEnergyEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsBrilliantEnergy", Utils.GetBlueprint<BlueprintWeaponEnchantment>("66e9e299c9002ea4bb65b6f300e43770")).ToReference<BlueprintEquipmentEnchantmentReference>();
        Data.masterworkEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsMasterwork", Utils.GetBlueprint<BlueprintWeaponEnchantment>("6b38844e2bffbac48b63036b66e735be")).ToReference<BlueprintEquipmentEnchantmentReference>();
        Data.holy = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsHoly", Utils.GetBlueprint<BlueprintWeaponEnchantment>("28a9964d81fedae44bae3ca45710c140")).ToReference<BlueprintEquipmentEnchantmentReference>();
        Data.baneLiving = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsBaneLiving", Utils.GetBlueprint<BlueprintWeaponEnchantment>("e1d6f5e3cd3855b43a0cb42f6c747e1c")).ToReference<BlueprintEquipmentEnchantmentReference>();

        Finnean.CreateFinnean(Data);
        Radiance.CreateRadiance(Data);

        CreateBasicEnchantedVersions(Data);
    }

    private static void CreateBasicEnchantedVersions(HandwrapsData handwrapsData)
    {
        var handwrapsItemLName = "Handwraps".ToLocalized();
        var handwrapsItemLDescription = "Handwraps enchantments apply only to unarmed attacks.".ToLocalized();
        int[] prices = [2000, 8000, 18000, 32000, 50000];
        var normalHandwrapsList = new List<BlueprintItemEquipmentUsableReference>();
        for (int i = 1; i <= 5; i++)
        {
            var normalWorkWraps = CreateHandwraps($"MagicArmoryHandwrapsPlus{i}", bp =>
            {
                bp.m_DisplayNameText = handwrapsItemLName;
                bp.m_DescriptionText = handwrapsItemLDescription;
                bp.m_FlavorText = new();
                bp.m_Cost = prices[i - 1];
                bp.m_Enchantments = [
                    handwrapsData.enhancementEnchs[i - 1]
                ];
            });
            normalHandwrapsList.Add(normalWorkWraps.ToReference<BlueprintItemEquipmentUsableReference>());
        }
        if (MagicArmorySettings.Handwraps.Shops)
        {
            // to base game
            Vendors.WarCamp_QuartermasterVendorTable.AddItem(normalHandwrapsList[0]);
            Vendors.Jeweler_Chapter3VendorTable.AddItem(normalHandwrapsList[0]);
            Vendors.Jeweler_Chapter3VendorTable.AddItem(normalHandwrapsList[1]);
            Vendors.Jeweler_Chapter3VendorTable.AddItem(normalHandwrapsList[2]);
            Vendors.Jeweler_Chapter5VendorTable.AddItem(normalHandwrapsList[3]);
            Vendors.RvanyVendorTable.AddItem(normalHandwrapsList[3]);
            Vendors.Jeweler_Chapter5VendorTable.AddItem(normalHandwrapsList[4]);

            // to DLC1
            Vendors.Jeweler_DLC1VendorTable.AddItem(normalHandwrapsList[4], 6);

            // to standalone DLC3
            Vendors.DLC3_VendorTable_Equipment.AddItem(normalHandwrapsList[0], 5);
            Vendors.DLC3_VendorTable_Equipment.AddItem(normalHandwrapsList[1], 5);
            Vendors.DLC3_VendorTable_Equipment.AddItem(normalHandwrapsList[2], 5);
            Vendors.DLC3_VendorTable_Equipment.AddItem(normalHandwrapsList[3], 5);
            Vendors.DLC3_VendorTable_Equipment.AddItem(normalHandwrapsList[4], 5);

            Main.log.Log("Added basic enchanted handwraps to shops");
        }
    }

    internal static BlueprintItemEquipmentUsable CreateHandwraps(string name, Action<BlueprintItemEquipmentUsable> init)
    {
        return Utils.CreateBlueprint<BlueprintItemEquipmentUsable>(name, bp =>
        {
            SetFields(bp);
            init?.Invoke(bp);
            bp.AddComponent<EquipmentRestrictionOnlyOneWraps>();
        });
    }

    internal static void SetFields(BlueprintItemEquipmentUsable bp)
    {
        bp.m_NonIdentifiedNameText = Data.handwrapsTypeString;
        bp.m_Cost = 1;
        bp.m_Weight = 0f;
        bp.m_Destructible = false;
        bp.m_MiscellaneousType = BlueprintItem.MiscellaneousItemType.None;
        bp.m_InventoryPutSound = Data.scabbard_ArodensWrathItem.m_InventoryPutSound;
        bp.m_InventoryTakeSound = Data.scabbard_ArodensWrathItem.m_InventoryTakeSound;
        bp.m_EquipmentEntity = Data.scabbard_ArodensWrathItem.m_EquipmentEntity;
        bp.m_Icon = Data.glovesIcon;
        bp.m_BeltItemPrefab = new();
        bp.Type = UsableItemType.Other;
    }
}
