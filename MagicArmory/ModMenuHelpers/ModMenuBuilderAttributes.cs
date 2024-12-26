using Kingmaker.Localization;
using System;

namespace MagicArmory.ModMenuHelpers;

[AttributeUsage(AttributeTargets.Class)]
public class ModMenuSettingsAttribute(string name, string rootKey) : Attribute
{
    public string name = name;
    public string rootKey = rootKey;

    public LocalizedString LocalizedName => name.ToLocalized();
}

[AttributeUsage(AttributeTargets.Class)]
public class SubHeaderAttribute(string name, bool expanded = false) : Attribute
{
    public string name = name;
    public bool expanded = expanded;

    public LocalizedString LocalizedName => name.ToLocalized();
}

[AttributeUsage(AttributeTargets.Field)]
public class ModMenuSettingAttribute(string name) : Attribute
{
    public string name = name;

    public LocalizedString LocalizedName => name.ToLocalized();
}

[AttributeUsage(AttributeTargets.Field)]
public class ToggleAttribute(string name, bool defaultValue) : ModMenuSettingAttribute(name)
{
    public bool defaultValue = defaultValue;
}

[AttributeUsage(AttributeTargets.Field)]
public class SliderIntAttribute(string name, int defaultValue, int minValue, int maxValue) : ModMenuSettingAttribute(name)
{
    public int defaultValue = defaultValue;
    public int minValue = minValue;
    public int maxValue = maxValue;
}
