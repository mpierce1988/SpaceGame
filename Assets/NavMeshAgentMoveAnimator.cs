using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NavMeshAgentMoveAnimator : MonoBehaviour
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
		// calculate the current Velocity based on the current and previousr position
		/// text
		/// text
		/// lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua
		/// sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua
		
		float fixedDeltaTime = Time.fixedDeltaTime;
		Vector2 currentVelocity = _navMeshAgentMove.CurrentVelocity;
		//Debug.Log("Current Velocity: " + currentVelocity);	
		// calculate float of the current speed compared to the max speed
		float speed = Mathf.Clamp01(currentVelocity.magnitude / _navMeshAgentMove.MaxSpeed);

			

		// calculate the normalized direction of the deltaPosition and set a float for the deltaX and deltaY in the animator
		Vector2 direction = (_navMeshAgentMove.Position - _previousPosition).normalized;

		// calculate the deltaX and the deltaY relatitive to the rotation of the agent
		direction = Quaternion.Inverse(_navMeshAgentMove.Rotation) * direction;


		// set deltaX in port and starboard thruster animators, changing the direction depending on if its the port or starboard thruster
		// use normal value for port thrusters
		//Debug.Log("DeltaX: " + direction.x);
		SetFloatOnAnimatorList(_portThrusterAnimators, "deltaX", direction.x);

		SetFloatOnAnimatorList(_starboardThrusterAnimators, "deltaX", -direction.x);

		SetFloatOnAnimatorList(_aftEngines, "deltaY", direction.y);

		SetFloatOnAnimatorList(_forwardEngines, "deltaY", -direction.y);


		Vector3 previousRotationEuler = _previousRotation.eulerAngles;
		Vector3 currentRotationEuler = _navMeshAgentMove.Rotation.eulerAngles;		


		float deltaZ = Mathf.DeltaAngle(previousRotationEuler.z, currentRotationEuler.z) / Time.fixedDeltaTime;
		//float normalizedDeltaZ = deltaZ / Time.fixedDeltaTime;

		// normalize the deltaZ to a scale of -1 to 1, based on the maximum rotation speed
		// division by 180 is necessary to convert from degrees to a scale of -1 to 1
		float rotation = Mathf.Clamp(deltaZ / (_navMeshAgentMove.MaxRotationSpeed), -1.0f, 1.0f);
		if(Mathf.Abs(rotation) > 0.1f)
		{
			Debug.Log("DeltaZ: " + deltaZ + ", ScaledDeltaZ: " + rotation);
			SetFloatOnAnimatorList(_portRotationThrusters, "deltaX", -rotation);
			SetFloatOnAnimatorList(_starboardRotationThrusters, "deltaX", rotation);
		} 
		else if(rotation == 0.0f)
		{
			SetFloatOnAnimatorList(_portRotationThrusters, "deltaX", 0);
			SetFloatOnAnimatorList(_starboardRotationThrusters, "deltaX", 0);
		}

		// Set isStopped on all engines and thrusters
		SetIsStoppedOnAllEnginesThrusters(_navMeshAgentMove.IsStopped);


		// save position to previous position
		_previousPosition = _navMeshAgentMove.Position;
		_previousRotation = _navMeshAgentMove.Rotation;
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
