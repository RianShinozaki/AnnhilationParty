using Godot;
using System;

public partial class Phone : Clickable
{
	public static Phone Instance;
	[Export] public AnimatedSprite2D animPlayer;
	[Export] public AudioStreamPlayer ringing;
	public bool isRinging;
    public override void _Ready()
    {
        base._Ready();
		Instance = this;
    }
    public override void CheckActive()
    {
        if(GameController.currentState != GameController.GameState.Office) {
			Visible = false;
			active = false;
		} else {
			Visible = true;
			active = true;
		}
    }
	public override void OnClick() {
		OfficeSpeaker.Instance.PickupCall();
		StopRinging();
	}
	public void StartRinging() {
		animPlayer.Play("Ring");
		ringing.Play();
		isRinging = true;
	}
	public void StopRinging() {
		animPlayer.Play("default");
		ringing.Stop();
		isRinging = false;
	}
}
