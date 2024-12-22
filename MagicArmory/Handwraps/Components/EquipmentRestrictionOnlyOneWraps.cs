using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using System.Linq;

namespace MagicArmory.Handwraps.Components;

/// <summary>
/// Limits unit to having only one item with this component
/// </summary>
[TypeId("31c157bfd0e847cabcee46f1eff95787")]
public class EquipmentRestrictionOnlyOneWraps : EquipmentRestriction
{
    public override bool CanBeEquippedBy(UnitDescriptor unit)
    {
        return unit.Body.AllSlots
            .Where(slot => slot.HasItem)
            .Select(slot => slot?.Item?.Blueprint)
            .OfType<BlueprintItemEquipmentUsable>()
            .All(bp => !bp.Components.Any(c => c is EquipmentRestrictionOnlyOneWraps));
    }
}