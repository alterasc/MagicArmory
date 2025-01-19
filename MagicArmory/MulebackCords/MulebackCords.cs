using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.Designers.Mechanics.Facts;

namespace MagicArmory.MulebackCords;
internal class MulebackCords
{
    internal static BlueprintItemEquipmentWrist AddMulebackCords()
    {
        var features = Utils.CreateBlueprint<BlueprintFeature>("MulebackCordsFeature", bp =>
        {
            bp.HideInUI = true;
            bp.AddComponent<MulebackCordsComponent>();
            bp.AddComponent<RecalculateOnStatChange>(c =>
            {
                c.Stat = Kingmaker.EntitySystem.Stats.StatType.Strength;
            });
        });

        var ench = Utils.CreateBlueprint<BlueprintEquipmentEnchantment>("MulebackCordsEnchant", bp =>
        {
            bp.AddComponent<AddUnitFeatureEquipment>(c =>
            {
                c.m_Feature = features.ToReference<BlueprintFeatureReference>();
            });
        });

        var bracersofAnimalFury = Utils.GetBlueprint<BlueprintItemEquipmentWrist>("e845debb19b593d4fa794cb4f2fba9fa");
        var item = Utils.CreateBlueprint<BlueprintItemEquipmentWrist>("MulebackCordsItem", bp =>
        {
            bp.m_DisplayNameText = "Muleback Cords".ToLocalized();
            bp.m_DescriptionText = "These thick leather cords wrap around the wearer’s biceps and shoulders. When worn, they make the wearer’s muscles appear larger than normal. The wearer treats his Strength score as 8 higher than normal when determining his carrying capacity.".ToLocalized();
            bp.m_Icon = bracersofAnimalFury.m_Icon;
            bp.m_Cost = 1000;
            bp.m_Weight = 0.25f;
            bp.m_Destructible = true;
            bp.m_InventoryPutSound = "ArmorPlatePut";
            bp.m_InventoryTakeSound = "ArmorPlateTake";
            bp.m_InventoryEquipSound = "ArmorPlateEquip";
            bp.m_EquipmentEntity = bracersofAnimalFury.m_EquipmentEntity;
            bp.m_Enchantments = [
                ench.ToReference<BlueprintEquipmentEnchantmentReference>()
            ];
        });
        Main.log.Log("Added Muleback Cords");
        if (MagicArmorySettings.MulebackCords.Shops)
        {
            Vendors.Potions_DefendersHeartVendorTable.AddItem(item);
            Vendors.Tailor_Chapter3VendorTable.AddItem(item);
            Main.log.Log("Added Muleback Cords to shops");
        }
        return item;
    }
}
