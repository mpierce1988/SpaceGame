using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface INavMeshAgentAnimatable
{
	NavEntity NavEntity { get; }
	List<Animator> PortThrusterAnimators { get; }
	List<Animator> StarboardThrusterAnimators { get; }
	List<Animator> PortRotationThrusters { get; }
	List<Animator> StarboardRotationThrusters { get; }
	List<Animator> AftEngineAnimators { get; }
	List<Animator> ForwardEngineAnimators { get; }
	Vector2 PreviousPosition { get; }
	Quaternion PreviousRotation { get; }
}

public class NavMeshAgentMoveAnimator : MonoBehaviour, INavMeshAgentAnimatable
{
    [SerializeField] private NavEntity _navMeshAgentMove;


	[SerializeField] private List<Animator> _portThrusterAnimators;
	[SerializeField] private List<Animator> _starboardThrusterAnimators;
	[SerializeField] private List<Animator> _portRotationThrusters;
	[SerializeField] private List<Animator> _starboardRotationThrusters;
	[SerializeField] private List<Animator> _aftEngines;
	[SerializeField] private List<Animator> _forwardEngines;

	private Vector2 _previousPosition;
	private Quaternion _previousRotation;

	public NavEntity NavEntity => _navMeshAgentMove;

	public List<Animator> PortThrusterAnimators => _portThrusterAnimators;

	public List<Animator> StarboardThrusterAnimators => _starboardThrusterAnimators;

	public List<Animator> PortRotationThrusters => _portRotationThrusters;

	public List<Animator> StarboardRotationThrusters => _starboardRotationThrusters;

	public List<Animator> AftEngineAnimators => _aftEngines;

	public List<Animator> ForwardEngineAnimators => _forwardEngines;

	public Vector2 PreviousPosition => _previousPosition;

	public Quaternion PreviousRotation => throw new System.NotImplementedException();

	private void Awake()
	{		

        if(_navMeshAgentMove != null)
        {
            GetComponentInParent<INavMeshAgentMove>();
        }
	}

	private void Start()
	{
		_previousPosition = _navMeshAgentMove.Position;
	}

	private void FixedUpdate()
	{
		SetShipMovement();

		SetShipRotation();

		// Set isStopped on all engines and thrusters
		SetIsStoppedOnAllEnginesThrusters(_navMeshAgentMove.IsStopped);


		// save position to previous position
		_previousPosition = _navMeshAgentMove.Position;
		_previousRotation = _navMeshAgentMove.Rotation;
	}

	private void SetShipMovement()
	{
		// calculate the normalized direction of the deltaPosition and set a float for the deltaX and deltaY in the animator
		Vector2 direction = CalculateLocalDirection();

		// set deltaX in port and starboard thruster animators, changing the direction depending on if its the port or starboard thruster
		// use normal value for port thrusters
		//Debug.Log("DeltaX: " + direction.x);
		SetFloatOnAnimatorList(_portThrusterAnimators, "deltaX", direction.x);

		SetFloatOnAnimatorList(_starboardThrusterAnimators, "deltaX", -direction.x);

		SetFloatOnAnimatorList(_aftEngines, "deltaY", direction.y);

		SetFloatOnAnimatorList(_forwardEngines, "deltaY", -direction.y);
	}

	private void SetShipRotation()
	{
		float rotation = CalculateRotationChange();
		if (Mathf.Abs(rotation) > 0.1f)
		{
			SetFloatOnAnimatorList(_portRotationThrusters, "deltaX", -rotation);
			SetFloatOnAnimatorList(_starboardRotationThrusters, "deltaX", rotation);
		}
		else if (rotation == 0.0f)
		{
			SetFloatOnAnimatorList(_portRotationThrusters, "deltaX", 0);
			SetFloatOnAnimatorList(_starboardRotationThrusters, "deltaX", 0);
		}
	}

	private float CalculateRotationChange()
	{
		Vector3 previousRotationEuler = _previousRotation.eulerAngles;
		Vector3 currentRotationEuler = _navMeshAgentMove.Rotation.eulerAngles;

		float deltaZ = Mathf.DeltaAngle(previousRotationEuler.z, currentRotationEuler.z) / Time.fixedDeltaTime;
		//float normalizedDeltaZ = deltaZ / Time.fixedDeltaTime;

		// normalize the deltaZ to a scale of -1 to 1, based on the maximum rotation speed
		// division by 180 is necessary to convert from degrees to a scale of -1 to 1
		float rotation = Mathf.Clamp(deltaZ / (_navMeshAgentMove.MaxRotationSpeed), -1.0f, 1.0f);

		return rotation;
	}

	private Vector2 CalculateLocalDirection()
	{
		// calculate the normalized direction of the deltaPosition and set a float for the deltaX and deltaY in the animator
		//Vector2 direction = (_navMeshAgentMove.Position - _previousPosition).normalized;
		Vector2 direction = _navMeshAgentMove.InputVector;

		// calculate the deltaX and the deltaY relatitive to the rotation of the agent
		direction = Quaternion.Inverse(_navMeshAgentMove.Rotation) * direction;

		return direction;
	}

	private void SetFloatOnAnimatorList(List<Animator> animators, string key, float value)
	{
		foreach(Animator anim in animators)
		{
			anim.SetFloat(key, value);
		}
	}

	private void SetBoolOnAnimatorList(List<Animator> animators, string key, bool value)
	{
		foreach(Animator anim in animators)
		{
			anim.SetBool(key, value);
		}
	}

	private void SetIsStoppedOnAllEnginesThrusters(bool isStopped)
	{
		// set for all thrusters
		SetBoolOnAnimatorList(_portThrusterAnimators, "isStopped", isStopped);
		SetBoolOnAnimatorList(_starboardRotationThrusters, "isStopped", isStopped);
		SetBoolOnAnimatorList(_portRotationThrusters, "isStopped", isStopped);
		SetBoolOnAnimatorList(_starboardRotationThrusters, "isStopped", isStopped);
		SetBoolOnAnimatorList(_aftEngines, "isStopped", isStopped);
		SetBoolOnAnimatorList(_forwardEngines, "isStopped", isStopped);
	}
}
