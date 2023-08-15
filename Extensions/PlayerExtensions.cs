using System;
using UnityEngine;

namespace SODPresence.Extensions;

public static class PlayerExtensions
{
	/// <summary>
	/// Gets the player game object
	/// </summary>
	/// <returns>Player if found</returns>
	/// TODO: so Player.Instance exists, get rid of all of this eventually.
	public static Player GetPlayer()
	{
		return Player.Instance;
	}

	/// <summary>
	/// Gets the players location, does some additional checks
	/// in case we're in just "path" or "street-side pavement" locations
	/// </summary>
	public static string GetFriendlyLocation( this Player player )
	{
		var building = player.currentBuilding;
		var location = player.currentGameLocation;
		var prevBuilding = player.previousBuilding;
		var prevLocation = player.previousGameLocation;

		string friendlyLocation = location?.name;

		if ( (friendlyLocation == "Street-side pavement" || friendlyLocation == "Yard") && prevLocation != null )
		{
			string prevLocName = prevLocation.name;

			// Lets cut out places like "Smith Cascades Ground floor lobby" and use the
			// previous building name instead
			if ( prevLocName.Contains( "lobby", StringComparison.OrdinalIgnoreCase ) && building != null )
			{
				prevLocName = building?.name;
			}

			friendlyLocation += $" near {prevLocName}";
		}
		else if ( friendlyLocation == "Path" && building != null )
		{
			friendlyLocation = building.name;
		}

		return friendlyLocation;
	}

	public static Case GetActiveCase(this Player player )
	{
		CasePanelController caseController = CasePanelController.Instance;
		if (caseController == null || caseController.activeCase == null)
		{
			return null;
		}

		return caseController.activeCase;
	}
}
