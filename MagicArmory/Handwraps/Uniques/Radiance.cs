using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using MagicArmory.Handwraps.Components;

namespace MagicArmory.Handwraps.Uniques;

internal static class Radiance
{
    internal static void CreateRadiance(HandwrapsAdder.HandwrapsData handwrapsData)
    {
        if (!Main.Settings.RadianceHandwraps)
        {
            return;
        }
        var radianceMasterWorkGuid = "3b2df06a731030d49a1240b763cb6069";
        var radianceMasterWorkSword = Utils.GetBlueprint<BlueprintItemWeapon>(radianceMasterWorkGuid);
        var radianceMasterWorkWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(radianceMasterWorkGuid, bp =>
        {
            HandwrapsAdder.SetFields(bp);
            bp.m_DisplayNameText = radianceMasterWorkSword.m_DisplayNameText;
            bp.m_DescriptionText = radianceMasterWorkSword.m_DescriptionText;
            bp.m_FlavorText = radianceMasterWorkSword.m_FlavorText;
            bp.m_Cost = radianceMasterWorkSword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.coldIronEnch,
                handwrapsData.masterworkEnch
            ];
        });

        var radiancePlus1ITemGuid = "de1fc233ad934a0a93a17ebed3ec0cfb";
        var radiancePlus1ITemSword = Utils.GetBlueprint<BlueprintItemWeapon>(radiancePlus1ITemGuid);
        var radiancePlus1ITemWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(radiancePlus1ITemGuid, bp =>
        {
            HandwrapsAdder.SetFields(bp);
            bp.m_DisplayNameText = radiancePlus1ITemSword.m_DisplayNameText;
            bp.m_DescriptionText = radiancePlus1ITemSword.m_DescriptionText;
            bp.m_FlavorText = radiancePlus1ITemSword.m_FlavorText;
            bp.m_Cost = radiancePlus1ITemSword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[0],
                handwrapsData.coldIronEnch
            ];
        });

        var radiancePlus2Guid = "6a80e629e9a5ca74da1dabc2984bba3b";
        var radiancePlus2Sword = Utils.GetBlueprint<BlueprintItemWeapon>(radiancePlus2Guid);
        var radiancePlus2Wraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(radiancePlus2Guid, bp =>
        {
            HandwrapsAdder.SetFields(bp);
            bp.m_DisplayNameText = radiancePlus2Sword.m_DisplayNameText;
            bp.m_DescriptionText = radiancePlus2Sword.m_DescriptionText;
            bp.m_FlavorText = radiancePlus2Sword.m_FlavorText;
            bp.m_Cost = radiancePlus2Sword.m_Cost;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[1],
                handwrapsData.coldIronEnch
            ];
        });
        // radiance +4 good
        {
            var radianceGoodEnch = Utils.GetBlueprint<BlueprintWeaponEnchantment>("119b0b2ddae69d4438e6a4bedff32412");
            var yaniel_Longsword4HolyAvengerGuid = "0ff011d62af77e9428e12ac08f63709e";
            var yaniel_Longsword4HolyAvengerSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword4HolyAvengerGuid);
            var yaniel_Longsword4HolyAvengerWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(yaniel_Longsword4HolyAvengerGuid, bp =>
            {
                HandwrapsAdder.SetFields(bp);
                bp.m_DisplayNameText = yaniel_Longsword4HolyAvengerSword.m_DisplayNameText;
                bp.m_DescriptionText = yaniel_Longsword4HolyAvengerSword.m_DescriptionText;
                bp.m_FlavorText = yaniel_Longsword4HolyAvengerSword.m_FlavorText;
                bp.m_Cost = yaniel_Longsword4HolyAvengerSword.m_Cost;
                bp.m_Enchantments = [
                    handwrapsData.enhancementEnchs[3],
                    handwrapsData.coldIronEnch,
                    radianceGoodEnch.ToReference<BlueprintEquipmentEnchantmentReference>()
                ];
                bp.SpendCharges = false;
                bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
            });

            var radianceGoodBuff = Utils.GetBlueprint<BlueprintBuff>("f10cba2c41612614ea28b5fc2743bc4c");
            var buffC = radianceGoodBuff.GetComponent<BuffEnchantSpecificWeaponWorn>();
            if (buffC != null)
            {
                radianceGoodBuff.RemoveComponents<BuffEnchantSpecificWeaponWorn>();
                radianceGoodBuff.AddComponent<BuffEnchantUnarmed>(c =>
                {
                    c.m_EnchantmentBlueprint = buffC.m_EnchantmentBlueprint;
                });
            }
        }
        // radiance +6 good
        {
            var radianceGood6Ench = Utils.GetBlueprint<BlueprintWeaponEnchantment>("431f0d8a21b9469d946f5ae50f5b126e");
            var yaniel_Longsword6HolyAvengerGuid = "cf5c1a507825f184dacbc3abe14b9db1";
            var yaniel_Longsword6HolyAvengerSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword6HolyAvengerGuid);
            var yaniel_Longsword6HolyAvengerWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(yaniel_Longsword6HolyAvengerGuid, bp =>
            {
                HandwrapsAdder.SetFields(bp);
                bp.m_DisplayNameText = yaniel_Longsword6HolyAvengerSword.m_DisplayNameText;
                bp.m_DescriptionText = yaniel_Longsword6HolyAvengerSword.m_DescriptionText;
                bp.m_FlavorText = yaniel_Longsword6HolyAvengerSword.m_FlavorText;
                bp.m_Cost = yaniel_Longsword6HolyAvengerSword.m_Cost;
                bp.m_Enchantments = [
                    handwrapsData.enhancementEnchs[5],
                    handwrapsData.coldIronEnch,
                    radianceGood6Ench.ToReference<BlueprintEquipmentEnchantmentReference>()
                ];
                bp.SpendCharges = false;
                bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
            });

            var radianceGood6Buff = Utils.GetBlueprint<BlueprintBuff>("f10cba2c41612614ea28b5fc2743bc4c");
            var buff6C = radianceGood6Buff.GetComponent<BuffEnchantSpecificWeaponWorn>();
            if (buff6C != null)
            {
                radianceGood6Buff.RemoveComponents<BuffEnchantSpecificWeaponWorn>();
                radianceGood6Buff.AddComponent<BuffEnchantUnarmed>(c =>
                {
                    c.m_EnchantmentBlueprint = buff6C.m_EnchantmentBlueprint;
                });
            }
        }
        // radiance +4 bad
        {
            var radianceBadEnch = Utils.GetBlueprint<BlueprintWeaponEnchantment>("40433f161d052d74aa80769dcaf1df09");
            var yaniel_Longsword4BaneLivingGuid = "27bab9c65039ef54d8fc24d9641cbea0";
            var yaniel_Longsword4BaneLivingSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword4BaneLivingGuid);
            var yaniel_Longsword4BaneLivingWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(yaniel_Longsword4BaneLivingGuid, bp =>
            {
                HandwrapsAdder.SetFields(bp);
                bp.m_DisplayNameText = yaniel_Longsword4BaneLivingSword.m_DisplayNameText;
                bp.m_DescriptionText = yaniel_Longsword4BaneLivingSword.m_DescriptionText;
                bp.m_FlavorText = yaniel_Longsword4BaneLivingSword.m_FlavorText;
                bp.m_Cost = yaniel_Longsword4BaneLivingSword.m_Cost;
                bp.m_Enchantments = [
                    handwrapsData.enhancementEnchs[3],
                    handwrapsData.coldIronEnch,
                    radianceBadEnch.ToReference<BlueprintEquipmentEnchantmentReference>()
                ];
                bp.SpendCharges = false;
                bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
            });

            var radianceBadBuff = Utils.GetBlueprint<BlueprintBuff>("b894f848bf557df47aacb00f2463c8f9");
            var buffEC = radianceBadBuff.GetComponent<BuffEnchantSpecificWeaponWorn>();
            if (buffEC != null)
            {
                radianceBadBuff.RemoveComponents<BuffEnchantSpecificWeaponWorn>();
                radianceBadBuff.AddComponent<BuffEnchantUnarmed>(c =>
                {
                    c.m_EnchantmentBlueprint = buffEC.m_EnchantmentBlueprint;
                });
            }
        }
        // radiance +6 bad
        {
            var radianceBad6Ench = Utils.GetBlueprint<BlueprintWeaponEnchantment>("955dae09e1484fdda91bafe1ca336e0c");
            var yaniel_Longsword6BaneLivingGuid = "bff8a4bb7f24a2c499db0781b5750133";
            var yaniel_Longsword6BaneLivingSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword6BaneLivingGuid);
            var yaniel_Longsword6BaneLivingWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(yaniel_Longsword6BaneLivingGuid, bp =>
            {
                HandwrapsAdder.SetFields(bp);
                bp.m_DisplayNameText = yaniel_Longsword6BaneLivingSword.m_DisplayNameText;
                bp.m_DescriptionText = yaniel_Longsword6BaneLivingSword.m_DescriptionText;
                bp.m_FlavorText = yaniel_Longsword6BaneLivingSword.m_FlavorText;
                bp.m_Cost = yaniel_Longsword6BaneLivingSword.m_Cost;
                bp.m_Enchantments = [
                    handwrapsData.enhancementEnchs[5],
                    handwrapsData.coldIronEnch,
                    radianceBad6Ench.ToReference<BlueprintEquipmentEnchantmentReference>()
                ];
                bp.SpendCharges = false;
                bp.m_Ability = Utils.GetBlueprintReference<BlueprintAbilityReference>("e8d005d22023483ea00e7263579c43cb");
            });

            var radianceBadBuff6 = Utils.GetBlueprint<BlueprintBuff>("5eaf6612b68a48109555cf0cf6106f58");
            var buff6EC = radianceBadBuff6.GetComponent<BuffEnchantSpecificWeaponWorn>();
            if (buff6EC != null)
            {
                radianceBadBuff6.RemoveComponents<BuffEnchantSpecificWeaponWorn>();
                radianceBadBuff6.AddComponent<BuffEnchantUnarmed>(c =>
                {
                    c.m_EnchantmentBlueprint = buff6EC.m_EnchantmentBlueprint;
                });
            }
        }
    }
}