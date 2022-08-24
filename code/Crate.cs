using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox;

partial class Crate : ModelEntity
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/citizen_props/crate01.vmdl" );

		Tags.Add( "test" );

		SetupPhysicsFromModel( PhysicsMotionType.Static );

		EnableDrawing = true;
	}

	[Event.Frame]
	private void ClientFrame()
	{
		DebugOverlay.Text( $"Tags: {string.Join( ", ", Tags.List )}", Position, Color.White );
	}
}
