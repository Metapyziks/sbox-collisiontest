using Sandbox;
using System;
using System.Linq;

namespace Sandbox;

partial class Pawn : AnimatedEntity
{
	/// <summary>
	/// Called when the entity is first created 
	/// </summary>
	public override void Spawn()
	{
		base.Spawn();

		//
		// Use a watermelon model
		//
		SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );

		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
	}

	/// <summary>
	/// Called every tick, clientside and serverside.
	/// </summary>
	public override void Simulate( Client cl )
	{
		base.Simulate( cl );
		
		if ( IsServer && Input.Pressed( InputButton.Jump ) )
		{
			foreach ( var crate in Entity.All.OfType<Crate>() )
			{
				crate.Tags.Toggle( "crate" );

				Log.Info( $"Entity has tag: {crate.Tags.Has( "crate" )}" );

				foreach ( var shape in crate.PhysicsBody.Shapes )
				{
					Log.Info( $"Shape has tag: {shape.HasTag( "crate" )}" );
				}
			}
		}
	}
}
