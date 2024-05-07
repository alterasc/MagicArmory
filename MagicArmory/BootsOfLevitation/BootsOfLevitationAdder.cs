using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.FactLogic;

namespace MagicArmory.BootsOfLevitation;
internal class BootsOfLevitationAdder
{
    internal static BlueprintItemEquipmentWrist Add()
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
        });

        var featurefactref = feature.ToReference<BlueprintUnitFactReference>();

        // immunity to pits has to be given manually for each ability       
        Utils.ApplyForAll<Kingmaker.UnitLogic.Abilities.Blueprints.BlueprintAbilityAreaEffect>(
            [
                "b905a3c987f22cb49a246f0ab211f34c", // PitOfDespairArea
                "bf68ec704dc186549a7c6fbf22d3d661", // TricksterRecreationalPitArea
                "cf742a1d377378e4c8799f6a3afff1ba", // CreatePitArea
                "beccc33f543b1f8469c018982c23ac06", // SpikedPitArea
                "e122151e93e44e0488521aed9e51b617", // AcidPitArea
                "d086b1aeb367a5b43808d34c321955d1", // HungryPitArea
                "9b51157a5305dbf4184bf15bdad39226", // RiftOfRuinArea
            ],
            bp =>
            {
                var pitComp = bp.GetComponent<AreaEffectPit>();
                if (pitComp != null)
                {
                    pitComp.m_EffectsImmunityFacts = pitComp.m_EffectsImmunityFacts.AppendToArray(featurefactref);
                }
            });

        var ench = Utils.CreateBlueprint<BlueprintEquipmentEnchantment>("BootsOfLevitationEnchant", bp =>
        {
            bp.AddComponent<AddUnitFeatureEquipment>(c =>
            {
                c.m_Feature = feature.ToReference<BlueprintFeatureReference>();
            });
        });

        var bootsOfTheLightStepItem = Utils.GetBlueprint<BlueprintItemEquipmentFeet>("815cc85ce13ab64428253aea3b6708a8");

        var item = Utils.CreateBlueprint<BlueprintItemEquipmentWrist>("BootsOfLevitationItem", bp =>
        {
            bp.m_DisplayNameText = Utils.CreateLocalizedString($"{bp.name}Title", "Boots of Levitation");
            bp.m_DescriptionText = Utils.CreateLocalizedString($"{bp.name}Description", "These boots grant the wearer immunity to difficult terrain, ground effects and pits.");
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
