using Godot;
using System;

public partial class StartGameButton : Button
{
	[Export] AnimationPlayer animPlayer;
	private async void _on_pressed() {
        animPlayer.Play("GameStart");

		await ToSignal(animPlayer, "animation_finished");
		StartTheGame();
	}
	public void StartTheGame() {
		GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://MainGame2D.tscn");
	}
}
