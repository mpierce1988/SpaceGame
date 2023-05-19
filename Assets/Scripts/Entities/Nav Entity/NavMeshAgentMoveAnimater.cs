using UnityEngine;

namespace SpaceGame.Entities.NavEntity
{
	public class NavMeshAgentMoveAnimater
	{
		private INavMeshAgentAnimatable _animatable;
		private INavMeshAgentMove _navAgentMove;

		public NavMeshAgentMoveAnimater(INavMeshAgentAnimatable animatable, INavMeshAgentMove navAgentMove)
		{
			_animatable = animatable;
			_navAgentMove = navAgentMove;
		}

		public float CalculateRotationChange(float deltaTime)
		{
			Vector3 previousRotationEuler = _animatable.PreviousRotation.eulerAngles;
			Vector3 currentRotationEuler = _navAgentMove.Rotation.eulerAngles;

			float deltaZ = Mathf.DeltaAngle(previousRotationEuler.z, currentRotationEuler.z) / deltaTime;
			//float normalizedDeltaZ = deltaZ / Time.fixedDeltaTime;

			// normalize the deltaZ to a scale of -1 to 1, based on the maximum rotation speed
			// division by 180 is necessary to convert from degrees to a scale of -1 to 1
			float rotation = Mathf.Clamp(deltaZ / (_navAgentMove.MaxRotationSpeed), -1.0f, 1.0f);

			return rotation;
		}

		public Vector2 CalculateLocalDirection()
		{
			// calculate the normalized direction of the deltaPosition and set a float for the deltaX and deltaY in the animator
			//Vector2 direction = (_navMeshAgentMove.Position - _previousPosition).normalized;
			Vector2 direction = _navAgentMove.InputVector;

			// calculate the deltaX and the deltaY relatitive to the rotation of the agent
			direction = Quaternion.Inverse(_navAgentMove.Rotation) * direction;

			return direction;
		}
	}

}

