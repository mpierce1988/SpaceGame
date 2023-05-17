using UnityEngine;
using System;

public interface IGameplayInput
{
	event Action<Vector2> OnMovementChange;
	event Action<Vector2> OnLookChange;
	event Action<bool> OnPrimaryChange;
	event Action<bool> OnSecondaryChange;
}