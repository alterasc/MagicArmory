using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using static MagicArmory.MulebackCords.MulebackCordsComponent;

namespace MagicArmory.MulebackCords;

/// <summary>
/// Component for Muleback Cords that increases amount of carrying weight as if wearer strength is 8 higher
/// </summary>
[TypeId("824a19ba6e1a4ac9be59b798359f7c26")]
public class MulebackCordsComponent : UnitFactComponentDelegate<MulebackCordsComponentBonusData>
{
    public override void OnTurnOn()
    {
        var str = Owner.Stats.Strength;
        var add = EncumbranceHelper.GetHeavy(str + 8) - EncumbranceHelper.GetHeavy(str);
        Data.Value = add;
        Owner.Ensure<UnitPartEncumbranceModifier>().AdditionalEncumbrance += Data.Value;
    }

    public override void OnTurnOff()
    {
        UnitPartEncumbranceModifier unitPartAdditionalEncumbrance = Owner.Get<UnitPartEncumbranceModifier>();
        if (unitPartAdditionalEncumbrance != null && Data != null)
        {
            unitPartAdditionalEncumbrance.AdditionalEncumbrance -= Data.Value;
        }
    }

    public class MulebackCordsComponentBonusData
    {
        public int Value;
    }
}
