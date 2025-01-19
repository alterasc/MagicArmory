using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;

namespace MagicArmory;
public static class Vendors
{
    public static class Refs
    {
        public static BlueprintSharedVendorTableReference potions_DefendersHeartVendorTable = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("fc312b3b4e355a842815b5c519924ef7");
        public static BlueprintSharedVendorTableReference warCamp_QuartermasterVendorTable = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("5753b6f35e7db234aa44085a358c27af");
        public static BlueprintSharedVendorTableReference tailor_Chapter3VendorTable = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("9b80d696f04aaef4cb4e13eadcc66731");
    }
    public static BlueprintSharedVendorTable Potions_DefendersHeartVendorTable => Refs.potions_DefendersHeartVendorTable.Get();
    public static BlueprintSharedVendorTable WarCamp_QuartermasterVendorTable => Refs.warCamp_QuartermasterVendorTable.Get();
    public static BlueprintSharedVendorTable Tailor_Chapter3VendorTable => Refs.tailor_Chapter3VendorTable.Get();
}
