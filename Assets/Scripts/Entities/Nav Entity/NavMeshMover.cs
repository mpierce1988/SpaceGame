using UnityEngine;
using UnityEngine.AI;

namespace SpaceGame.Entities.NavEntity
{
	public class NavMeshMover
	{
		private INavMeshAgentMove _move;
		private Vector3 _previousPosition;
		private bool _isStopped = false;

		private float _maxSlowdownRate = 2f;

		public bool IsStopped => _isStopped;

		public NavMeshMover(INavMeshAgentMove move)
		{
			_move = move;
		}

		public Vector3 CalculateNextPosition(Vector2 input, float deltaTime)
		{
			

			var targetDestination = (Vector3)_move.Position + (new Vector3(input.x, input.y, 0) * _move.MaxSpeed);
			NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, _move.MaxSpeed, NavMesh.AllAreas);


			// Keep moving
			Vector3 nextPosition = Vector3.zero;
			/*if(_move.InputVector.magnitude < 0.1f)
			{
				// calculate drifting
				// Smooth destination
				// 1. Calculate the distance between the current position and the target position
				var distanceBetweenTargetandCurrentPosition = Vector2.Distance(hit.position, _move.Position);
				// 2. Calculate the time it will take to reach the target position
				var timeToReachTargetPosition = distanceBetweenTargetandCurrentPosition / _maxSlowdownRate;
				Debug.Log("Distance Between Target and Current Position: " + distanceBetweenTargetandCurrentPosition);
				Debug.Log("Time to Reach Target Position: " + timeToReachTargetPosition);
				nextPosition = Vector3.Lerp(_previousPosition, hit.position, deltaTime / timeToReachTargetPosition);
			}
			else
			{
				// calculate next position according to max speed
				nextPosition = Vector3.Lerp(_previousPosition, hit.position, deltaTime / _move.TimeToStopSeconds);
			}*/

			var distanceBetweenTargetandCurrentPosition = Vector2.Distance(hit.position, _move.Position);
			// calculate next position according to max speed
			nextPosition = Vector3.Lerp(_previousPosition, hit.position, (1 / deltaTime) * (_move.TimeToStopSeconds * distanceBetweenTargetandCurrentPosition));


			_previousPosition = nextPosition;
			return nextPosition;

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
}


