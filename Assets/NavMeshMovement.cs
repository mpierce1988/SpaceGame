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
		var targetDestination = _move.Position + new Vector2(input.x, input.y) * _move.MaxSpeed * deltaTime;
		NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, _move.MaxSpeed * deltaTime, NavMesh.AllAreas);

		
		// Keep moving
		

		_nextPosition = hit.position;
		return hit.position;
		
		
	}

	public bool IsTargetDestinationCloseEnoughToStop(Vector2 targetDestionation)
	{
		_isStopped = Vector2.Distance(targetDestionation, _move.Position) <= _move.DistanceToDestinationThreshold;
		return IsStopped;
	}

	public float CalculateRotation(float deltaTime)
    {
		float rotationDirection = 0f;
		if (_move.GameplayInput.IsRotateClockwiseDown && !_move.GameplayInput.IsRotateCounterclockwiseDown)
		{
			// rotate clockwise
			// calculate the new rotation angle based on the MaxRotationSpeed and taking into consideration the fixedDeltaTime
			rotationDirection = -1f;
		}
		else if (_move.GameplayInput.IsRotateCounterclockwiseDown && !_move.GameplayInput.IsRotateClockwiseDown)
		{
			// rotate counterclockwise
			rotationDirection = 1f;
		}

		float rotationAmount = rotationDirection * _move.MaxRotationSpeed * Time.fixedDeltaTime;

		return rotationAmount;
	}
   
}
