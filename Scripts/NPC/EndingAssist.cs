using Godot;
using System;

public partial class EndingAssist : Node
{
	public static int actionState = 0;
	public override void _Ready()
	{
		actionState = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//if(GameController.currentState == GameController.GameState.Dialogue)
			//return;

		if(actionState != 0) {
			switch(actionState) {
				case 1:
					InvestigationScene.Instance.QuickFade(1);
					actionState = 0;
					break;
				case 2:
					InvestigationScene.Instance.QuickFade(0);
					actionState = 0;
					break;
				case 3:
					if(GameController.currentState == GameController.GameState.Dialogue) {
						return;
					}
					GameController.currentLocation = GameController.Location.Office;
					GameController.currentDay = 30;
					GameController.currentTime = 1;
					
					GameController.Instance.OnSwitchSceneTransitionBegin("res://Scenes/office.tscn");
					
					actionState = 0;
					break;
				case 4:
					if(GameController.currentState == GameController.GameState.Dialogue) {
						return;
					}
					GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://Scenes/Credits.tscn");
					break;
			}
		}
		
	}
}
