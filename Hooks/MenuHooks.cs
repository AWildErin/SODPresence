using HarmonyLib;
using Rewired;
using SODPresence.Behaviours;
using SODPresence.Extensions;
using UnityEngine;

namespace SODPresence.Hooks;

public class MenuHooks
{
	private static GameObject mainMenuCanvas;
	public static GameObject MainMenuCanvas
	{
		get
		{
			if ( mainMenuCanvas == null )
			{
				mainMenuCanvas = GameObject.Find( "/MenuCanvas/MainMenu/" );
			}

			return mainMenuCanvas;
		}
	}

	[HarmonyPatch( typeof( MainMenuController ), "Start" )]
	public class MainMenuController_Start
	{
		// Create the DiscordManager gameobject.
		// Why here? I had some issues when doing it from within the plugin
		// Load() method, and it works here so..
		public static void Prefix()
		{
			GameObject go = new GameObject( "DiscordManager" );
			go.AddComponent<DiscordManager>();
			UnityEngine.Object.DontDestroyOnLoad( go );
		}

		public static void Postfix()
		{
			DiscordManager.Instance.ClearSettableProperties();
			DiscordManager.Instance.SetDetails( "In the Main Menu..." );
		}
	}
}
