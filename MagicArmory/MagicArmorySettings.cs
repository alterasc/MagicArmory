using MagicArmory.ModMenuHelpers;

namespace MagicArmory;

[ModMenuSettings("Magic Armory", "alterasc.magicarmory")]
public static class MagicArmorySettings
{
    [SubHeader("Belt of Mighty Hurling", true)]
    public static class BeltofMightyHurling
    {
        [Toggle("Add to shops", true)]
        public static bool Shops;
    }

    [SubHeader("Muleback Cords", true)]
    public static class MulebackCords
    {
        [Toggle("Add to shops", true)]
        public static bool Shops;
    }

    [SubHeader("Boots of Levitation", true)]
    public static class BootsofLevitation
    {
        [Toggle("Add to shops", true)]
        public static bool Shops;
        [Toggle("Add to enemies", true)]
        public static bool Enemies;
    }

    [SubHeader("Handwraps", true)]
    public static class Handwraps
    {
        [Toggle("Add to shops", true)]
        public static bool Shops;
        [Toggle("Finnean handwraps option", true)]
        public static bool Finnean;
        [Toggle("Radiance handwraps", true)]
        public static bool Radiance;
    }
}
