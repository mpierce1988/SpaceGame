using UnityEngine;

public interface INavMeshAgentMove
{
    float MaxSpeed { get; }
    Vector3 CurrentVelocity { get; }
    float MaxRotationSpeed { get;}
    
    Vector2 Position { get; }
    Vector2 InputVector { get; }
    Quaternion Rotation { get; }
    bool IsStopped { get; }
    bool CanRotate { get; }
    float DistanceToDestinationThreshold { get; }
}
