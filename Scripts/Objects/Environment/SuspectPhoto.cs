using Godot;
using System;

public partial class SuspectPhoto : Clickable
{
	[Export] public GameController.Location myLocation;
	[Export] public string goToScene;
	bool clicked;

    public override void _Ready()
    {
        base._Ready();
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void CheckActive()
    {
        if(myLocation == GameController.Location.Office) {
            if(GameController.currentState != GameController.GameState.SuspectLocation || !GameController.canReturnButtonAppear) {
                Visible = false;
                active = false;
            } else {
                Visible = true;
                active = true;
            }
        } else {
            if(GameController.currentState != GameController.GameState.Office) {
                Visible = false;
                active = false;
            } else {
                Visible = true;
                active = true;
            }
        }
        if(GameController.currentTime == 2) {
            Visible = false;
            active = false;
        }
        if(Phone.Instance.isRinging) {
            Visible = false;
            active = false;
        }
    }

    public override void OnClick()
    {
        base.OnClick();
		GameController.currentLocation = myLocation;
		GameController.Instance.OnSwitchSceneTransitionBegin(goToScene);
		clicked = true;
        GameController.canReturnButtonAppear =false;
    }
	public void OnSwitchScene() {
		if(clicked) {

		}
	}
}
