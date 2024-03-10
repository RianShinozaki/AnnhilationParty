using Godot;
using System;

public partial class BGMPlayer : AudioStreamPlayer
{
	public static BGMPlayer Instance;
    public override void _Ready()
    {
        base._Ready();
		Instance = this;
    }
    public void BeginPlaying() {
		VolumeDb = -20;
		Play();
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "volume_db", 0, 5).SetTrans(Tween.TransitionType.Sine);

	}
}
