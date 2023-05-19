using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Entities.NavEntity
{
	public class NavMeshAgentMoveAnimator : MonoBehaviour, INavMeshAgentAnimatable
	{
		[SerializeField] private BaseNavEntity _navMeshAgentMove;


		[SerializeField] private List<Animator> _portThrusterAnimators;
		[SerializeField] private List<Animator> _starboardThrusterAnimators;
		[SerializeField] private List<Animator> _portRotationThrusters;
		[SerializeField] private List<Animator> _starboardRotationThrusters;
		[SerializeField] private List<Animator> _aftEngines;
		[SerializeField] private List<Animator> _forwardEngines;

		private NavMeshAgentMoveAnimater _animater;

		private Vector2 _currentPosition;
		private Quaternion _currentRotation;
		private Vector2 _previousPosition;
		private Quaternion _previousRotation;
		

		public Vector2 PreviousPosition => _previousPosition;

		public Quaternion PreviousRotation => _previousRotation;

		public Vector2 CurrentPosition => _currentPosition;

		public Quaternion CurrentRotation => _currentRotation;

		private void Awake()
		{

			if (_navMeshAgentMove != null)
			{
				GetComponentInParent<INavMeshAgentMove>();
			}
		}

		private void Start()
		{
			_animater = new NavMeshAgentMoveAnimater(this, _navMeshAgentMove);
			_previousPosition = _navMeshAgentMove.Position;
		}

		private void FixedUpdate()
		{
			SetShipMovement(Time.fixedDeltaTime);

			SetShipRotation(Time.fixedDeltaTime);

			// Set isStopped on all engines and thrusters
			SetIsStoppedOnAllEnginesThrusters(_navMeshAgentMove.IsStopped);


			// save position to previous position
			_previousPosition = _navMeshAgentMove.Position;
			_previousRotation = _navMeshAgentMove.Rotation;
		}

		private void SetShipMovement(float deltaTime)
		{
			// calculate the normalized direction of the deltaPosition and set a float for the deltaX and deltaY in the animator
			Vector2 direction = _animater.CalculateLocalDirection();

			// set deltaX in port and starboard thruster animators, changing the direction depending on if its the port or starboard thruster
			// use normal value for port thrusters
			//Debug.Log("DeltaX: " + direction.x);
			SetFloatOnAnimatorList(_portThrusterAnimators, "deltaX", direction.x);

			SetFloatOnAnimatorList(_starboardThrusterAnimators, "deltaX", -direction.x);

			SetFloatOnAnimatorList(_aftEngines, "deltaY", direction.y);

			SetFloatOnAnimatorList(_forwardEngines, "deltaY", -direction.y);
		}

		private void SetShipRotation(float deltaTime)
		{
			float rotation = _animater.CalculateRotationChange(deltaTime);
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

		private void SetFloatOnAnimatorList(List<Animator> animators, string key, float value)
		{
			foreach (Animator anim in animators)
			{
				anim.SetFloat(key, value);
			}
		}

		private void SetBoolOnAnimatorList(List<Animator> animators, string key, bool value)
		{
			foreach (Animator anim in animators)
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

}

