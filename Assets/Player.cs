using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour, INavMeshAgentMove
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _rotationSpeed = 0f;
    [SerializeField] private float _distanceToDestinationThreshold = 1.0f;

    private NavMeshAgent _navMeshAgent;
    private IGameplayInput _gameplayInput;
    private Rigidbody2D _rigidBody2D;
    private NavMeshMovement _movement;
    private Vector2 _inputVector;
    private bool _canRotate = false;

	public float Speed => _speed;

	public float RotationSpeed => _rotationSpeed;	

	Vector2 INavMeshAgentMove.Position => this.transform.position;

	public Quaternion Rotation => this.transform.rotation;

	public bool CanRotate { get => _canRotate; }

	public float DistanceToDestinationThreshold => _distanceToDestinationThreshold;

	// Start is called before the first frame update
	void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _gameplayInput = new PlayerGameplayInput();
        _rigidBody2D = GetComponent<Rigidbody2D>();

        _movement = new NavMeshMovement(this);

		// Register to events
		_gameplayInput.OnMovementChange += _gameplayInput_OnMovementChange;
		_gameplayInput.OnPrimaryChange += _gameplayInput_OnPrimaryChange;
    }

    void FixedUpdate()
    {
        _navMeshAgent.speed = _speed;
       
        Vector2 nextPosition = _movement.CalculateNextPosition(_inputVector, Time.fixedDeltaTime);

        _navMeshAgent.isStopped = _movement.IsTargetDestinationCloseEnoughToStop(nextPosition);
		_navMeshAgent.speed = Speed;
		_navMeshAgent.angularSpeed = RotationSpeed;
		_navMeshAgent.SetDestination(nextPosition);
		_rigidBody2D.SetRotation(_movement.CalculateRotation(Time.fixedDeltaTime));
	}

	void OnDestroy()
	{
		_gameplayInput.OnMovementChange -= _gameplayInput_OnMovementChange;
	}

	private void _gameplayInput_OnPrimaryChange(bool isActive)
	{
        if (isActive)
        {
            _canRotate = true;
        }
        else
        {
            _canRotate = false;
        }
	}	

	private void _gameplayInput_OnMovementChange(Vector2 inputVector)
	{
        _inputVector = inputVector;        
	}
}
