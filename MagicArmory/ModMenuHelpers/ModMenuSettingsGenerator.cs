using ModMenu.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MagicArmory.ModMenuHelpers;

public static class ModMenuSettingsGenerator
{
    public static void Generate()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var modSettings = assembly.GetTypes()
            .Where(x => x.IsClass)
            .Where(x => x.GetCustomAttribute<ModMenuSettingsAttribute>(false) != null);

        foreach (var modSetting in modSettings)
        {
            List<Action> actions = [];
            var rootAttribute = modSetting.GetCustomAttributes<ModMenuSettingsAttribute>(false).First();

            var builder = SettingsBuilder.New(rootAttribute.rootKey, rootAttribute.LocalizedName);

            var fields = modSetting.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var field in fields)
            {
                GenerateSetting(actions, rootAttribute.rootKey, builder, field);
            }
            var subHeaders = modSetting.GetNestedTypes(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var subHeader in subHeaders)
            {
                var subheaderAttr = subHeader.GetCustomAttribute<SubHeaderAttribute>(false);
                if (subheaderAttr == null)
                {
                    continue;
                }
                builder.AddSubHeader(subheaderAttr.LocalizedName, subheaderAttr.expanded);
                var keyPrefix = rootAttribute.rootKey + "." + subHeader.Name.ToLowerInvariant();
                var subFields = subHeader.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var field in subFields)
                {
                    GenerateSetting(actions, keyPrefix, builder, field);
                }
            }
            ModMenu.ModMenu.AddSettings(builder);
            actions.ForEach(a => a());
        }
    }

    private static void GenerateSetting(List<Action> actions, string keyPrefix, SettingsBuilder builder, FieldInfo field)
    {
        var settingAttr = field.GetCustomAttribute<ModMenuSettingAttribute>();
        var fieldLowerName = field.Name.ToLowerInvariant();
        if (settingAttr == null)
        {
            return;
        }
        string settingKey = $"{keyPrefix}.{fieldLowerName}";
        if (settingAttr is ToggleAttribute toggleAttribute)
        {
            var setting = Toggle.New(settingKey, toggleAttribute.defaultValue, toggleAttribute.LocalizedName);
            setting.OnValueChanged(a =>
            {
                field.SetValue(null, a);
            });
            builder.AddToggle(setting);
            actions.Add(() =>
            {
                field.SetValue(null, ModMenu.ModMenu.GetSettingValue<bool>(settingKey));
            });
        }
        else if (settingAttr is SliderIntAttribute sliderIntAttribute)
        {
            var setting = SliderInt.New(settingKey, sliderIntAttribute.defaultValue, sliderIntAttribute.LocalizedName, sliderIntAttribute.minValue, sliderIntAttribute.maxValue);
            setting.OnValueChanged(a =>
            {
                field.SetValue(null, a);
            });
            builder.AddSliderInt(setting);
            actions.Add(() =>
            {
                field.SetValue(null, ModMenu.ModMenu.GetSettingValue<int>(settingKey));
            });
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}