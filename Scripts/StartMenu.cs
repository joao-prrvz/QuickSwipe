using Godot;
using System;

namespace QuickSwipe.Scripts;

public partial class StartMenu : Node2D
{
    private Button _btnPlay = null!;
    private PackedScene _gameScene = null!;

    public override void _Ready()
    {
        _gameScene = GD.Load<PackedScene>("res://Scenes/game.tscn");
        _btnPlay = GetNode<Button>("Play");
        _btnPlay.Pressed += BtnPlay_OnPressed;
    }

    private void BtnPlay_OnPressed()
    {
        GetTree().ChangeSceneToPacked(_gameScene);
    }
}
