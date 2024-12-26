using HarmonyLib;
using System;
using UnityModManagerNet;

namespace MagicArmory;

#if DEBUG
[EnableReloading]
#endif
public static class Main
{
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger log;
    internal static Guid ModNamespaceGuid;

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
        log = modEntry.Logger;
        ModNamespaceGuid = NamespaceGuidUtils.CreateV5Guid(NamespaceGuidUtils.UrlNamespace, modEntry.Info.Id);
#if DEBUG
        modEntry.OnUnload = OnUnload;
#endif
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        HarmonyInstance.CreateClassProcessor(typeof(Patches)).Patch();
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