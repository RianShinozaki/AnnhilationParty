using Godot;
using System;

public partial class Phone : Clickable
{
	public static Phone Instance;
	[Export] public AnimatedSprite2D animPlayer;
	[Export] public AudioStreamPlayer ringing;
	public bool isRinging;
	int specialInt = -1;
    public override void _Ready()
    {
        base._Ready();
		Instance = this;

		if(GameController.currentDay - OfficeSpeaker.Instance.lastCalledDay > 1 && GameController.timesCalledOldGuard == 1) {
			StartRinging();
			specialInt = 114;
		} else {
			GD.Print(GameController.currentDay - OfficeSpeaker.Instance.lastCalledDay);
		}
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
		if(specialInt == -1) {
			OfficeSpeaker.Instance.PickupCall();
			
		} else {
			OfficeSpeaker.Instance.SpecialCall(specialInt);
			specialInt = -1;
		}
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
