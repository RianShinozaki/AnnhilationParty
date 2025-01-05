using Godot;
using Microsoft.VisualBasic;
using System;

public partial class SuspectLog : Control
{
	[Export] public LogDay[] logs = new LogDay[31];
	[Export] public LogDay[] defaultLogs = new LogDay[7];


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
		GameController.Instance.SwitchScene += _onSwitchScene;
    }
    public void Init() {
		//currentLogDay = 1;
		currentLogDay = Mathf.Max(1, GameController.Instance.currentDay-1);
		OnDayChanged();

		Rotation = Mathf.Pi * 2/3;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation", 0, 0.5f).SetTrans(Tween.TransitionType.Sine);
		tween.Finished += switchToSuspectLog;
		Visible = true;
	}
	public void OnDayChanged() {
		if(currentLogDay > GameController.Instance.currentDay-1 && GameController.Instance.currentDay > 1) {
			EngineerButton.Text = "";
			TeacherButton.Text = "";
			ButcherButton.Text = "";
			OccultistButton.Text = "";
		} else if (GameController.Instance.currentDay > 1){
			if(logs[currentLogDay-1].EngineerLog == "") 
				EngineerButton.Text = defaultLogs[ (currentLogDay-1)%7 ].EngineerLog;
			else
				EngineerButton.Text = logs[currentLogDay-1].EngineerLog;
			
			if(logs[currentLogDay-1].TeacherLog == "") 
				TeacherButton.Text = defaultLogs[ (currentLogDay-1)%7 ].TeacherLog;
			else
				TeacherButton.Text = logs[currentLogDay-1].TeacherLog;
			
			if(logs[currentLogDay-1].ButcherLog == "") 
				ButcherButton.Text = defaultLogs[ (currentLogDay-1)%7 ].ButcherLog;
			else
				ButcherButton.Text = logs[currentLogDay-1].ButcherLog;
			
			if(logs[currentLogDay-1].OccultistLog == "") 
				OccultistButton.Text = defaultLogs[ (currentLogDay-1)%7 ].OccultistLog;
			else
				OccultistButton.Text = logs[currentLogDay-1].OccultistLog;
		} else {
			EngineerButton.Text = "No reports have come in yet. Check tomorrow.";
			TeacherButton.Text = "";
			ButcherButton.Text = "";
			OccultistButton.Text = "";
		}
		GD.Print(currentLogDay);
		GD.Print(days.Count);
		String thisDayName = (String)days[(currentLogDay-1)%7];
		Date.Text = thisDayName + ", December " + currentLogDay.ToString("D2") + ", 50XX";

		if(currentLogDay == 2 && GameController.Instance.currentDay > 2) {
			GameController.Instance.engineerMemory[0] = 1;
		}
		if(currentLogDay == 15 || currentLogDay == 16 || currentLogDay == 17) {
			if(GameController.Instance.engineerMemory[3] == 0) {
				GameController.Instance.engineerMemory[3] = 1;
				GameController.Instance.engineerQuestionFlags[4] = true;
			}

		}
		if(currentLogDay == 22) {
			if(GameController.Instance.teacherMemory[5] == 0) {
				GameController.Instance.teacherMemory[5] = 1;
				GameController.Instance.teacherQuestionFlags[4] = true;
			}

		}
		if(currentLogDay == 12) {
			if(GameController.Instance.butcherMemory[5] == 0) {
				GameController.Instance.butcherMemory[5] = 1;
				GameController.Instance.butcherQuestionFlags[4] = true;
			}

		}
		if(currentLogDay == 19 || currentLogDay == 26) {
			if(GameController.Instance.occultistMemory[5] == 0) {
				GameController.Instance.occultistMemory[5] = 1;
				GameController.Instance.OccultistQuestionFlags[5] = true;
			}

		}
	}

	private void _on_next_day_pressed() {
		currentLogDay = Mathf.Clamp(currentLogDay + 1, 1, Mathf.Max(1, GameController.Instance.currentDay-1));
		OnDayChanged();
	}
	private void _on_previous_day_pressed() {
		currentLogDay = Mathf.Clamp(currentLogDay - 1, 1, Mathf.Max(1, GameController.Instance.currentDay-1));
		OnDayChanged();
	}
	private void _on_close_pressed() {
		GameController.Instance.currentState = GameController.GameState.Transitioning;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation",  Mathf.Pi * 2/3, 0.5f).SetTrans(Tween.TransitionType.Sine);
		tween.Finished += switchToOffice;

	}
	private void _onSwitchScene() {
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "rotation",  Mathf.Pi * 2/3, 0.5f).SetTrans(Tween.TransitionType.Sine);
	}
	private void switchToSuspectLog() {
		GameController.Instance.currentState = GameController.GameState.SuspectLog;
	}
	private void switchToOffice() {
		GameController.Instance.currentState = GameController.GameState.Office;
		Visible = false;
	}

}
