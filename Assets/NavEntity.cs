using UnityEngine;

public abstract class NavEntity : MonoBehaviour, INavMeshAgentMove
{
	public abstract float MaxSpeed { get; }
	public abstract Vector3 CurrentVelocity { get; }
	public abstract float MaxRotationSpeed { get; }
	public abstract Vector2 Position { get; }
	public abstract Quaternion Rotation { get; }
	public abstract bool IsStopped { get; }
	public abstract bool CanRotate { get; }
	public abstract float DistanceToDestinationThreshold { get; }
	public abstract Vector2 InputVector { get; }
	public abstract IGameplayInput GameplayInput { get; }
	public abstract float Acceleration { get; }
	public abstract float TimeToStopSeconds { get; }
}
