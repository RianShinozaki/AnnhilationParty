using Godot;
using System;

public partial class StartGameButton : Button
{
	[Export] AnimationPlayer animPlayer;
	private void _on_pressed() {
        animPlayer.Play("GameStart");
	}
	public void StartTheGame() {
		GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://MainGame2D.tscn");
	}
}
