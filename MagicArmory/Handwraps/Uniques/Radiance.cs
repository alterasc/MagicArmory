using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using static MagicArmory.Handwraps.HandwrapsAdder;

namespace MagicArmory.Handwraps.Uniques;

internal static class Radiance
{

    private static readonly Dictionary<Guid, BlueprintItem> RadianceDict = [];

    /// <summary>
    /// Adds or remove handwraps version together with radiance
    /// Because editing all cues and checks and conditions is too much work
    /// </summary>
    [HarmonyPatch]
    private static class RadiancePatches
    {
        [HarmonyPatch(typeof(AddItemToPlayer), nameof(AddItemToPlayer.GiveItem))]
        [HarmonyPostfix]
        private static void AfterAddItemToPlayer(ItemEntity item, ItemsCollection inventory)
        {
            if (item?.Blueprint == null)
            {
                return;
            }
            if (RadianceDict.TryGetValue(item.Blueprint.AssetGuid.m_Guid, out var addonItem))
            {
                var addonEntity = addonItem.CreateEntity();
                inventory.Add(addonEntity);
            }
        }

        [HarmonyPatch(typeof(RemoveItemFromPlayer), nameof(RemoveItemFromPlayer.RunAction))]
        [HarmonyPostfix]
        private static void AfterRemoveItemFromPlayer(RemoveItemFromPlayer __instance)
        {
            if (__instance.m_ItemToRemove == null)
            {
                return;
            }
            if (RadianceDict.TryGetValue(__instance.m_ItemToRemove.Guid.m_Guid, out var addonItem))
            {
                int count = GameHelper.GetPlayerCharacter().Inventory.Count(addonItem);
                if (count > 0)
                {
                    GameHelper.GetPlayerCharacter().Inventory.Remove(addonItem, count, true);
                }
            }
        }
    }

    internal static void CreateRadiance(HandwrapsData handwrapsData)
    {
        if (MagicArmorySettings.Handwraps.Radiance)
        {
            Main.HarmonyInstance.CreateClassProcessor(typeof(RadiancePatches)).Patch();
            Main.log.Log("Added Radiance handwraps");
        }
        RadianceMasterWork(handwrapsData);
        Radiance1(handwrapsData);
        Radiance2(handwrapsData);

        var radianceAuraBuff = Utils.CreateBlueprint<BlueprintBuff>("RadianceWrapsAuraBuff", bp =>
        {
            bp.AddComponent<AddAreaEffect>(c =>
            {
                c.m_AreaEffect = Utils.GetBlueprintReference<BlueprintAbilityAreaEffectReference>("11e53f84005993e4695ca1c3c0ed883a");
            });
            bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
        });

        var radianceAuraAndDispelFeature = Utils.CreateBlueprint<BlueprintFeature>("RadianceWrapsFeature", bp =>
        {
            bp.AddComponent<AddFacts>(c =>
            {
                c.m_Facts = [
                    Utils.GetBlueprintReference<BlueprintUnitFactReference>("e8d005d22023483ea00e7263579c43cb"),
                    radianceAuraBuff.ToReference<BlueprintUnitFactReference>()
                ];
            });
            bp.HideInUI = true;
        });

        var radianceEnchantment = Utils.CreateBlueprint<BlueprintEquipmentEnchantment>("RadianceWrapsEnchantment", bp =>
        {
            bp.AddComponent<AddUnitFeatureEquipment>(c =>
            {
                c.m_Feature = radianceAuraAndDispelFeature.ToReference<BlueprintFeatureReference>();
            });
        });
        var radianceEnchantmentRef = radianceEnchantment.ToReference<BlueprintEquipmentEnchantmentReference>();
        Radiance4Good(handwrapsData, radianceEnchantmentRef);
        Radiance6Good(handwrapsData, radianceEnchantmentRef);
        Radiance4Bad(handwrapsData, radianceEnchantmentRef);
        Radiance6Bad(handwrapsData, radianceEnchantmentRef);
    }

