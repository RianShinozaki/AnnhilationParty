using Godot;
using System;

public partial class Screen : TextureRect
{
	Control SplitLine;
	Control Split2Line;
	Control Split3Line;
	Label TimeAndDate;
    public override void _Ready()
    {
        base._Ready();
        SplitLine = GetNode<TextureRect>("SplitLine");
        Split2Line = GetNode<TextureRect>("SplitLine2");
        Split3Line = GetNode<TextureRect>("SplitLine3");
        TimeAndDate = SplitLine.GetNode<Label>("Label");
        GameController.Instance.SwitchScene += OnSwitchScene;
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
		((ShaderMaterial)Material).SetShaderParameter("split_origin", new Vector2(GameController.splitX/320, 0.5f));
		SplitLine.GlobalPosition = new Vector2(GameController.splitX*4 - 49*4, 0);
		Split2Line.GlobalPosition = new Vector2(GameController.split2X*4 - 49*4, 0);
		Split3Line.GlobalPosition = new Vector2(GameController.split3X*4 - 49*4, 0);
    }
    public void OnSwitchScene() {
        string time = GameController.currentTime == 0 ? "MORNING" : (GameController.currentTime == 1 ? "EVENING" : "NIGHTFALL");
        string date = "12    " + GameController.currentDay.ToString("D2");
        TimeAndDate.Text = date + "\r\n" + time;
    }
}
