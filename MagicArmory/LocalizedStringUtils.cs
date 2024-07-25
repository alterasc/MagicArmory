using Kingmaker.Localization;
using System.Collections.Generic;

namespace MagicArmory;

public static class LocalizedStringUtils
{
    internal static IDictionary<string, LocalizedString> modStrings = new Dictionary<string, LocalizedString>();
    private static LocalizedString CreateLocalizedString(string key, string value)
    {
        if (modStrings.TryGetValue(value, out var existingKey))
        {
            return existingKey;
        }
        else
        {
            var localizedString = new LocalizedString() { m_Key = key };
            LocalizationManager.CurrentPack.PutString(key, value);
            modStrings[value] = localizedString;
            return localizedString;
        }
    }

    public static LocalizedString ToLocalized(this string str)
    {
        var key = NamespaceGuidUtils.CreateV5Guid(Main.ModNamespaceGuid, str).ToString();
        return CreateLocalizedString(key, str);
    }
}