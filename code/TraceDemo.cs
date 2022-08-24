using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
	internal partial class TraceDemo : Entity
	{
		[Net] public Vector3 Size { get; set; } = 64f;
		[Net] public Vector3 Vector { get; set; }
		
		[Event.Frame]
		private void ClientFrame()
		{
			var bbox = new BBox( - Size * 0.5f, Size * 0.5f );

			DebugOverlay.Box( Position + bbox.Mins, Position + bbox.Maxs, Color.Green, depthTest: false );

			var result = Trace.Box( bbox, Position, Position + Vector )
				.Ignore( this )
				.WithoutTags( "crate" )
				.Run();

			if ( result.Hit )
			{
				DebugOverlay.Box( bbox.Mins + result.EndPosition, bbox.Maxs + result.EndPosition, Color.Red, depthTest: false );
				DebugOverlay.Text( $"Shape tags: {string.Join( ", ", result.Tags )}\nEntity tags: {string.Join( ", ", result.Entity.Tags.List )}", result.EndPosition, Color.Red );
			}
			else
			{
				DebugOverlay.Box( bbox.Mins + Position + Vector, bbox.Maxs + Position + Vector, Color.Blue, depthTest: false );
			}
		}
	}
}
