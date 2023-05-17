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
		_gameplayInputActions.Gameplay.Look.performed += Look_performed;
		_gameplayInputActions.Gameplay.Primary.performed += Primary_performed;
		_gameplayInputActions.Gameplay.Secondary.performed += Secondary_performed;
	}

	private void Secondary_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		if (obj.canceled)
		{
			OnSecondaryChange?.Invoke(false);
		} 
		else
		{
			OnSecondaryChange?.Invoke(true);
		}
	}

	private void Primary_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		if (obj.canceled)
		{
			OnPrimaryChange?.Invoke(false);
		} 
		else
		{
			OnPrimaryChange?.Invoke(true);
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
