using HarmonyLib;
using System;
using System.Reflection;
using UnityModManagerNet;

namespace MagicArmory;

#if DEBUG
[EnableReloading]
#endif
static class Main
{
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger log;
    internal static Guid ModNamespaceGuid;
    internal static SettingsModMenu Settings = new();

    static bool Load(UnityModManager.ModEntry modEntry)
    {
        log = modEntry.Logger;
        ModNamespaceGuid = NamespaceGuidUtils.CreateV5Guid(NamespaceGuidUtils.UrlNamespace, modEntry.Info.Id);
#if DEBUG
        modEntry.OnUnload = OnUnload;
#endif
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        return true;
    }

#if DEBUG
    static bool OnUnload(UnityModManager.ModEntry modEntry)
    {
        HarmonyInstance.UnpatchAll(modEntry.Info.Id);
        return true;
    }
#endif
}