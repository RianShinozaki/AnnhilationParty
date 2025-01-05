using Godot;
using System;

public partial class DayChange : Button
{
	[Export] int val;
	private void _on_pressed() {
		GameController.Instance.currentDay += val;
		if(GameController.Instance.currentDay > 30) {
			GameController.Instance.currentDay = 30;
		}
		GameController.Instance.OnSwitchScene();
	}
}
