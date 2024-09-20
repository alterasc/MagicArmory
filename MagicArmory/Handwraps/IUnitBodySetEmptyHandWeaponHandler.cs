using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;

namespace MagicArmory.Handwraps;

interface IUnitBodySetEmptyHandWeaponHandler : ISubscriber, IGlobalSubscriber
{
    void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem);
}
