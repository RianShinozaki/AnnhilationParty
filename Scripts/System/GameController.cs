using Godot;
using System;

public partial class GameController : Node
{
	public static GameController Instance;
    
	public static float splitX;
	static float wishSplitX;
    public override void _Ready()
    {
        base._Ready();
        wishSplitX = 200;
    }
    public static void SetSplitX(float x) {
		wishSplitX = x;
	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		splitX = Mathf.Lerp(splitX, wishSplitX, 0.1f);
		GD.Print(splitX);
    }
}

