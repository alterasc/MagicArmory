using MagicArmory.ModMenuHelpers;

namespace MagicArmory;

[ModMenuSettings("Magic Armory", "alterasc.magicarmory")]
public static class MagicArmorySettings
{
    [SubHeader("Belt of Mighty Hurling", true)]
    public static class BeltofMightyHurling
    {
        [Toggle("Enable", true)]
        public static bool Enabled;
        [Toggle("Add to shops", true)]
        public static bool Shops;
    }

    [SubHeader("Muleback Cords", true)]
    public static class MulebackCords
    {
        [Toggle("Enable", true)]
        public static bool Enabled;
        [Toggle("Add to shops", true)]
        public static bool Shops;
    }

    [SubHeader("Boots of Levitation", true)]
    public static class BootsofLevitation
    {
        [Toggle("Enable", true)]
        public static bool Enabled;
        [Toggle("Add to shops", true)]
        public static bool Shops;
    }

    [SubHeader("Handwraps", true)]
    public static class Handwraps
    {
        [Toggle("Enable", true)]
        public static bool Enabled;
        [Toggle("Add to shops", true)]
        public static bool Shops;
        [Toggle("Finnean handwraps option", true)]
        public static bool Finnean;
        [Toggle("Radiance handwraps", true)]
        public static bool Radiance;
    }
}
