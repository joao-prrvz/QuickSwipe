using Godot;
using System;

namespace QuickSwipe.Scripts;

public partial class Game : Node2D
{
	private PackedScene[] _arrowScenes = [];
	private Timer _timer = null!;
	private readonly Random _rnd = new();
	private Arrow? _currentArrow;
	public override void _Ready()
	{
		_arrowScenes = 
		[
			GD.Load<PackedScene>("res://Scenes/up_arrow.tscn"),
			GD.Load<PackedScene>("res://Scenes/left_arrow.tscn"),
			GD.Load<PackedScene>("res://Scenes/down_arrow.tscn"),
			GD.Load<PackedScene>("res://Scenes/right_arrow.tscn"),
		];
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += Timer_OnTimeout;
		_timer.Start();
	}

	private void Timer_OnTimeout()
	{
		CreateRandomArrow();
	}

	private void CreateRandomArrow()
	{
		if (_currentArrow != null)
		{
			_currentArrow.Swipe -= Arrow_OnSwipe;
			RemoveChild(_currentArrow);
		}
		var arrowScene = _arrowScenes[_rnd.Next(4)];
		var arrow = arrowScene.Instantiate<Arrow>();
		arrow.Swipe += Arrow_OnSwipe;
		_currentArrow = arrow;
		AddChild(arrow);
	}

	private void Arrow_OnSwipe(object? sender, SwipeEventArgs e)
	{
		CreateRandomArrow();
	}
}
