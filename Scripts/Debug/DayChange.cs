using Godot;
using System;

public partial class DayChange : Button
{
	[Export] int val;
	private void _on_pressed() {
		GameController.currentDay += val;
		GameController.Instance.OnSwitchScene();
	}
}
