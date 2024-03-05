using Godot;
using System;

public partial class Screen : TextureRect
{
	Control SplitLine;
	Control Split2Line;
	Control Split3Line;
    public override void _Ready()
    {
        base._Ready();
        SplitLine = GetNode<TextureRect>("SplitLine");
        Split2Line = GetNode<TextureRect>("SplitLine2");
        Split3Line = GetNode<TextureRect>("SplitLine3");
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
		((ShaderMaterial)Material).SetShaderParameter("split_origin", new Vector2(GameController.splitX/320, 0.5f));
		SplitLine.GlobalPosition = new Vector2(GameController.splitX*4 - 49*4, 0);
		Split2Line.GlobalPosition = new Vector2(GameController.split2X*4 - 49*4, 0);
		Split3Line.GlobalPosition = new Vector2(GameController.split3X*4 - 49*4, 0);
    }
}
