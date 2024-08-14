using ModMenu.Settings;

namespace MagicArmory;
internal class SettingsModMenu
{
    internal static readonly string RootKey = "AlterAsc.MagicArmory".ToLower();

    public bool RadianceHandwraps => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("handwraps.radiance"));

    internal void Initialize()
    {
        ModMenu.ModMenu.AddSettings(
            SettingsBuilder
                .New(GetKey("title"), "Magic Armory".ToLocalized())
                    .AddSubHeader("Belt of Mighty Hurling".ToLocalized(), true)
                        .BuildMyToggle("beltofmightyhurling.enable", "Enable", true)
                        .BuildMyToggle("beltofmightyhurling.shops", "Add to shops", true)
                    .AddSubHeader("Muleback Cords".ToLocalized(), true)
                        .BuildMyToggle("mulebackcords.enable", "Enable", true)
                        .BuildMyToggle("mulebackcords.shops", "Add to shops", true)
                    .AddSubHeader("Boots of Levitation".ToLocalized(), true)
                        .BuildMyToggle("bootsoflevitation.enable", "Enable", true)
                        .BuildMyToggle("bootsoflevitation.shops", "Add to shops", true)
                        .BuildMyToggle("bootsoflevitation.enemies", "Add to enemies", true)
                    .AddSubHeader("Handwraps".ToLocalized(), true)
                        .BuildMyToggle("handwraps.enable", "Enable", true)
                        .BuildMyToggle("handwraps.shops", "Add to shops", true)
                        .BuildMyToggle("handwraps.finnean", "Finnean handwraps option", true)
                        .BuildMyToggle("handwraps.radiance", "Radiance handwraps", false)
        );
    }

    internal static string GetKey(string partialKey)
    {
        return $"{RootKey}.{partialKey}";
    }
}

internal static class ModMenuExtensions
{
    internal static SettingsBuilder BuildMyToggle(this SettingsBuilder builder, string partialKey, string description, bool defaultValue)
    {
        return builder.AddToggle(Toggle.New(SettingsModMenu.GetKey(partialKey), defaultValue, description.ToLocalized()));
    }
}