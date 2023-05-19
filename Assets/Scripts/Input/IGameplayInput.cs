using UnityEngine;
using System;

namespace SpaceGame.Input
{
	public interface IGameplayInput
	{
		event Action<Vector2> OnMovementChange;
		event Action<Vector2> OnLookChange;
		event Action<bool> OnPrimaryChange;
		event Action<bool> OnSecondaryChange;
		event Action<bool> OnRotateCounterclockwiseChange;
		event Action<bool> OnRotateClockwiseChange;

		Vector2 MovementInput { get; }
		Vector2 LookInput
		{
			get;
		}
		bool IsPrimaryDown { get; }
		bool IsSecondaryDown { get; }
		bool IsRotateCounterclockwiseDown { get; }
		bool IsRotateClockwiseDown { get; }
	}
}