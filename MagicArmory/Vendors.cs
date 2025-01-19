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
        public static BlueprintSharedVendorTableReference jeweler_Chapter3VendorTable = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("9f959fcff8d929042b1be6311d209580");
        public static BlueprintSharedVendorTableReference jeweler_Chapter5VendorTable = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("fad4f5653f1d4943ad21a2dd85ee746b");
        public static BlueprintSharedVendorTableReference rvanyVendorTable = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("8edcc85ae155a3f45947d09876634131");
        public static BlueprintSharedVendorTableReference dLC3_VendorTable_Equipment = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("195579adaa20483ca3aad66bb2b06f8f");
        public static BlueprintSharedVendorTableReference jeweler_DLC1VendorTable = Utils.GetBlueprintReference<BlueprintSharedVendorTableReference>("3f08f2fcaed14d989ff1230fb214f1fb");
    }
    public static BlueprintSharedVendorTable Potions_DefendersHeartVendorTable => Refs.potions_DefendersHeartVendorTable.Get();
    public static BlueprintSharedVendorTable WarCamp_QuartermasterVendorTable => Refs.warCamp_QuartermasterVendorTable.Get();
    public static BlueprintSharedVendorTable Tailor_Chapter3VendorTable => Refs.tailor_Chapter3VendorTable.Get();
    public static BlueprintSharedVendorTable Jeweler_Chapter3VendorTable => Refs.jeweler_Chapter3VendorTable.Get();
    public static BlueprintSharedVendorTable Jeweler_Chapter5VendorTable => Refs.jeweler_Chapter5VendorTable.Get();
    public static BlueprintSharedVendorTable RvanyVendorTable => Refs.rvanyVendorTable.Get();
    public static BlueprintSharedVendorTable DLC3_VendorTable_Equipment => Refs.dLC3_VendorTable_Equipment.Get();
    public static BlueprintSharedVendorTable Jeweler_DLC1VendorTable => Refs.jeweler_DLC1VendorTable.Get();
}
