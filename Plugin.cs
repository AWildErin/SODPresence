using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using DiscordRPC;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Il2CppSystem.Net;
using SODPresence.Behaviours;
using System;
using UnityEngine;

namespace SODPresence;

[BepInPlugin( MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION )]
public class Plugin : BasePlugin
{
	public static ManualLogSource Logger;

	public static GameObject DiscordManagerObject { get; private set; }
	public static DiscordManager DiscordManager { get; private set; }

	public override void Load()
	{
		Logger = Log;

		// Plugin startup logic
		Log.LogInfo( $"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!" );

		ClassInjector.RegisterTypeInIl2Cpp<DiscordManager>();

#if DEBUG
		ClassInjector.RegisterTypeInIl2Cpp<DiscordDebugUI>();
#endif

		var harmony = new Harmony( $"{MyPluginInfo.PLUGIN_GUID}" );
		harmony.PatchAll();

		createDiscordManager();
	}

	public override bool Unload()
	{
		return true;
	}

	private void createDiscordManager()
	{

	}
}
