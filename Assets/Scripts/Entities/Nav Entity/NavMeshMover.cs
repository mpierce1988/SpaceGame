using UnityEngine;
using UnityEngine.AI;

namespace SpaceGame.Entities.NavEntity
{
	public class NavMeshMover
	{
		private INavMeshAgentMove _move;
		private Vector3 _previousPosition;
		private bool _isStopped = false;

		public bool IsStopped => _isStopped;

		public NavMeshMover(INavMeshAgentMove move)
		{
			_move = move;
		}

		public Vector3 CalculateNextPosition(Vector2 input, float deltaTime)
		{
			Debug.Log("Input: " + input);

			var targetDestination = (Vector3)_move.Position + (new Vector3(input.x, input.y, 0) * _move.MaxSpeed);
			NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, _move.MaxSpeed, NavMesh.AllAreas);

			var distanceBetweenTargetandCurrentPosition = Vector2.Distance(hit.position, _move.Position);
			Debug.Log("Distance from target: " + distanceBetweenTargetandCurrentPosition);
			// Keep moving

			// Smooth destination

			Vector3 nextPosition = Vector3.Lerp(_previousPosition, hit.position, deltaTime / _move.TimeToStopSeconds);


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


