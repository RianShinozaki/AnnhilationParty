using Godot;
using System;
using System.Linq.Expressions;

public partial class Phone : Clickable
{
	public static Phone Instance;
	[Export] public AnimatedSprite2D animPlayer;
	[Export] public AudioStreamPlayer ringing;
	public bool isRinging;
	public int specialInt = -1;
    public override void _Ready()
    {
        base._Ready();
		Instance = this;

		if(GameController.Instance.currentDay - OfficeSpeaker.Instance.lastCalledDay > 1 && GameController.Instance.timesCalledOldGuard == 1 && !GameController.Instance.oldGuardCheck) {
			StartRinging();
			specialInt = 114;
			GameController.Instance.oldGuardCheck = true;
		}
		if(GameController.Instance.currentDay == 31) {
            BGMPlayer.Instance.Stop();
           	StartRinging();
            specialInt = 900;
        }
    }
    public override void CheckActive()
    {
        if(GameController.Instance.currentState != GameController.GameState.Office) {
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
