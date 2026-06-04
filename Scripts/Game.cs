using Godot;
using System;

namespace QuickSwipe.Scripts;

public partial class Game : Node2D
{
	private PackedScene[] _arrowScenes = [];
	private Timer _timer = null!;
	private Label _lblScore = null!;
	private Label _lblTime = null!;
	private readonly Random _rnd = new();
	private Arrow? _currentArrow;
	private int _score;
	private int _timeLeft = 60;
	
	public override void _Ready()
	{
		_arrowScenes = 
		[
			GD.Load<PackedScene>("res://Scenes/up_arrow.tscn"),
			GD.Load<PackedScene>("res://Scenes/left_arrow.tscn"),
			GD.Load<PackedScene>("res://Scenes/down_arrow.tscn"),
			GD.Load<PackedScene>("res://Scenes/right_arrow.tscn"),
		];
		_lblTime = GetNode<Label>("Time");
		_lblScore = GetNode<Label>("Score");
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += Timer_OnTimeout;
		_timer.Start();
		CreateRandomArrow();
	}

	private void Timer_OnTimeout()
	{
		_timeLeft--;
		_lblTime.Text = $"Time left: {_timeLeft}s";
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
		if (e.Valid)
		{
			_score++;
			_lblScore.Text = $"Score: {_score}";
		}
		CreateRandomArrow();
	}
}
