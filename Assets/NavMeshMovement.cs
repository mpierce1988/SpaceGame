using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement
{
    private INavMeshAgentMove _move;
    private Vector2 _destination;
    private bool _rotate = false;

	public NavMeshMovement(INavMeshAgentMove move)
    {
		_move = move;        
	}

    public Vector3 CalculateNextPosition(Vector2 input, float deltaTime)
    {
        if(input.magnitude > 0.1f)
        {
            _destination = _move.Position + new Vector2(input.x, input.y) * _move.Speed * deltaTime;
            NavMesh.SamplePosition(_destination, out NavMeshHit hit, _move.Speed * deltaTime, NavMesh.AllAreas);
            return hit.position;            
        }

        return _destination;
    }

    public Quaternion CalculateRotation(Vector2 input, float deltaTime)
    {
        if (_rotate)
        {
			Vector2 direction = _destination - _move.Position;
			if (direction.magnitude > 0.1f)
			{
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
				Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
				return Quaternion.RotateTowards(_move.Rotation, targetRotation, _move.RotationSpeed * deltaTime);
			}
		}

        return _move.Rotation;
	}

    public void EnableRotation()
    {
        _rotate = true;
    }

    public void DisableRotation()
    {
        _rotate = false;
    }
}
