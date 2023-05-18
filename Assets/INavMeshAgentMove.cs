using UnityEngine;

public interface INavMeshAgentMove
{
    float Speed { get; }
    float RotationSpeed { get;}
    Vector2 Position { get; }
    Quaternion Rotation { get; }
    bool CanRotate { get; }
    float DistanceToDestinationThreshold { get; }
}
