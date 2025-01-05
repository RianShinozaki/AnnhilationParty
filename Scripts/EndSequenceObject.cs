using Godot;
using System;

public partial class EndSequenceObject : Node
{
	[Export] public GameController.Location myLocation;
	[Export] public string goToScene;
	public static bool active = false;
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(active && GameController.Instance.currentState != GameController.GameState.Dialogue) {
			GameController.Instance.currentLocation = myLocation;
			GameController.Instance.OnSwitchSceneTransitionBegin(goToScene);
			GameController.Instance.SetSplitX(-100);
			active = false;
		}
	}
}
