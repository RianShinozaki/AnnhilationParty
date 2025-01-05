using Godot;
using System;
using System.Data;

public partial class Clickable : Area2D
{
	bool mouseOver = false;
	public bool active = true;
	[Export] string tooltip = "";
	public override void _Ready()
	{
		AreaEntered += _on_area_2d_area_entered;
		AreaExited += _on_area_2d_area_exited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(active) {
			
			if(mouseOver) {
				Tooltip.Instance.Visible = true;
				Tooltip.Instance.Text = tooltip;
				if(Input.IsActionJustPressed("Click")) {
					OnClick();
				}
			}
		}
		CheckActive();
	}

	public virtual void CheckActive() {
		if(GameController.Instance.currentState == GameController.GameState.Dialogue) {
			active = false;
		}
	}
	
	private void _on_area_2d_area_entered(Area2D area) {
		mouseOver = true;
		
	}
	private void _on_area_2d_area_exited(Area2D area) {
		mouseOver = false;
		Tooltip.Instance.Visible = false;
	}
	public virtual void OnClick() {
		
	}
}
