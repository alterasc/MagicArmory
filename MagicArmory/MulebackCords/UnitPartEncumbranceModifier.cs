using HarmonyLib;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;

namespace MagicArmory.MulebackCords;
public class UnitPartEncumbranceModifier : OldStyleUnitPart
{
    public int AdditionalEncumbrance;
    private static bool PatchEnabled;

    public override void OnDidAttachToEntity()
    {
        base.OnDidAttachToEntity();
        if (!PatchEnabled)
        {
            Main.HarmonyInstance.CreateClassProcessor(typeof(EncumbranceHelper_GetCarryingCapacity_Patch)).Patch();
            Main.log.Log("UnitPartEncumbranceModifier patch applied");
            PatchEnabled = true;
        }
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


