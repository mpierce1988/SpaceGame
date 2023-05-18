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

		// set a bool is the agent is stopped


		// calculate the rotation as a float between the previous position and the current position and the max rotation speed
		float deltaZ = Quaternion.Angle(_navMeshAgentMove.Rotation, _previousRotation);		
		//float normalizedDeltaZ = deltaZ / Time.fixedDeltaTime;
		float scaledDeltaZ = deltaZ / (_navMeshAgentMove.MaxRotationSpeed * Time.fixedDeltaTime);
		float rotation = Mathf.Clamp01(scaledDeltaZ);
		Debug.Log("DeltaZ: " + deltaZ + ", ScaledDeltaZ: " + scaledDeltaZ);
		SetFloatOnAnimatorList(_portRotationThrusters, "deltaX", rotation);
		SetFloatOnAnimatorList(_starboardRotationThrusters, "deltaX", -rotation);

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
	}
}
