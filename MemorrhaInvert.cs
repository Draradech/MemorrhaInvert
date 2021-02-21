using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;

namespace MemorrhaInvert
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class MemorrhaInvertMain : BaseUnityPlugin
    {
        public const String PluginGuid = "draradech.memorrha.invert";
        public const String PluginName = "Invert Mouse";
        public const String PluginVersion = "0.0.1";

        private static BepInEx.Logging.ManualLogSource staticLogger;
        private static ConfigEntry<float> sensX;
        private static ConfigEntry<float> sensY;

        void Awake()
        {
            sensX = Config.Bind("", "Mouse Sensitivity X", 2.0f);
            sensY = Config.Bind("", "Mouse Sensitivity Y", -2.0f);
            staticLogger = Logger;

            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(UnityStandardAssets.Characters.FirstPerson.MouseLook), "LookRotation")]
        static class Patch_MouseLook_LookRotation
        {
            [HarmonyPrefix]
            static void Prefix(UnityStandardAssets.Characters.FirstPerson.MouseLook __instance)
            {
                __instance.XSensitivity = sensX.Value;
                __instance.YSensitivity = sensY.Value;
            }
        }
    }
}
