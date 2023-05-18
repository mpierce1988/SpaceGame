using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGame.Input;
using System;

public class PlayerGameplayInput : IGameplayInput
{
	private GameplayInputActions _gameplayInputActions;

	// Events
	public event Action<Vector2> OnMovementChange;
	public event Action<Vector2> OnLookChange;
	public event Action<bool> OnPrimaryChange;
	public event Action<bool> OnSecondaryChange;

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
	}

	~PlayerGameplayInput()
	{
		// unregister from all events
		_gameplayInputActions.Gameplay.Movement.performed -= Movement_performed;
		_gameplayInputActions.Gameplay.Movement.canceled -= Movement_performed;

		_gameplayInputActions.Gameplay.Look.performed -= Look_performed;
		_gameplayInputActions.Gameplay.Look.canceled -= Look_performed;

		_gameplayInputActions.Gameplay.Primary.performed -= Primary_performed;
		_gameplayInputActions.Gameplay.Primary.canceled -= Primary_performed;

		_gameplayInputActions.Gameplay.Secondary.performed -= Secondary_performed;
		_gameplayInputActions.Gameplay.Secondary.canceled -= Secondary_performed;

		_gameplayInputActions.Disable();
	}

	private void Secondary_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
		{
			OnSecondaryChange?.Invoke(true);
		} 
		else
		{
			OnSecondaryChange?.Invoke(false);
		}
	}

	private void Primary_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		Debug.Log("Primary Action received by PlayerGameplayInput");
		if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
		{
			OnPrimaryChange?.Invoke(true);
		} 
		else
		{
			OnPrimaryChange?.Invoke(false);
		}
	}

	private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnLookChange?.Invoke(obj.ReadValue<Vector2>());
	}

	private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnMovementChange?.Invoke(obj.ReadValue<Vector2>());
	}
}
