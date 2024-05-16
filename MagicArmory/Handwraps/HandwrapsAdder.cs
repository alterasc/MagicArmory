using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;

namespace MagicArmory.Handwraps;
internal class HandwrapsAdder
{
    internal static List<BlueprintEquipmentEnchantment> CreateUnarmedOnlyEnhancements()
    {
        List<BlueprintWeaponEnchantment> weaponEnchantments = [
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("eb2faccc4c9487d43b3575d7e77ff3f5"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("80bb8a737579e35498177e1e3c75899b"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("783d7d496da6ac44f9511011fc5f1979"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("bdba267e951851449af552aa9f9e3992"),
            Utils.GetBlueprint<BlueprintWeaponEnchantment>("0326d02d2e24d254a9ef626cc7a3850f")
        ];

        List<BlueprintEquipmentEnchantment> enchantments = [];
        for (int i = 1; i <= 6; i++)
        {
            var ench = CreateUnarmedOnlyEnchantment($"MagicArmoryUnarmedEnhancement{i}", weaponEnchantments[i - 1]);
            enchantments.Add(ench);
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
        var handwrapsTypeString = Utils.CreateLocalizedString("MagicArmoryHandwrapsType", "Handwraps");
        // gloves of dueling
        var glovesIcon = Utils.GetBlueprint<BlueprintItemEquipmentGloves>("c33129949c59ee044900e741691a64d3").m_Icon;
        var scabbard_ArodensWrathItem = Utils.GetBlueprint<BlueprintItemEquipmentUsable>("c5eb4864c764a9945809525dda77acc8");

        void SetFields(BlueprintItemEquipmentUsable bp)
        {
            bp.m_NonIdentifiedNameText = handwrapsTypeString;
            bp.m_Cost = 1;
            bp.m_Weight = 1.0f;
            bp.m_Destructible = false;
            bp.m_MiscellaneousType = BlueprintItem.MiscellaneousItemType.None;
            bp.m_InventoryPutSound = scabbard_ArodensWrathItem.m_InventoryPutSound;
            bp.m_InventoryTakeSound = scabbard_ArodensWrathItem.m_InventoryTakeSound;
            bp.m_Icon = glovesIcon;
            bp.m_BeltItemPrefab = new();
            bp.Type = UsableItemType.Other;
        }

        BlueprintItemEquipmentUsable CreateHandwraps(string name, Action<BlueprintItemEquipmentUsable> init)
        {
            return Utils.CreateBlueprint<BlueprintItemEquipmentUsable>(name, bp =>
            {
                SetFields(bp);
                init?.Invoke(bp);
            });
        }

        var enchantments = CreateUnarmedOnlyEnhancements();
        var coldIronEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsColdIron", Utils.GetBlueprint<BlueprintWeaponEnchantment>("e5990dc76d2a613409916071c898eee8")).ToReference<BlueprintEquipmentEnchantmentReference>();
        var ghostTouchEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsGhostTouch", Utils.GetBlueprint<BlueprintWeaponEnchantment>("47857e1a5a3ec1a46adf6491b1423b4f")).ToReference<BlueprintEquipmentEnchantmentReference>();
        var heartSeekerEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsHeartseeker", Utils.GetBlueprint<BlueprintWeaponEnchantment>("e252b26686ab66241afdf33f2adaead6")).ToReference<BlueprintEquipmentEnchantmentReference>();
        var finneanChapter3Ench = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsFinneanChapter3", Utils.GetBlueprint<BlueprintWeaponEnchantment>("b183bd491793d194c9e4c96cd11769b1")).ToReference<BlueprintEquipmentEnchantmentReference>();
        var brilliantEnergyEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsBrilliantEnergy", Utils.GetBlueprint<BlueprintWeaponEnchantment>("66e9e299c9002ea4bb65b6f300e43770")).ToReference<BlueprintEquipmentEnchantmentReference>();
        var masterworkEnch = CreateUnarmedOnlyEnchantment("MagicArmoryHandwrapsMasterwork", Utils.GetBlueprint<BlueprintWeaponEnchantment>("6b38844e2bffbac48b63036b66e735be")).ToReference<BlueprintEquipmentEnchantmentReference>();

        var enchPlus1 = enchantments[0].ToReference<BlueprintEquipmentEnchantmentReference>();
        var enchPlus2 = enchantments[1].ToReference<BlueprintEquipmentEnchantmentReference>();
        var enchPlus3 = enchantments[2].ToReference<BlueprintEquipmentEnchantmentReference>();
        var enchPlus4 = enchantments[3].ToReference<BlueprintEquipmentEnchantmentReference>();
        var enchPlus5 = enchantments[4].ToReference<BlueprintEquipmentEnchantmentReference>();
        var enchPlus6 = enchantments[5].ToReference<BlueprintEquipmentEnchantmentReference>();

        #region Finnean
        {
            var finneanLongswordStage1 = Utils.GetBlueprint<BlueprintItemWeapon>("167234a9f905629468a7466fdbd5f89d");
            var finneanLongswordStage2 = Utils.GetBlueprint<BlueprintItemWeapon>("532f736a3e89b0047bf2507923a35732");
            var finneanLongswordStage3Base = Utils.GetBlueprint<BlueprintItemWeapon>("8e1c0fcdbb0fef34199f5f94fb42c2d6");
            var finneanLongswordStage3Lich = Utils.GetBlueprint<BlueprintItemWeapon>("9b198f6d0e13a9f4bbe66a26415f1d9a");

            var finneanWorkWraps1 = CreateHandwraps("FinneanHandwrapsStage1", bp =>
            {
                bp.m_DisplayNameText = finneanLongswordStage1.m_DisplayNameText;
                bp.m_DescriptionText = finneanLongswordStage1.m_DescriptionText;
                bp.m_FlavorText = finneanLongswordStage1.m_FlavorText;
                bp.m_Cost = 0;
                bp.m_Weight = 0f;
                bp.m_Enchantments = [
                    coldIronEnch,
                    enchPlus1,
                    ghostTouchEnch
                ];
            });

            var finneanWorkWraps2 = CreateHandwraps("FinneanHandwrapsStage2", bp =>
            {
                bp.m_DisplayNameText = finneanLongswordStage2.m_DisplayNameText;
                bp.m_DescriptionText = finneanLongswordStage2.m_DescriptionText;
                bp.m_FlavorText = finneanLongswordStage2.m_FlavorText;
                bp.m_Cost = 0;
                bp.m_Weight = 0f;
                bp.m_Enchantments = [
                    enchPlus3,
                    ghostTouchEnch,
                    heartSeekerEnch,
                    finneanChapter3Ench
                ];
            });

            var finneanWorkWraps3Base = CreateHandwraps("FinneanHandwrapsStage3Base", bp =>
            {
                bp.m_DisplayNameText = finneanLongswordStage3Base.m_DisplayNameText;
                bp.m_DescriptionText = finneanLongswordStage3Base.m_DescriptionText;
                bp.m_FlavorText = finneanLongswordStage3Base.m_FlavorText;
                bp.m_Cost = 0;
                bp.m_Weight = 0f;
                bp.m_Enchantments = [
                    enchPlus5,
                    brilliantEnergyEnch,
                    heartSeekerEnch,
                ];
            });

            var finneanWorkWraps3Lich = CreateHandwraps("FinneanHandwrapsStage3Lich", bp =>
            {
                bp.m_DisplayNameText = finneanLongswordStage3Lich.m_DisplayNameText;
                bp.m_DescriptionText = finneanLongswordStage3Lich.m_DescriptionText;
                bp.m_FlavorText = finneanLongswordStage3Lich.m_FlavorText;
                bp.m_Cost = 0;
                bp.m_Weight = 0f;
                bp.m_Enchantments = [
                    enchPlus5,
                    brilliantEnergyEnch
                ];
            });

            var finneanFlag0 = Utils.GetBlueprintReference<BlueprintUnlockableFlagReference>("e50ce85131b64acbb14dc1cade2434d0");
            var finneanFlag1 = Utils.GetBlueprintReference<BlueprintUnlockableFlagReference>("10f9d67c98bf4796831ea5aa99e580b3");
            var finneanFlag2 = Utils.GetBlueprintReference<BlueprintUnlockableFlagReference>("dce45f9c5e23496284a6b32a5b3f8a7f");
            var finneanFlag2Lich = Utils.GetBlueprintReference<BlueprintUnlockableFlagReference>("58dd746a84c54571998753d065684723");

            var finnean = Utils.GetBlueprint<BlueprintHiddenItem>("95c126deb99ba054aa5b84710520c035");
            foreach (var c in finnean.GetComponents<ItemPolymorph>())
            {
                if (c.m_FlagToCheck == null) continue;
                if (c.m_FlagToCheck.Equals(finneanFlag0))
                {
                    c.m_PolymorphItems.Add(finneanWorkWraps1.ToReference<BlueprintItemReference>());
                }
                else if (c.m_FlagToCheck.Equals(finneanFlag1))
                {
                    c.m_PolymorphItems.Add(finneanWorkWraps2.ToReference<BlueprintItemReference>());
                }
                else if (c.m_FlagToCheck.Equals(finneanFlag2))
                {
                    c.m_PolymorphItems.Add(finneanWorkWraps3Base.ToReference<BlueprintItemReference>());
                }
                else if (c.m_FlagToCheck.Equals(finneanFlag2Lich))
                {
                    c.m_PolymorphItems.Add(finneanWorkWraps3Lich.ToReference<BlueprintItemReference>());
                }
            }
        }
        #endregion

        #region Radiance
        {
            var radianceMasterWorkGuid = "3b2df06a731030d49a1240b763cb6069";
            var radianceMasterWorkSword = Utils.GetBlueprint<BlueprintItemWeapon>(radianceMasterWorkGuid);
            var radianceMasterWorkWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(radianceMasterWorkGuid, bp =>
            {
                SetFields(bp);
                bp.m_DisplayNameText = radianceMasterWorkSword.m_DisplayNameText;
                bp.m_DescriptionText = radianceMasterWorkSword.m_DescriptionText;
                bp.m_FlavorText = radianceMasterWorkSword.m_FlavorText;
                bp.m_Cost = radianceMasterWorkSword.m_Cost;
                bp.m_Enchantments = [
                    coldIronEnch,
                    masterworkEnch
                ];
            });

            var radiancePlus1ITemGuid = "de1fc233ad934a0a93a17ebed3ec0cfb";
            var radiancePlus1ITemSword = Utils.GetBlueprint<BlueprintItemWeapon>(radiancePlus1ITemGuid);
            var radiancePlus1ITemWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(radiancePlus1ITemGuid, bp =>
            {
                SetFields(bp);
                bp.m_DisplayNameText = radiancePlus1ITemSword.m_DisplayNameText;
                bp.m_DescriptionText = radiancePlus1ITemSword.m_DescriptionText;
                bp.m_FlavorText = radiancePlus1ITemSword.m_FlavorText;
                bp.m_Cost = radiancePlus1ITemSword.m_Cost;
                bp.m_Enchantments = [
                    enchPlus1,
                    coldIronEnch
                ];
            });

            var radiancePlus2Guid = "6a80e629e9a5ca74da1dabc2984bba3b";
            var radiancePlus2Sword = Utils.GetBlueprint<BlueprintItemWeapon>(radiancePlus2Guid);
            var radiancePlus2Wraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(radiancePlus2Guid, bp =>
            {
                SetFields(bp);
                bp.m_DisplayNameText = radiancePlus2Sword.m_DisplayNameText;
                bp.m_DescriptionText = radiancePlus2Sword.m_DescriptionText;
                bp.m_FlavorText = radiancePlus2Sword.m_FlavorText;
                bp.m_Cost = radiancePlus2Sword.m_Cost;
                bp.m_Enchantments = [
                    enchPlus2,
                    coldIronEnch
                ];
            });
            // radiance +4 good
            {
                var radianceGoodEnch = Utils.GetBlueprint<BlueprintWeaponEnchantment>("119b0b2ddae69d4438e6a4bedff32412");
                var yaniel_Longsword4HolyAvengerGuid = "0ff011d62af77e9428e12ac08f63709e";
                var yaniel_Longsword4HolyAvengerSword = Utils.GetBlueprint<BlueprintItemWeapon>(yaniel_Longsword4HolyAvengerGuid);
                var yaniel_Longsword4HolyAvengerWraps = Utils.ReplaceBlueprint<BlueprintItemEquipmentUsable>(yaniel_Longsword4HolyAvengerGuid, bp =>
                {
                    SetFields(bp);
                    bp.m_DisplayNameText = yaniel_Longsword4HolyAvengerSword.m_DisplayNameText;
                    bp.m_DescriptionText = yaniel_Longsword4HolyAvengerSword.m_DescriptionText;
                    bp.m_FlavorText = yaniel_Longsword4HolyAvengerSword.m_FlavorText;
                    bp.m_Cost = yaniel_Longsword4HolyAvengerSword.m_Cost;
                    bp.m_Enchantments = [
                        enchPlus4,
                        coldIronEnch,
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
                    SetFields(bp);
                    bp.m_DisplayNameText = yaniel_Longsword6HolyAvengerSword.m_DisplayNameText;
                    bp.m_DescriptionText = yaniel_Longsword6HolyAvengerSword.m_DescriptionText;
                    bp.m_FlavorText = yaniel_Longsword6HolyAvengerSword.m_FlavorText;
                    bp.m_Cost = yaniel_Longsword6HolyAvengerSword.m_Cost;
                    bp.m_Enchantments = [
                        enchPlus6,
                        coldIronEnch,
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
                    SetFields(bp);
                    bp.m_DisplayNameText = yaniel_Longsword4BaneLivingSword.m_DisplayNameText;
                    bp.m_DescriptionText = yaniel_Longsword4BaneLivingSword.m_DescriptionText;
                    bp.m_FlavorText = yaniel_Longsword4BaneLivingSword.m_FlavorText;
                    bp.m_Cost = yaniel_Longsword4BaneLivingSword.m_Cost;
                    bp.m_Enchantments = [
                        enchPlus4,
                        coldIronEnch,
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
                    SetFields(bp);
                    bp.m_DisplayNameText = yaniel_Longsword6BaneLivingSword.m_DisplayNameText;
                    bp.m_DescriptionText = yaniel_Longsword6BaneLivingSword.m_DescriptionText;
                    bp.m_FlavorText = yaniel_Longsword6BaneLivingSword.m_FlavorText;
                    bp.m_Cost = yaniel_Longsword6BaneLivingSword.m_Cost;
                    bp.m_Enchantments = [
                        enchPlus6,
                        coldIronEnch,
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
        #endregion

        #region Basic Enhancement version
        {
            var handwrapsItemLName = Utils.CreateLocalizedString("MagicArmoryHandwrapsName", "Handwraps");
            var handwrapsItemLDescription = Utils.CreateLocalizedString("MagicArmoryHandwrapsDescription", "Handwraps enchantments apply only to unarmed attacks.");
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
                        enchantments[i - 1].ToReference<BlueprintEquipmentEnchantmentReference>()
                    ];
                });
                normalHandwrapsList.Add(normalWorkWraps.ToReference<BlueprintItemEquipmentUsableReference>());
            }
        }
        #endregion
    }
}
