using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGame.Input;
using System;
using UnityEngine.InputSystem;

namespace SpaceGame.Input
{
	public class PlayerGameplayInput : IGameplayInput
	{
		private GameplayInputActions _gameplayInputActions;
		private bool _isRotateClockwiseDown;
		private bool _isRotateCounterclockwiseDown;
		private bool _isSecondaryDown;
		private bool _isPrimaryDown;
		private Vector2 _lookInput;
		private Vector2 _movementInput;

		public Vector2 MovementInput => _movementInput;

		public Vector2 LookInput => _lookInput;

		public bool IsPrimaryDown => _isPrimaryDown;

		public bool IsSecondaryDown => _isSecondaryDown;

		public bool IsRotateCounterclockwiseDown => _isRotateCounterclockwiseDown;

		public bool IsRotateClockwiseDown => _isRotateClockwiseDown;

		// Events
		public event Action<Vector2> OnMovementChange;
		public event Action<Vector2> OnLookChange;
		public event Action<bool> OnPrimaryChange;
		public event Action<bool> OnSecondaryChange;
		public event Action<bool> OnRotateCounterclockwiseChange;
		public event Action<bool> OnRotateClockwiseChange;

		public PlayerGameplayInput()
		{
			_gameplayInputActions = new GameplayInputActions();

			_gameplayInputActions.Enable();

			_gameplayInputActions.Gameplay.Movement.performed += Movement_performed;
			_gameplayInputActions.Gameplay.Movement.canceled += Movement_performed;

			_gameplayInputActions.Gameplay.Look.performed += Look_performed;
			_gameplayInputActions.Gameplay.Look.canceled += Look_performed;

			_gameplayInputActions.Gameplay.Primary.performed += Primary_performed;
			_gameplayInputActions.Gameplay.Primary.canceled += Primary_performed;

			_gameplayInputActions.Gameplay.Secondary.performed += Secondary_performed;
			_gameplayInputActions.Gameplay.Secondary.canceled += Secondary_performed;

			_gameplayInputActions.Gameplay.RotateCounterclockwise.performed += RotateCounterclockwise_performed;
			_gameplayInputActions.Gameplay.RotateCounterclockwise.canceled += RotateCounterclockwise_performed;

			_gameplayInputActions.Gameplay.RotateClockwise.performed += RotateClockwise_performed;
			_gameplayInputActions.Gameplay.RotateClockwise.canceled += RotateClockwise_performed;
		}

		~PlayerGameplayInput()
		{
			if (_gameplayInputActions == null)
				return;

			// unregister from all events
			_gameplayInputActions.Gameplay.Movement.performed -= Movement_performed;
			_gameplayInputActions.Gameplay.Movement.canceled -= Movement_performed;

			_gameplayInputActions.Gameplay.Look.performed -= Look_performed;
			_gameplayInputActions.Gameplay.Look.canceled -= Look_performed;

			_gameplayInputActions.Gameplay.Primary.performed -= Primary_performed;
			_gameplayInputActions.Gameplay.Primary.canceled -= Primary_performed;

			_gameplayInputActions.Gameplay.Secondary.performed -= Secondary_performed;
			_gameplayInputActions.Gameplay.Secondary.canceled -= Secondary_performed;

			_gameplayInputActions.Gameplay.RotateCounterclockwise.performed -= RotateCounterclockwise_performed;
			_gameplayInputActions.Gameplay.RotateCounterclockwise.canceled -= RotateCounterclockwise_performed;

			_gameplayInputActions.Gameplay.RotateClockwise.performed -= RotateClockwise_performed;
			_gameplayInputActions.Gameplay.RotateClockwise.canceled -= RotateClockwise_performed;

			_gameplayInputActions.Disable();
		}

		private void RotateClockwise_performed(InputAction.CallbackContext obj)
		{
			if (obj.phase == InputActionPhase.Performed)
			{
				OnRotateClockwiseChange?.Invoke(true);
				_isRotateClockwiseDown = true;
			}
			else
			{
				OnRotateClockwiseChange?.Invoke(false);
				_isRotateClockwiseDown = false;
			}
		}

		private void RotateCounterclockwise_performed(InputAction.CallbackContext obj)
		{
			if (obj.phase == InputActionPhase.Performed)
			{
				OnRotateCounterclockwiseChange?.Invoke(true);
				_isRotateCounterclockwiseDown = true;
			}
			else
			{
				OnRotateCounterclockwiseChange?.Invoke(false);
				_isRotateCounterclockwiseDown = false;
			}
		}

		private void Secondary_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
			{
				OnSecondaryChange?.Invoke(true);
				_isSecondaryDown = true;
			}
			else
			{
				OnSecondaryChange?.Invoke(false);
				_isSecondaryDown = false;
			}
		}

		private void Primary_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			Debug.Log("Primary Action received by PlayerGameplayInput");
			if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
			{
				OnPrimaryChange?.Invoke(true);
				_isPrimaryDown = true;
			}
			else
			{
				OnPrimaryChange?.Invoke(false);
				_isPrimaryDown = false;
			}
		}

		private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
		{
			_lookInput = obj.ReadValue<Vector2>();
			OnLookChange?.Invoke(LookInput);
		}

		private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
		{
			_movementInput = obj.ReadValue<Vector2>();
			OnMovementChange?.Invoke(MovementInput);
		}
	}
}