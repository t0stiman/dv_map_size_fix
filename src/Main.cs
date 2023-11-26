using System;
using HarmonyLib;
using UnityModManagerNet;

namespace dv_map_size_fix
{
    [EnableReloading]
    static class Main
    {
        public static UnityModManager.ModEntry MyModEntry { get; private set; }
        public static Settings MySettings { get; private set; }
        private static Harmony MyHarmony;

        //================================================================

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                MyModEntry = modEntry;
                
                MySettings = UnityModManager.ModSettings.Load<Settings>(MyModEntry);
                MyModEntry.OnGUI = entry => MySettings.Draw(entry);
                MyModEntry.OnSaveGUI = entry => MySettings.Save(entry);
                
                MyHarmony = new Harmony(MyModEntry.Info.Id);
                MyHarmony.PatchAll();
                modEntry.OnUnload = Unload;
                MyModEntry.Logger.Log("loaded");
            }
            catch (Exception ex)
            {
                MyModEntry.Logger.LogException($"Failed to load {MyModEntry.Info.DisplayName}:", ex);
                MyHarmony?.UnpatchAll();
                return false;
            }

            return true;
        }

        private static bool Unload(UnityModManager.ModEntry modEntry)
        {
            MyHarmony?.UnpatchAll();
            return true;
        }

        //Logger functions
        public static void Log(string message)
        {
            MyModEntry.Logger.Log(message);
        }

        public static void LogWarning(string message)
        {
            MyModEntry.Logger.Warning(message);
        }

        public static void LogError(string message)
        {
            MyModEntry.Logger.Error(message);
        }
    }
}