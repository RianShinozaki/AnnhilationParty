using Godot;
using System;

public partial class ContinueGameButton : Button
{
	[Export] AnimationPlayer animPlayer;

    public override void _Ready()
    {
        base._Ready();
		if(!FileAccess.FileExists("user://savegame.save")) Visible = false;
    }
    private async void _on_pressed() {
        animPlayer.Play("GameStart");

		await ToSignal(animPlayer, "animation_finished");
		StartTheGame();
	}
	public void StartTheGame() {
		GameController.Instance.Load();
		GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://MainGame2D.tscn");
	}
}
