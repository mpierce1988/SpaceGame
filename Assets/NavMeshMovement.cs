using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement
{
    private INavMeshAgentMove _move;
    private Vector2 _nextPosition;  
    private bool _isStopped = false;

    public bool IsStopped => _isStopped;

	public NavMeshMovement(INavMeshAgentMove move)
    {
		_move = move;        
	}

    public Vector3 CalculateNextPosition(Vector2 input, float deltaTime)
	{
		var targetDestination = _move.Position + new Vector2(input.x, input.y) * _move.Speed * deltaTime;
		NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, _move.Speed * deltaTime, NavMesh.AllAreas);

		
		// Keep moving
		

		_nextPosition = hit.position;
		return hit.position;
		
		
	}

	public bool IsTargetDestinationCloseEnoughToStop(Vector2 targetDestionation)
	{
		_isStopped = Vector2.Distance(targetDestionation, _move.Position) <= _move.DistanceToDestinationThreshold;
		return IsStopped;
	}

	public Quaternion CalculateRotation(float deltaTime)
    {
        if (_move.CanRotate)
        {
			Vector2 direction = _nextPosition - _move.Position;
			if (direction.magnitude > 0.1f)
			{
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
				Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
				return Quaternion.RotateTowards(_move.Rotation, targetRotation, _move.RotationSpeed * deltaTime);
			}
		}

        return _move.Rotation;
	}
   
}
