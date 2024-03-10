using Godot;
using Microsoft.VisualBasic;
using System;

public partial class SuspectLog : Control
{
	[Export] public LogDay[] logs = new LogDay[31];

	[Export] public Label EngineerButton;
	[Export] public Label TeacherButton;
	[Export] public Label ButcherButton;
	[Export] public Label OccultistButton;
	[Export] public Label Date;
	public int currentLogDay;
	public static SuspectLog Instance;

	public Godot.Collections.Array days = new Godot.Collections.Array{
		"Friday",
		"Saturday",
		"Sunday",
		"Monday",
		"Tuesday",
		"Wednesday",
		"Thursday"
		};
    public override void _Ready()
    {
        base._Ready();
		Instance = this;
    }
    public void Init() {
		currentLogDay = 1;
		//currentLogDay = Mathf.Max(1, GameController.currentDay-1);
		OnDayChanged();

		Rotation = Mathf.Pi * 2/3;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation", 0, 0.5f).SetTrans(Tween.TransitionType.Sine);
		tween.Finished += switchToSuspectLog;
		Visible = true;
	}
	public void OnDayChanged() {
		if(currentLogDay > GameController.currentDay-1 && GameController.currentDay > 1) {
			EngineerButton.Text = "";
			TeacherButton.Text = "";
			ButcherButton.Text = "";
			OccultistButton.Text = "";
		} else if (GameController.currentDay > 1){
			EngineerButton.Text = logs[currentLogDay-1].EngineerLog;
			TeacherButton.Text = logs[currentLogDay-1].TeacherLog;
			ButcherButton.Text = logs[currentLogDay-1].ButcherLog;
			OccultistButton.Text = logs[currentLogDay-1].OccultistLog;
		} else {
			EngineerButton.Text = "No reports have come in yet. Check tomorrow.";
		}
		GD.Print(currentLogDay);
		GD.Print(days.Count);
		String thisDayName = (String)days[(currentLogDay-1)%7];
		Date.Text = thisDayName + ", December " + currentLogDay.ToString("D2") + ", 50XX";

		if(currentLogDay == 2 && GameController.currentDay > 2) {
			GameController.engineerMemory[0] = 1;
			GD.Print("Saw the Auta thing.");
		}
	}

	private void _on_next_day_pressed() {
		currentLogDay = Mathf.Clamp(currentLogDay + 1, 1, Mathf.Max(1, GameController.currentDay-1));
		OnDayChanged();
	}
	private void _on_previous_day_pressed() {
		currentLogDay = Mathf.Clamp(currentLogDay - 1, 1, Mathf.Max(1, GameController.currentDay-1));
		OnDayChanged();
	}
	private void _on_close_pressed() {
		GameController.currentState = GameController.GameState.Transitioning;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation",  Mathf.Pi * 2/3, 0.5f).SetTrans(Tween.TransitionType.Sine);
		tween.Finished += switchToOffice;

	}
	private void switchToSuspectLog() {
		GameController.currentState = GameController.GameState.SuspectLog;
	}
	private void switchToOffice() {
		GameController.currentState = GameController.GameState.Office;
		Visible = false;
	}

}
