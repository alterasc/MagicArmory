using HarmonyLib;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;

namespace MagicArmory.Handwraps;
internal class UnarmedPatches
{
    /// <summary>
    /// Raises equipment slot updated event when unit has empty hand weapon set
    /// </summary>
    [HarmonyPatch(typeof(UnitBody), nameof(UnitBody.SetEmptyHandWeapon))]
    private static class UnitBody_SetEmptyHandWeapon_Patch
    {
        [HarmonyPostfix]
        private static void AfterSetEmptyHandWeapon(UnitBody __instance)
        {
            ItemSlot slot = __instance.Owner.Body.PrimaryHand;
            ItemEntity item = slot.MaybeItem;
            EventBus.RaiseEvent(delegate (IUnitEquipmentHandler h)
            {
                h.HandleEquipmentSlotUpdated(slot, item);
            }, true);
        }
    }
}
