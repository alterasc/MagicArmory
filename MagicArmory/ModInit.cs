using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using MagicArmory.Handwraps;
using MagicArmory.ModMenuHelpers;

namespace MagicArmory;

[HarmonyPatch]
internal class ModInit
{
    private static bool _initialized;

    [HarmonyPostfix]
    [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
    public static void AfterBlueprintCreation()
    {
        if (_initialized) return;
        _initialized = true;

        ModMenuSettingsGenerator.Generate();

        MulebackCords.MulebackCords.AddMulebackCords();
        BeltOfMightyHurling.BeltOfMightyHurling.AddBeltOfMightyHurling();
        BootsOfLevitation.BootsOfLevitationAdder.Add();
        HandwrapsAdder.CreateEnchantmentsAndHandwraps();
    }
}
