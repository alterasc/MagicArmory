using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;

namespace MagicArmory.BeltOfMightyHurling;
internal class BeltOfMightyHurling
{
    internal static BlueprintItemEquipmentBelt AddBeltOfMightyHurling()
    {
        var features = Utils.CreateBlueprint<BlueprintFeature>("BeltofMightyHurlingLesserFeature", bp =>
        {
            bp.HideInUI = true;
            bp.AddComponent<AttackStatReplacementForWeaponGroup>(c =>
            {
                c.ReplacementStat = Kingmaker.EntitySystem.Stats.StatType.Strength;
                c.FighterGroupFlag = Kingmaker.Blueprints.Items.Weapons.WeaponFighterGroupFlags.Thrown;
            });
        });

        var ench = Utils.CreateBlueprint<BlueprintEquipmentEnchantment>("BeltofMightyHurlingLesserEnchant", bp =>
        {
            bp.AddComponent<AddUnitFeatureEquipment>(c =>
            {
                c.m_Feature = features.ToReference<BlueprintFeatureReference>();
            });
        });

        var cordofStubbornFury = Utils.GetBlueprint<BlueprintItemEquipmentBelt>("3b7820965662afe4ab17627103e44228");
        var item = Utils.CreateBlueprint<BlueprintItemEquipmentBelt>("BeltofMightyHurlingLesserItem", bp =>
        {
            bp.m_DisplayNameText = "Lesser Belt of Mighty Hurling".ToLocalized();
            bp.m_DescriptionText = "This thick leather belt is buckled with a bright bronze clasp in the shape of a fist.\r\n\r\nWhen worn, it grants its wearer a +2 enhancement bonus to Strength and allows him to apply his Strength modifier as a bonus on attack rolls instead of his Dexterity modifier when making ranged attacks with thrown weapons.".ToLocalized();
            bp.m_Icon = cordofStubbornFury.m_Icon;
            bp.m_Cost = 14000;
            bp.m_Weight = 1.0f;
            bp.m_Destructible = true;
            bp.m_InventoryPutSound = "BeltPut";
            bp.m_InventoryTakeSound = "BeltTake";
            bp.m_InventoryEquipSound = "BeltPut";
            bp.m_EquipmentEntity = cordofStubbornFury.m_EquipmentEntity;
            bp.m_Enchantments = [
                ench.ToReference<BlueprintEquipmentEnchantmentReference>(),
                // Strength + 2 enchantment
                Utils.GetBlueprintReference<BlueprintEquipmentEnchantmentReference>("5f25b5f8c89d40c46ba8026bd7b73dcc")
            ];
        });
        Main.log.Log("Added Lesser Belt of Mighty Hurling");
        if (MagicArmorySettings.BeltofMightyHurling.Shops)
        {
            Vendors.WarCamp_QuartermasterVendorTable.AddItem(item);
            Main.log.Log("Added Lesser Belt of Mighty Hurling to shops");
        }
        return item;
    }
}
