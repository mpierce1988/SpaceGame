using Cinemachine;
using SpaceGame.Entities.NavEntity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceGame
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class AnimateCinemachineVCOrthoSizeByPlayerSpeed : MonoBehaviour
    {
        [SerializeField] private BaseNavEntity _navAgentMove;
		[SerializeField] private Rigidbody2D _playerRigidBody;
		[SerializeField] private float _minOrthoSize;
        [SerializeField] private float _maxOrthoSize;        
		[SerializeField] private float _smoothTime = 0.3f;
		[SerializeField] private AnimationCurve _orthoSizeCurve;

		private CinemachineVirtualCamera _playerVirtualCamera;
		
		private float _velocity;

		private void Awake()
		{
			_playerVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            
		}

		private void FixedUpdate()
		{
			_playerVirtualCamera.m_Lens.OrthographicSize = CalculateOrthoSize(Time.fixedDeltaTime);
		}

		// A method that takes the delta time and calculates the new ortho size in between the minimum and maximum size. it also
		// slowly lerps to this ortho size value according to the changeSizeSpeed
		private float CalculateOrthoSize(float deltaTime)
		{
			
            float percOfTopSpeed = (_navAgentMove.CurrentVelocity.magnitude / deltaTime) / _navAgentMove.MaxSpeed;
            float newOrthoSize = _minOrthoSize + (_orthoSizeCurve.Evaluate(percOfTopSpeed) * (_maxOrthoSize - _minOrthoSize));

			float orthoSize  = Mathf.SmoothDamp(_playerVirtualCamera.m_Lens.OrthographicSize, newOrthoSize, ref _velocity, _smoothTime);
			            
            return orthoSize;
        }
		
    }
}
