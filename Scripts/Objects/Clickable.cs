using Godot;
using System;

public partial class Clickable : Area2D
{
	bool mouseOver = false;
	public override void _Ready()
	{
		AreaEntered += _on_area_2d_area_entered;
		AreaExited += _on_area_2d_area_exited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Click")) {
			if(GameController.currentState != GameController.GameState.Dialogue)
				if(mouseOver) OnClick();
		}
	}
	
	private void _on_area_2d_area_entered(Area2D area) {
		mouseOver = true;
	}
	private void _on_area_2d_area_exited(Area2D area) {
		mouseOver = false;
	}
	public virtual void OnClick() {
		
	}
}
