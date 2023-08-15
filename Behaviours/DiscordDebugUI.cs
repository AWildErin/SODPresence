using SODPresence.Extensions;
using SODPresence.Hooks;
using UnityEngine;

namespace SODPresence.Behaviours;

#if DEBUG
/// <summary>
/// A general debug UI for testing the Discord rich presence.
/// </summary>
public class DiscordDebugUI : MonoBehaviour
{
	private bool showUI = false;

	private Player player = null;
	private GameplayController gameplay = null;
	private DiscordManager discord = null;

	private void Start()
	{
		player = PlayerExtensions.GetPlayer();
		gameplay = GameplayController.Instance;
		discord = DiscordManager.Instance;
	}

	private void Update()
	{
		if ( Input.GetKeyDown( KeyCode.F2 ) )
		{
			showUI = !showUI;
		}
	}

	// TODO: use ref instead, i was just too lazy to reload the game here lol
	private const int WIDTH = 400;
	private int AddLine( int row, string text )
	{
		GUI.Label( new Rect( 20, row += 20, WIDTH, 100 ), text );

		return row;
	}

	private void OnGUI()
	{
		if ( !showUI )
		{
			return;
		}

		// Ideally I would love to have this drawn last so we can size depending on row,
		// however I don't really know how IMGUI works and it seems to draw based on when we call it
		// and I'm unsure if that's possible to change, so we have to settle for the height here.
		GUI.Box( new Rect( 10, 10, WIDTH, 460), "Discord Debug" );

		// Row gets incremented by 20 each time, 5 is just for a bit of padding
		int row = 5;
		row = AddLine( row, $"== Player ==" );
		row = AddLine( row, $"Player Name: {player?.citizenName}" );
		row = AddLine( row, $"Social Credit: {gameplay?.GetCurrentSocialCreditLevel()}" );
		row = AddLine( row, $"Active Case: {player?.GetActiveCase()?.name}" );

		row += 10;
		row = AddLine( row, $"== Location ==" );
		row = AddLine( row, $"Current Location: {player?.currentGameLocation?.name}" );
		row = AddLine( row, $"Current Building: {player?.currentBuilding?.name}" );
		row = AddLine( row, $"Current Room: {player?.currentRoom?.name}" );
		row = AddLine( row, $"Previous Location: {player?.previousGameLocation?.name}" );
		row = AddLine( row, $"Previous Building: {player?.previousBuilding?.name}" );
		row = AddLine( row, $"Previous Room: {player?.previousRoom?.name}" );

		row += 10;
		row = AddLine( row, $"== Interaction ==" );
		row = AddLine( row, $"Current Interactable: {player?.interactingWith?.name}" );

		row += 10;
		row = AddLine( row, $"== Misc ==" );
		row = AddLine( row, $"MainMenuCavas active: {MenuHooks.MainMenuCanvas.active}" );

		row += 10;
		row = AddLine( row, $"== Discord ==" );
		row = AddLine( row, $"Details: {discord.Details}" );
		row = AddLine( row, $"State: {discord.State}" );
		row = AddLine( row, $"SmallImageKey: {discord.SmallImageKey}" );
		row = AddLine( row, $"SmallImageText: {discord.SmallImageText}" );
	}
}
#endif
