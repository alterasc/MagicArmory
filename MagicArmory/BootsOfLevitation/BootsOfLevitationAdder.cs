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
        Main.log.Log("Added Boots of Levitation");
        if (MagicArmorySettings.BootsofLevitation.Shops)
        {
            Vendors.Tailor_Chapter3VendorTable.AddItem(item);
            Main.log.Log("Added Boots of Levitation to shops");
        }
        if (MagicArmorySettings.BootsofLevitation.Enemies)
        {
            // add boots of levitation to Zacharius (all versions) and Alderpash
            var bootsRef = item.ToReference<BlueprintItemEquipmentFeetReference>();
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
            Main.log.Log("Added Boots of Levitation to enemies");
        }

        return item;
    }
}
