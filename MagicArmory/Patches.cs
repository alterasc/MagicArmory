using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.JsonSystem;
using MagicArmory.Handwraps;
using MagicArmory.ModMenuHelpers;

namespace MagicArmory;

[HarmonyPatch]
internal class Patches
{
    private static bool _initialized;

    [HarmonyPostfix]
    [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
    public static void Postfix()
    {
        if (_initialized) return;
        _initialized = true;

        ModMenuSettingsGenerator.Generate();

        var mulebackCord = MulebackCords.MulebackCords.AddMulebackCords();
        var beltofHurling = BeltOfMightyHurling.BeltOfMightyHurling.AddBeltOfMightyHurling();
        var bootsOfLevitation = BootsOfLevitation.BootsOfLevitationAdder.Add();

        var Potions_DefendersHeartVendorTable = Utils.GetBlueprint<BlueprintSharedVendorTable>("fc312b3b4e355a842815b5c519924ef7");
        Potions_DefendersHeartVendorTable.AddItem(mulebackCord);

        var WarCamp_QuartermasterVendorTable = Utils.GetBlueprint<BlueprintSharedVendorTable>("5753b6f35e7db234aa44085a358c27af");
        WarCamp_QuartermasterVendorTable.AddItem(beltofHurling);

        var Tailor_Chapter3VendorTable = Utils.GetBlueprint<BlueprintSharedVendorTable>("9b80d696f04aaef4cb4e13eadcc66731");
        Tailor_Chapter3VendorTable.AddItem(mulebackCord);

        Tailor_Chapter3VendorTable.AddItem(bootsOfLevitation);

        var addToEnemies = false;

        if (addToEnemies)
        {
            // add boots of levitation to Zacharius (all versions) and Alderpash
            var bootsRef = bootsOfLevitation.ToReference<BlueprintItemEquipmentFeetReference>();
            Utils.ApplyForAll<BlueprintUnit>(
                [
                    //Zacharius
                    "e006d3f1b8e45ec4587358aa941409b7",
                    "d67527d9c2246734baffc69808d40b01",
                    "b1c89ced808048c1b9d7e328112ab067",
                    "babbc46e076b78f408d8d87643daea3e",
                    //Alderpash
                    "72bc5adfe506b6f4ba419963b036a553"
                ],
                a => a.Body.m_Feet = bootsRef
            );
        }
        HandwrapsAdder.CreateEnchantmentsAndHandwraps();
    }
}
