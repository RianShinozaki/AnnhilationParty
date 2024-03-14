using Godot;
using System;

public partial class StartGameButton : Button
{
	private void _OnButtonPressed() {
        
	}
	public void StartTheGame() {
		GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://MainGame2D.tscn");
	}
}
