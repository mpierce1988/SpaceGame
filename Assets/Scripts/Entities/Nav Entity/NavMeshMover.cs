using UnityEngine;
using UnityEngine.AI;

namespace SpaceGame.Entities.NavEntity
{
	public class NavMeshMover
	{
		private INavMeshAgentMove _move;
		private Vector3 _previousPosition;
		private bool _isStopped = false;
		private float _currentSpeed = 0f;
		private float _lastTopSpeed = 0f;

		private float _maxSlowdownRate = 2f;

		public bool IsStopped => _isStopped;

		public NavMeshMover(INavMeshAgentMove move)
		{
			_move = move;
			_currentSpeed = _move.MaxSpeed;
		}

		public Vector3 CalculateNextPosition(Vector2 input, float deltaTime)
		{
			float accelerationUnitsPerSecond = CalculateAccelerationUnits(input, deltaTime);
			_currentSpeed = AdjustSpeedBasedOnInput(input, _currentSpeed, accelerationUnitsPerSecond, deltaTime);
			var nextPosition = CalculateNextPositionBasedOnSpeed(input, _currentSpeed, deltaTime);

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

		public float CalculateAccelerationUnits(Vector2 input, float deltaTime)
		{
			// Define different accelerations for longitudinal (front/back) and lateral (side to side) movement
			float longAcc = _move.LongAccelerationUnitsPerSecond;  // Greater acceleration for front/back movement
			float latAcc = _move.LateralAccelerationUnitsPerSecond;

			// Normalize input direction
			Vector2 inputDir = input.normalized;

			// Forward direction of the ship
			Vector2 forwardDir = (Vector2)(_move.Rotation * Vector3.up); // Assuming up is forward for the ship
			
			// Calculate dot product between input direction and forward direction
			float dotProduct = Vector2.Dot(inputDir, forwardDir);

			// Convert dot product into a range from 0 to 1
			float blend = Mathf.Abs(dotProduct);

			// Interpolate between latAcc and longAcc based on blend factor
			float accelerationUnitsPerSecond = Mathf.Lerp(latAcc, longAcc, blend);
			return accelerationUnitsPerSecond;
		}

		private float AdjustSpeedBasedOnInput(Vector2 input, float currentSpeed, float accelerationUnitsPerSecond, float deltaTime)
		{
			float adjustedSpeed;

			// If there's input, accelerate up to max speed
			if (input.magnitude >= 0.1f)
			{
				adjustedSpeed = Mathf.Min(_move.MaxSpeed, currentSpeed + accelerationUnitsPerSecond * deltaTime);
				_lastTopSpeed = adjustedSpeed;
			}
			// If there's no input, decelerate down to zero speed
			else
			{
				float decelerationFactor = Mathf.Pow(1 - (currentSpeed / _lastTopSpeed), 3);
				float deceleration = Mathf.Max(0.1f, decelerationFactor) * _move.SlowDownUnitsPerSecond;				
				adjustedSpeed = Mathf.Max(0, currentSpeed - deceleration * deltaTime);
			}
			_currentSpeed = adjustedSpeed;
			Debug.Log("Adjusted Speed: " + adjustedSpeed);
			return adjustedSpeed;
		}

		private Vector3 CalculateNextPositionBasedOnSpeed(Vector2 input, float currentSpeed, float deltaTime)
		{
			Vector3 targetDisplacement = new Vector3(input.x, input.y, 0) * _move.MaxSpeed;
			var targetDestination = (Vector3)_move.Position + targetDisplacement;
			NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, currentSpeed, NavMesh.AllAreas);

			Vector3 nextPosition = Vector3.Lerp(_previousPosition, hit.position, deltaTime);
			//Vector3 nextPosition = hit.position;

			return nextPosition;
		}

	}
}


