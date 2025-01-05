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
        GameController.Instance.SwitchSceneTransitionBegin += OnSwitchScene;
        OnSwitchScene("");
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
		((ShaderMaterial)Material).SetShaderParameter("split_origin", new Vector2(GameController.Instance.splitX/320, 0.5f));
		SplitLine.GlobalPosition = new Vector2(GameController.Instance.splitX*4 - 49*4, 0);
		Split2Line.GlobalPosition = new Vector2(GameController.Instance.split2X*4 - 49*4, 0);
		Split3Line.GlobalPosition = new Vector2(GameController.Instance.split3X*4 - 49*4, 0);
    }
    public void OnSwitchScene(string dummy) {
        if(GameController.Instance.currentLocation == GameController.Location.Office) {
            Tween tween = GetTree().CreateTween();
            int toLen = TimeAndDate.Text.Length - (GameController.Instance.currentTime == 1 ? "MORNING" : "EVENING").Length;
            tween.TweenProperty(TimeAndDate, "visible_characters", toLen, 1).SetTrans(Tween.TransitionType.Linear);
            tween.Finished += ChangeDateDisplay;
        }
    }
    public void ChangeDateDisplay() {
        string day = GameController.Instance.GetDay(GameController.Instance.currentDay);
        string time = GameController.Instance.currentTime == 0 ? "MORNING" : (GameController.Instance.currentTime == 1 ? "EVENING" : "NIGHTFALL");
        string date = "12    " + GameController.Instance.currentDay.ToString("D2");
        string stringFirst = date + "\n" + day + "\n";
        TimeAndDate.VisibleCharacters = stringFirst.Length;
        TimeAndDate.Text = stringFirst + time;

        Tween tween = GetTree().CreateTween();
		tween.TweenProperty(TimeAndDate, "visible_characters", (stringFirst + time).Length, 1).SetTrans(Tween.TransitionType.Linear);
    }
}