    private static void RadianceMasterWork(HandwrapsData handwrapsData)
    {
        var radianceMasterWorkGuid = "3b2df06a731030d49a1240b763cb6069";
        var radianceMasterWorkSword = Utils.GetBlueprint<BlueprintItemWeapon>(radianceMasterWorkGuid);

        var radianceMasterWorkWraps = CreateHandwraps("RadianceWrapsMasterwork", bp =>
        {
            bp.m_DisplayNameText = radianceMasterWorkSword.m_DisplayNameText;
            bp.m_DescriptionText = radianceMasterWorkSword.m_DescriptionText;
            bp.m_FlavorText = radianceMasterWorkSword.m_FlavorText;
            bp.m_Cost = 0;
            bp.m_Enchantments = [
                handwrapsData.coldIronEnch,
                handwrapsData.masterworkEnch
            ];
        });
        if (MagicArmorySettings.Handwraps.Radiance)
        {
            RadianceDict.Add(Guid.Parse(radianceMasterWorkGuid), radianceMasterWorkWraps);
            var loot = Utils.GetBlueprint<BlueprintLoot>("92e368d7ac577d34c95ecc5fcac22781");
            loot.Items = [.. loot.Items, new LootEntry() { m_Item = radianceMasterWorkWraps.ToReference<BlueprintItemReference>() }];
        }
    }

