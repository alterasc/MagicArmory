using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;

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
        return item;
    }

    /// <summary>
    /// Recalculates character encumbrance if they have new unit part
    /// <see cref="UnitPartEncumbranceModifier"/>
    /// </summary>
    [HarmonyPatch(typeof(EncumbranceHelper), nameof(EncumbranceHelper.GetCarryingCapacity), [typeof(UnitDescriptor)])]
    private static class EncumbranceHelper_GetCarryingCapacity_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(UnitDescriptor unit, ref EncumbranceHelper.CarryingCapacity __result)
        {
            UnitPartEncumbranceModifier myunitPartAdditionalEncumbrance = unit.Get<UnitPartEncumbranceModifier>();
            if (myunitPartAdditionalEncumbrance == null || myunitPartAdditionalEncumbrance.AdditionalEncumbrance == 0)
            {
                return;
            }
            int heavy = EncumbranceHelper.GetHeavy(unit.Stats.Strength);
            heavy += myunitPartAdditionalEncumbrance.AdditionalEncumbrance;
            var unitPartAdditionalEncumbrance = unit.Get<UnitPartAdditionalEncumbrance>();
            int b = unitPartAdditionalEncumbrance != null ? unitPartAdditionalEncumbrance.AdditionalEncumbrance : 0;
            __result.Light = EncumbranceHelper.CombineWeight(EncumbranceHelper.GetLight(unit.Stats.Strength, heavy), b, 333333333);
            __result.Medium = EncumbranceHelper.CombineWeight(EncumbranceHelper.GetMedium(unit.Stats.Strength, heavy), b, 666666666);
            __result.Heavy = EncumbranceHelper.CombineWeight(heavy, b, 1000000000);
        }
    }
}
