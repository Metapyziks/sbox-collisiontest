using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Sandbox;

/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players.
/// </summary>
public partial class MyGame : Sandbox.Game
{
	private bool _hasSpawnedDemo;

	public MyGame()
	{
	}

	/// <summary>
	/// A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		// Create a pawn for this client to play with
		var pawn = new Pawn();
		client.Pawn = pawn;

		// Get all of the spawnpoints
		var spawnpoints = Entity.All.OfType<SpawnPoint>();

		// chose a random one
		var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

		// if it exists, place the pawn there
		if ( randomSpawnPoint != null )
		{
			var tx = randomSpawnPoint.Transform;
			tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
			pawn.Transform = tx;
		}

		SpawnDemo( pawn );
	}

	private void SpawnDemo( Pawn targetPawn )
	{
		if ( _hasSpawnedDemo ) return;

		_hasSpawnedDemo = true;

		var center = Vector3.Zero;

		new EnvironmentLightEntity
		{
			Rotation = global::Rotation.From( 75f, 30f, 0f ),
			Color = Color.FromRgb( 0xffffcc ),
		};

		new EnvironmentLightEntity
		{
			Rotation = global::Rotation.From( -15f, 180f + 60f, 0f ),
			Color = Color.FromRgb( 0xccccff ),
		};

		var cube = new Crate
		{
			Position = center
		};

		var trace = new TraceDemo { Position = center - Vector3.Forward * 128f, Vector = Vector3.Forward * 256f };

		targetPawn.Position = new Vector3( -92f, -170f, 108f );
		targetPawn.EyeRotation = Rotation.From( 35f, 70f, 0f );
	}
}
