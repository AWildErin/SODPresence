using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SODPresence.Behaviours;

public class DiscordManager : MonoBehaviour
{
	public static DiscordManager Instance { get; private set; }

	public string State { get; private set; }
	public string Details { get; private set; }

	public string SmallImageKey { get; private set; }
	public string SmallImageText { get; private set; }

	private const string CLIENT_ID = "1132111285224493147";
	private Timestamps timestamps;
	private DiscordRpcClient client;

	private void Awake()
	{
		// Remove ourselves if we're not the current instance.
		// This generally shouldn't happen, but in case it does
		// we should guard against it.
		if ( Instance != null && Instance != this )
		{
			Debug.LogError( "DiscordManager::Awake: Tried to create new instance while one already existed!!" );
			Destroy( this );
		}
		else
		{
			Instance = this;
		}

		client = new DiscordRpcClient( CLIENT_ID, -1 );

#if DEBUG
		// Only create logging and debug UI when we're built in debug
		client.Logger = new DiscordRPC.Logging.ConsoleLogger( DiscordRPC.Logging.LogLevel.Trace );

		// Add the 
		gameObject.AddComponent<DiscordDebugUI>();
#endif

		timestamps = Timestamps.Now;
		client.Initialize();
	}

	public void OnDestroy()
	{
		client.Dispose();
	}

	private void Update()
	{
		// Push our status and details to Discord

		client.SetPresence( new RichPresence()
		{
			Details = Details,
			State = State,
			Timestamps = timestamps,
			Assets = new Assets()
			{
				LargeImageKey = "gamelogo",
				SmallImageKey = SmallImageKey,
				SmallImageText = SmallImageText,
			}
		} );
	}

	public void SetState( string state )
	{
		State = state;
	}

	public void SetDetails( string details )
	{
		Details = details;
	}

	public void SetSmallImageKey( string smallImageKey )
	{
		SmallImageKey = smallImageKey;
	}

	public void SetSmallImageText( string smallImageText )
	{
		SmallImageText = smallImageText;
	}

	public void ClearSettableProperties()
	{
		State = "";
		Details = "";
		SmallImageKey = "";
		SmallImageText = "";
	}
}