    private static void Radiance1(HandwrapsData handwrapsData)
    {
        var radiancePlus1ITemGuid = "de1fc233ad934a0a93a17ebed3ec0cfb";
        var radiancePlus1ITemSword = Utils.GetBlueprint<BlueprintItemWeapon>(radiancePlus1ITemGuid);

        var radiancePlus1ITemWraps = CreateHandwraps("RadianceWrapsPlus1", bp =>
        {
            bp.m_DisplayNameText = radiancePlus1ITemSword.m_DisplayNameText;
            bp.m_DescriptionText = radiancePlus1ITemSword.m_DescriptionText;
            bp.m_FlavorText = radiancePlus1ITemSword.m_FlavorText;
            bp.m_Cost = radiancePlus1ITemSword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[0],
                handwrapsData.coldIronEnch
            ];
        });
        if (MagicArmorySettings.Handwraps.Radiance)
        {
            RadianceDict.Add(Guid.Parse(radiancePlus1ITemGuid), radiancePlus1ITemWraps);
        }
    }

    private static void Radiance2(HandwrapsData handwrapsData)
    {
        var radiancePlus2Guid = "6a80e629e9a5ca74da1dabc2984bba3b";
        var radiancePlus2Sword = Utils.GetBlueprint<BlueprintItemWeapon>(radiancePlus2Guid);

        var radiancePlus2Wraps = CreateHandwraps("RadianceWrapsPlus2", bp =>
        {
            bp.m_DisplayNameText = radiancePlus2Sword.m_DisplayNameText;
            bp.m_DescriptionText = radiancePlus2Sword.m_DescriptionText;
            bp.m_FlavorText = radiancePlus2Sword.m_FlavorText;
            bp.m_Cost = radiancePlus2Sword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[1],
                handwrapsData.coldIronEnch
            ];
        });

        if (MagicArmorySettings.Handwraps.Radiance)
        {
            RadianceDict.Add(Guid.Parse(radiancePlus2Guid), radiancePlus2Wraps);
        }
    }

    private static void Radiance4Good(HandwrapsData handwrapsData, BlueprintEquipmentEnchantmentReference radianceEnchantment)
    {
        var radianceGoodEnch = Utils.GetBlueprint<BlueprintWeaponEnchantment>("119b0b2ddae69d4438e6a4bedff32412");
        var yaniel_Longsword4HolyAvengerGuid = "0ff011d62af77e9428e12ac08f63709e";
        var yaniel_Longsword4HolyAvengerSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword4HolyAvengerGuid);

        var radianceWrapsPlus4Good = CreateHandwraps("RadianceWrapsPlus4Good", bp =>
        {
            bp.m_DisplayNameText = yaniel_Longsword4HolyAvengerSword.m_DisplayNameText;
            bp.m_DescriptionText = yaniel_Longsword4HolyAvengerSword.m_DescriptionText;
            bp.m_FlavorText = yaniel_Longsword4HolyAvengerSword.m_FlavorText;
            bp.m_Cost = yaniel_Longsword4HolyAvengerSword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[3],
                handwrapsData.coldIronEnch,
                handwrapsData.holy,
                radianceEnchantment
            ];
            bp.SpendCharges = false;
            bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
        });

        if (MagicArmorySettings.Handwraps.Radiance)
        {
            RadianceDict.Add(Guid.Parse(yaniel_Longsword4HolyAvengerGuid), radianceWrapsPlus4Good);
        }
    }

    private static void Radiance6Good(HandwrapsData handwrapsData, BlueprintEquipmentEnchantmentReference radianceEnchantment)
    {
        var radianceGood6Ench = Utils.GetBlueprint<BlueprintWeaponEnchantment>("431f0d8a21b9469d946f5ae50f5b126e");
        var yaniel_Longsword6HolyAvengerGuid = "cf5c1a507825f184dacbc3abe14b9db1";
        var yaniel_Longsword6HolyAvengerSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword6HolyAvengerGuid);

        var radianceWrapsPlus6Good = CreateHandwraps("RadianceWrapsPlus6Good", bp =>
        {
            bp.m_DisplayNameText = yaniel_Longsword6HolyAvengerSword.m_DisplayNameText;
            bp.m_DescriptionText = yaniel_Longsword6HolyAvengerSword.m_DescriptionText;
            bp.m_FlavorText = yaniel_Longsword6HolyAvengerSword.m_FlavorText;
            bp.m_Cost = yaniel_Longsword6HolyAvengerSword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[5],
                handwrapsData.coldIronEnch,
                handwrapsData.holy,
                radianceEnchantment
            ];
            bp.SpendCharges = false;
            bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
        });

        if (MagicArmorySettings.Handwraps.Radiance)
        {
            RadianceDict.Add(Guid.Parse(yaniel_Longsword6HolyAvengerGuid), radianceWrapsPlus6Good);
        }
    }

    private static void Radiance4Bad(HandwrapsData handwrapsData, BlueprintEquipmentEnchantmentReference radianceEnchantment)
    {
        var radianceBadEnch = Utils.GetBlueprint<BlueprintWeaponEnchantment>("40433f161d052d74aa80769dcaf1df09");
        var yaniel_Longsword4BaneLivingGuid = "27bab9c65039ef54d8fc24d9641cbea0";
        var yaniel_Longsword4BaneLivingSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword4BaneLivingGuid);

        var radianceWrapsPlus4Bad = CreateHandwraps("RadianceWrapsPlus4Bad", bp =>
        {
            bp.m_DisplayNameText = yaniel_Longsword4BaneLivingSword.m_DisplayNameText;
            bp.m_DescriptionText = yaniel_Longsword4BaneLivingSword.m_DescriptionText;
            bp.m_FlavorText = yaniel_Longsword4BaneLivingSword.m_FlavorText;
            bp.m_Cost = yaniel_Longsword4BaneLivingSword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[3],
                handwrapsData.coldIronEnch,
                handwrapsData.baneLiving,
                radianceEnchantment
            ];
            bp.SpendCharges = false;
            bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
        });

        if (MagicArmorySettings.Handwraps.Radiance)
        {
            RadianceDict.Add(Guid.Parse(yaniel_Longsword4BaneLivingGuid), radianceWrapsPlus4Bad);
        }
    }

    private static void Radiance6Bad(HandwrapsData handwrapsData, BlueprintEquipmentEnchantmentReference radianceEnchantment)
    {
        var radianceBad6Ench = Utils.GetBlueprint<BlueprintWeaponEnchantment>("955dae09e1484fdda91bafe1ca336e0c");
        var yaniel_Longsword6BaneLivingGuid = "bff8a4bb7f24a2c499db0781b5750133";
        var yaniel_Longsword6BaneLivingSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword6BaneLivingGuid);

        var radianceWrapsPlus6Bad = CreateHandwraps("RadianceWrapsPlus6Bad", bp =>
        {
            bp.m_DisplayNameText = yaniel_Longsword6BaneLivingSword.m_DisplayNameText;
            bp.m_DescriptionText = yaniel_Longsword6BaneLivingSword.m_DescriptionText;
            bp.m_FlavorText = yaniel_Longsword6BaneLivingSword.m_FlavorText;
            bp.m_Cost = yaniel_Longsword6BaneLivingSword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[5],
                handwrapsData.coldIronEnch,
                handwrapsData.baneLiving,
                radianceEnchantment
            ];
            bp.SpendCharges = false;
            bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
        });

        if (MagicArmorySettings.Handwraps.Radiance)
        {
            RadianceDict.Add(Guid.Parse(yaniel_Longsword6BaneLivingGuid), radianceWrapsPlus6Bad);
        }
    }
}