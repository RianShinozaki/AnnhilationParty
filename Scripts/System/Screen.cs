using Godot;
using System;

public partial class Screen : TextureRect
{
	Control SplitLine;
    public override void _Ready()
    {
        base._Ready();
        SplitLine = GetNode<TextureRect>("SplitLine");
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
		((ShaderMaterial)Material).SetShaderParameter("split_origin", new Vector2(GameController.splitX/320, 0.5f));
		SplitLine.GlobalPosition = new Vector2(GameController.splitX - 49, 0);
    }
}
