using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.UnitLogic.FactLogic;

namespace MagicArmory.BootsOfLevitation;
internal class BootsOfLevitationAdder
{
    internal static BlueprintItemEquipmentFeet Add()
    {
        var feature = Utils.CreateBlueprint<BlueprintFeature>("BootsOfLevitationFeature", bp =>
        {
            bp.HideInUI = true;
            bp.AddComponent<BuffDescriptorImmunity>(c =>
            {
                c.Descriptor = SpellDescriptor.Ground;
            });
            bp.AddComponent<SpellImmunityToSpellDescriptor>(c =>
            {
                c.Descriptor = SpellDescriptor.Ground;
            });
            bp.AddComponent<AddConditionImmunity>(c =>
            {
                c.Condition = Kingmaker.UnitLogic.UnitCondition.DifficultTerrain;
            });
            bp.AddComponent<AddMechanicsFeature>(c =>
            {
                c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.Flying;
            });
        });

        var ench = Utils.CreateBlueprint<BlueprintEquipmentEnchantment>("BootsOfLevitationEnchant", bp =>
        {
            bp.AddComponent<AddUnitFeatureEquipment>(c =>
            {
                c.m_Feature = feature.ToReference<BlueprintFeatureReference>();
            });
        });

        var bootsOfTheLightStepItem = Utils.GetBlueprint<BlueprintItemEquipmentFeet>("815cc85ce13ab64428253aea3b6708a8");

        var item = Utils.CreateBlueprint<BlueprintItemEquipmentFeet>("BootsOfLevitationItem", bp =>
        {
            bp.m_DisplayNameText = "Boots of Levitation".ToLocalized();
            bp.m_DescriptionText = "These boots grant the wearer immunity to difficult terrain and ground effects.".ToLocalized();
            bp.m_Icon = bootsOfTheLightStepItem.m_Icon;
            bp.m_Cost = 7500;
            bp.m_Weight = 1.0f;
            bp.m_Destructible = true;
            bp.m_InventoryPutSound = "BootPut";
            bp.m_InventoryTakeSound = "BootTake";
            bp.m_InventoryEquipSound = "BootPut";
            bp.m_EquipmentEntity = bootsOfTheLightStepItem.m_EquipmentEntity;
            bp.m_Enchantments = [
                ench.ToReference<BlueprintEquipmentEnchantmentReference>()
            ];
        });

        return item;
    }
}
