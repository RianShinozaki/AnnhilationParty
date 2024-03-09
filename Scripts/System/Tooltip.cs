using Godot;
using System;
using System.Numerics;

public partial class Tooltip : Label
{
	bool leftMode = false;
	public static Tooltip Instance;

    public override void _Ready()
    {
        base._Ready();
		Instance = this;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Godot.Vector2 mousePos = GetViewport().GetMousePosition();
		leftMode = mousePos.X > 1280/2;
		Position = mousePos - (new Godot.Vector2(1, 0) * 500 * (leftMode ? 1 : 0));
		HorizontalAlignment = (leftMode ? HorizontalAlignment.Right : HorizontalAlignment.Left); 
	}
}
