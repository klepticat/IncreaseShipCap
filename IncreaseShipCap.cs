using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace IncreaseShipCap
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class IncreaseShipCapPlugin : BaseUnityPlugin
    {
        static Harmony _harmony;

        public static ConfigEntry<int> configMaxItemCount;

        private void Awake()
        {
            configMaxItemCount = Config.Bind<int>("General",
                                                  "MaxItemCount",
                                                  200,
                                                  "Number of max items on the ship.");

            _harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(typeof(Patches));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Logger.LogInfo($"Ship Item Cap set to {configMaxItemCount.Value}");
        }

        

    }

    public class Patches {
        [HarmonyPatch(typeof(StartOfRound), "Awake")]
        static void Postfix(ref int ___maxShipItemCapacity) {
            ___maxShipItemCapacity = IncreaseShipCapPlugin.configMaxItemCount.Value - 1;
        }
    }
}
