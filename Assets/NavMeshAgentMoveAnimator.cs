using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NavMeshAgentMoveAnimator : MonoBehaviour
{
    [SerializeField] private NavEntity _navMeshAgentMove;
    private Animator _animator;

	private Vector2 _previousPosition;

	private void Awake()
	{
		_animator = GetComponent<Animator>();

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
		Debug.Log("Current Velocity: " + currentVelocity);	
		// calculate float of the current speed compared to the max speed
		float speed = Mathf.Clamp01(currentVelocity.magnitude / _navMeshAgentMove.MaxSpeed);

		_animator.SetFloat("speed", speed);		

		// calculate the normalized direction of the deltaPosition and set a float for the deltaX and deltaY in the animator
		Vector2 direction = (_navMeshAgentMove.Position - _previousPosition).normalized;

		// calculate the deltaX and the deltaY relatitive to the rotation of the agent
		direction = Quaternion.Inverse(_navMeshAgentMove.Rotation) * direction;


		_animator.SetFloat("deltaX", direction.x);
		_animator.SetFloat("deltaY", direction.y);

		// set a bool is the agent is stopped
		_animator.SetBool("isStopped", _navMeshAgentMove.IsStopped);

		// calculate the rotation as a float between the previous position and the current position and the max rotation speed
		float rotation = Mathf.Clamp01(Vector2.Angle(_previousPosition, _navMeshAgentMove.Position) / _navMeshAgentMove.MaxRotationSpeed);
		_animator.SetFloat("rotation", rotation);

		// save position to previous position
		_previousPosition = _navMeshAgentMove.Position;
	}
}
