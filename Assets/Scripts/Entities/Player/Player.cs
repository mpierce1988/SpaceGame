using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;
using UnityEngine.Rendering;
using SpaceGame.Input;
using SpaceGame.Entities.NavEntity;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace SpaceGame.Entities.Player
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class Player : BaseNavEntity
	{
		[SerializeField] private float _speed = 3.5f;
		[SerializeField] private float _rotationSpeed = 0f;
		[SerializeField] private float _distanceToDestinationThreshold = 1.0f;
		[SerializeField] private float _longAccelerationUnitsPerSecond = 16f;
		[SerializeField] private float _lateralAccelerationUnitsPerSecond = 4f;
		[SerializeField] private float _timeToStopSeconds = 1.0f;

		private NavMeshAgent _navMeshAgent;
		private IGameplayInput _gameplayInput;
		private Rigidbody2D _rigidBody2D;
		private NavMeshMover _movement;
		private Vector2 _inputVector;
		private bool _canRotate = false;
		private bool _isStopped = false;
		private Vector2 _position = Vector2.zero;
		private Quaternion _rotation = Quaternion.identity;
		private Vector3 _velocity;


		public override float MaxSpeed => _speed;

		public override Vector3 CurrentVelocity => _velocity;

		public override float MaxRotationSpeed => _rotationSpeed;

		public override Vector2 Position => _position;

		public override Quaternion Rotation => _rotation;

		public override bool IsStopped => _isStopped;

		public override bool CanRotate => _canRotate;

		public override float DistanceToDestinationThreshold => _distanceToDestinationThreshold;

		public override Vector2 InputVector => _inputVector;

		public override IGameplayInput GameplayInput => _gameplayInput;

		//public override float Acceleration => _accelerationUnitsPerSecond;

		public override float SlowDownUnitsPerSecond => _timeToStopSeconds;

		public override float LongAccelerationUnitsPerSecond => _longAccelerationUnitsPerSecond;

		public override float LateralAccelerationUnitsPerSecond => _lateralAccelerationUnitsPerSecond;

		// Start is called before the first frame update
		void Start()
		{
			_navMeshAgent = GetComponent<NavMeshAgent>();
			_navMeshAgent.updateRotation = false;
			_navMeshAgent.updateUpAxis = false;

			_gameplayInput = new PlayerGameplayInput();
			_rigidBody2D = GetComponent<Rigidbody2D>();

			_movement = new NavMeshMover(this);

			// Register to events
			_gameplayInput.OnMovementChange += _gameplayInput_OnMovementChange;
			_gameplayInput.OnPrimaryChange += _gameplayInput_OnPrimaryChange;
		}



		void FixedUpdate()
		{
			Vector2 nextPosition = _movement.CalculateNextPosition(_inputVector, Time.fixedDeltaTime);
			//_isStopped = _movement.IsTargetDestinationCloseEnoughToStop(nextPosition);
			//_navMeshAgent.isStopped = IsStopped;
			_navMeshAgent.speed = MaxSpeed;
			_navMeshAgent.angularSpeed = MaxRotationSpeed;
			_navMeshAgent.acceleration = _movement.CalculateAccelerationUnits(_inputVector, Time.fixedDeltaTime);
			_navMeshAgent.SetDestination(nextPosition);

			float rotationAmount = _movement.CalculateRotation(Time.fixedDeltaTime);

			// apply the rotation to the rigidbody
			if (rotationAmount != 0.0)
				_rigidBody2D.MoveRotation(_rigidBody2D.rotation + rotationAmount);

			// update position and rotation and velocity
			_position = transform.position;
			_rotation = transform.rotation;
			_velocity = _navMeshAgent.velocity;
		}

		void OnDrawGizmos()
		{
			if (_navMeshAgent != null && _navMeshAgent.hasPath)
			{
				Gizmos.color = Color.red;
				var previousCorner = _navMeshAgent.path.corners[0];

				for (int i = 1; i < _navMeshAgent.path.corners.Length; i++)
				{
					var currentCorner = _navMeshAgent.path.corners[i];
					Gizmos.DrawLine(previousCorner, currentCorner);
					previousCorner = currentCorner;
				}
			}
		}

		void OnDestroy()
		{
			if (_gameplayInput != null)
				_gameplayInput.OnMovementChange -= _gameplayInput_OnMovementChange;
		}

		private void _gameplayInput_OnPrimaryChange(bool isActive)
		{
			if (isActive)
			{
				Debug.Log("Activate Primary Received");
				_canRotate = true;
			}
			else
			{
				Debug.Log("Deactivate Primary Received");

				_canRotate = false;
			}
		}

		private void _gameplayInput_OnMovementChange(Vector2 inputVector)
		{
			_inputVector = inputVector;
		}
	}
}


