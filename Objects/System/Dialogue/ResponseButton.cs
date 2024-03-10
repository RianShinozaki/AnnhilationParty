using Godot;
using System;

public partial class ResponseButton : Button
{
	[Signal]
	public delegate void PressedEventHandler(int number);
	public int aNumber;
	
	private void _on_pressed() {
		EmitSignal(SignalName.Pressed, aNumber);
	}
}
