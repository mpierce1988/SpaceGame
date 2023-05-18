using UnityEngine;

public interface INavMeshAgentMove1
{
	Vector3 Position { get; }
	Quaternion Rotation { get; }
	float RotationSpeed { get; }
	float Speed { get; }
}