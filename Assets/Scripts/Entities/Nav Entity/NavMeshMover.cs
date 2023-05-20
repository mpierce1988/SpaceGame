﻿using UnityEngine;
using UnityEngine.AI;

namespace SpaceGame.Entities.NavEntity
{
	public class NavMeshMover
	{
		private INavMeshAgentMove _move;
		private Vector3 _previousPosition;
		private bool _isStopped = false;
		private float _currentSpeed = 0f;

		private float _maxSlowdownRate = 2f;

		public bool IsStopped => _isStopped;

		public NavMeshMover(INavMeshAgentMove move)
		{
			_move = move;
			_currentSpeed = _move.MaxSpeed;
		}

		public Vector3 CalculateNextPosition(Vector2 input, float deltaTime)
		{

			// If there's input, accelerate up to max speed
			if (input.magnitude >= 0.1f)
			{
				_currentSpeed = Mathf.Min(_move.MaxSpeed, _currentSpeed + _move.AccelerationUnitsPerSecond * deltaTime);
			}
			// If there's no input, decelerate down to zero speed
			else
			{
				_currentSpeed = Mathf.Max(0, _currentSpeed - _move.SlowDownUnitsPerSecond * deltaTime);
			}

			var targetDestination = (Vector3)_move.Position + (new Vector3(input.x, input.y, 0) * _currentSpeed);
			NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, _currentSpeed, NavMesh.AllAreas);

			Vector3 nextPosition = Vector3.Lerp(_previousPosition, hit.position, deltaTime);
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


