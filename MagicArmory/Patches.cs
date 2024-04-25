using HarmonyLib;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.JsonSystem;

namespace MagicArmory;
internal class Patches
{
    [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
    internal static class BlueprintInitPatch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            var mulebackCord = MulebackCords.MulebackCords.AddMulebackCords();
            var beltofHurling = BeltOfMightyHurling.BeltOfMightyHurling.AddBeltOfMightyHurling();

            var Potions_DefendersHeartVendorTable = Utils.GetBlueprint<BlueprintSharedVendorTable>("fc312b3b4e355a842815b5c519924ef7");
            Potions_DefendersHeartVendorTable.AddItem(mulebackCord);

            var WarCamp_QuartermasterVendorTable = Utils.GetBlueprint<BlueprintSharedVendorTable>("5753b6f35e7db234aa44085a358c27af");
            WarCamp_QuartermasterVendorTable.AddItem(beltofHurling);

            var Tailor_Chapter3VendorTable = Utils.GetBlueprint<BlueprintSharedVendorTable>("9b80d696f04aaef4cb4e13eadcc66731");
            Tailor_Chapter3VendorTable.AddItem(mulebackCord);
        }
    }
}
