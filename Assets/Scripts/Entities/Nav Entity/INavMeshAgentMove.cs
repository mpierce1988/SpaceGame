using UnityEngine;
using SpaceGame.Input;

namespace SpaceGame.Entities.NavEntity
{
	public interface INavMeshAgentMove
	{
		float MaxSpeed { get; }
		Vector3 CurrentVelocity { get; }
		float MaxRotationSpeed { get; }
		float SlowDownUnitsPerSecond { get; }
		float AccelerationUnitsPerSecond { get; }
		Vector2 Position { get; }
		Vector2 InputVector { get; }
		Quaternion Rotation { get; }
		bool IsStopped { get; }
		bool CanRotate { get; }
		float DistanceToDestinationThreshold { get; }
		IGameplayInput GameplayInput { get; }
	}
}

