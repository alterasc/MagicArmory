using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Items.Weapons;
using static MagicArmory.Handwraps.HandwrapsAdder;

namespace MagicArmory.Handwraps.Uniques;

internal static class Finnean
{
    internal static void CreateFinnean(HandwrapsData handwrapsData)
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
            bp.m_Enchantments = [
                    handwrapsData.coldIronEnch,
                    handwrapsData.enhancementEnchs[0],
                    handwrapsData.ghostTouchEnch
            ];
        });

        var finneanWorkWraps2 = CreateHandwraps("FinneanHandwrapsStage2", bp =>
        {
            bp.m_DisplayNameText = finneanLongswordStage2.m_DisplayNameText;
            bp.m_DescriptionText = finneanLongswordStage2.m_DescriptionText;
            bp.m_FlavorText = finneanLongswordStage2.m_FlavorText;
            bp.m_Cost = 0;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[2],
                handwrapsData.ghostTouchEnch,
                handwrapsData.heartSeekerEnch,
                handwrapsData.finneanChapter3Ench
            ];
        });

        var finneanWorkWraps3Base = CreateHandwraps("FinneanHandwrapsStage3Base", bp =>
        {
            bp.m_DisplayNameText = finneanLongswordStage3Base.m_DisplayNameText;
            bp.m_DescriptionText = finneanLongswordStage3Base.m_DescriptionText;
            bp.m_FlavorText = finneanLongswordStage3Base.m_FlavorText;
            bp.m_Cost = 0;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[4],
                handwrapsData.brilliantEnergyEnch,
                handwrapsData.heartSeekerEnch,
                ];
        });

        var finneanWorkWraps3Lich = CreateHandwraps("FinneanHandwrapsStage3Lich", bp =>
        {
            bp.m_DisplayNameText = finneanLongswordStage3Lich.m_DisplayNameText;
            bp.m_DescriptionText = finneanLongswordStage3Lich.m_DescriptionText;
            bp.m_FlavorText = finneanLongswordStage3Lich.m_FlavorText;
            bp.m_Cost = 0;
            bp.m_Enchantments = [
                handwrapsData.enhancementEnchs[4],
                handwrapsData.brilliantEnergyEnch
            ];
        });

        if (Main.Settings.FinneanHandwraps)
        {
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
    }
}
