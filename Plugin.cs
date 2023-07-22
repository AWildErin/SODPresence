using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace SODPresence;

[BepInPlugin( MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION )]
public class Plugin : BasePlugin
{
	public static ManualLogSource Logger;

	public override void Load()
	{
		Logger = Log;

		// Plugin startup logic
		Log.LogInfo( $"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!" );
	}
}
