using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Entities.NavEntity
{
	public interface INavMeshAgentAnimatable
	{
		Vector2 CurrentPosition { get; }
		Quaternion CurrentRotation { get; }
		Vector2 PreviousPosition { get; }
		Quaternion PreviousRotation { get; }
		
	}
}
