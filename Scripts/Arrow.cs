using Godot;
using System;

namespace QuickSwipe.Scripts;

public class SwipeEventArgs(bool valid) : EventArgs
{
	public bool Valid { get; } = valid;
}

[GlobalClass, Tool]
public partial class Arrow : Node2D
{
	[Export]
	public Vector2 Direction { get; set; }
	public bool Enabled { get; set; } = true;
	public EventHandler<SwipeEventArgs>? Swipe;

	public override void _Process(double delta)
	{
		if (!Enabled)
			return;
		var mouseVelocity = Input.GetLastMouseVelocity().Normalized();
		var swipeDirection = GetSwipeDirection(mouseVelocity);
		if (swipeDirection != Vector2.Zero)
			OnSwipe(swipeDirection == -Direction);
	}

	protected void OnSwipe(bool valid)
	{
		Swipe?.Invoke(this, new SwipeEventArgs(valid));
	}
	
	private Vector2 GetSwipeDirection()
	{
		if (Input.IsActionJustPressed("ui_right"))
			return Vector2.Right;
		if (Input.IsActionJustPressed("ui_left"))
			return Vector2.Left;
		if (Input.IsActionJustPressed("ui_up"))
			return Vector2.Up;
		if (Input.IsActionJustPressed("ui_down"))
			return Vector2.Down;
		return Vector2.Zero;

	}

	private Vector2 GetSwipeDirection(Vector2 mouseVelocity)
	{
		if (mouseVelocity == Vector2.Zero)
			return GetSwipeDirection();
		var direction = Vector2.Zero;
		direction.X = mouseVelocity.X > 0.5f ? 1 : -1;
		direction.Y = mouseVelocity.Y > 0.5f ? 1 : -1;

		if (MathF.Abs(mouseVelocity.X) > Math.Abs(mouseVelocity.Y))
			direction.Y = 0;
		else
			direction.X = 0;
		return direction;
	}
}