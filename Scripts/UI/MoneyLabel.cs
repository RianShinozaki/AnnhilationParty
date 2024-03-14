using Godot;
using System;

public partial class MoneyLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameController.Instance.MoneyChanged += OnMoneyChanged;
		Text = GameController.money.ToString("C");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnMoneyChanged() {
		Text = GameController.money.ToString("C");
		if(GameController.money > 900) {
			((Control)GetParent()).Visible = false;
		}
	}
}
