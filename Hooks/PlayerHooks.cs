using HarmonyLib;
using SODPresence.Behaviours;
using SODPresence.Extensions;
using System.Linq;
using UnityEngine;

namespace SODPresence.Hooks;

public class PlayerHooks
{
	public static bool IsPaused { get; private set; }

	[HarmonyPatch( typeof( Player ), "Update" )]
	public class Player_Update
	{
		public static void Postfix()
		{
			Player player = PlayerExtensions.GetPlayer();

			if ( player.name == "FPSController" || MenuHooks.MainMenuCanvas.active )
			{
				return;
			}

			GameplayController gameplay = GameplayController.Instance;

			string state = player.name;
			if ( gameplay != null )
			{
				state += $" | Social Credit: {gameplay.GetCurrentSocialCreditLevel()}";
			}

			// Player information
			DiscordManager.Instance.SetDetails( state );
			DiscordManager.Instance.SetState( player.GetFriendlyLocation() );

			// Case information
			var cityData = CityData.Instance;
			if ( cityData != null )
			{
				DiscordManager.Instance.SetSmallImageKey( "iconagent" );
				DiscordManager.Instance.SetSmallImageText( GetSmallImageText() );
			}
		}

		private static string GetSmallImageText()
		{
			Case activeCase = Player.Instance?.GetActiveCase();
			string cityName = CityData.Instance?.cityName;

			// Escape early if we're custom or null.
			// TODO: I'm not sure if the switch will fall through to default if activeCase is 
			// null, but if it does we can probably remove this.
			if ( activeCase == null )
			{
				return $"Patrolling the streets of {cityName}";
			}

			string text = "";
			switch ( activeCase?.caseType )
			{
				case Case.CaseType.murder:
					text = $"Solving the case of the ${activeCase.name} in {cityName}";
					break;

				case Case.CaseType.sideJob:
					text = $"Completing a side job in {cityName}";
					break;

				default:
					text = $"Patrolling the streets of {cityName}";
					break;
			}

			return text;
		}
	}
}
