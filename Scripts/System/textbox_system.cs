using Godot;
using System;
using System.Linq;

public partial class textbox_system : Control
{
	public DialogueSet dialogueSet;
	public int idx;
	public RichTextLabel dialogue;
	public float visibleT;
	public Button nextButton;
	bool responseMode = false;
	public static textbox_system Instance;

	[Export] Control ResponseGroup;
	[Export] PackedScene ResponseButton;

    public override void _Ready()
    {
		dialogue = GetNode("Container").GetNode<RichTextLabel>("Dialogue");
		nextButton = GetNode<Button>("NextButton");
		Instance = this;
        base._Ready();
		Visible = false;
    }

	public void GetNextDialogue(int id) {
		GameController.currentState = GameController.GameState.Dialogue;
		dialogueSet = GameController.theSpeaker.GetNextDialogue(id);
		if(dialogueSet == null) {
			GameController.currentState = GameController.GameState.Office;
			Visible = false;
			return;
		}
		visibleT = 0;
		idx = 0;
		dialogue.Text = (string)dialogueSet.lines[idx];
		dialogue.VisibleCharacters = 0;
		GD.Print("test?");
	}

	//METHODS

	public void Initialize(int id) {
		GetNextDialogue(id);
		Visible = true;
	}
    public override void _Process(double delta)
    {
        base._Process(delta);

		if(!responseMode) {
			//Typewriter effect
			if(dialogue.VisibleCharacters < dialogue.GetTotalCharacterCount()) {
				visibleT += (float)delta * 20;
				dialogue.VisibleCharacters = Mathf.RoundToInt(visibleT);
				nextButton.Visible = false;

			}
			else {
				//If no more letters to display, make the next button visible
				nextButton.Visible = true;
			}
		}
		else {
			nextButton.Visible = false;
		}
    }

	private void _on_next_button_pressed() {
		if(idx == dialogueSet.lines.Count-1) {
			//If there are any responses, switch to response mode. Otherwise, go to the next dialogue
			if(dialogueSet.responses.Count == 0)
				GetNextDialogue((int)dialogueSet.nextLines[0]);
			else {
				SetResponses();
			}
			return;
		}
		visibleT = 0;
		idx++;
		dialogue.Text = (string)dialogueSet.lines[idx];
		dialogue.VisibleCharacters = 0;
	}
	
	private void SetResponses() {
		dialogue.Visible = false;
		ResponseGroup.Visible = true;
		responseMode = true;

		//Clear existing responses
		foreach(Node n in ResponseGroup.GetChildren()) {
			n.QueueFree();
		}
		//Add each response as a button
		for(int i = 0; i < dialogueSet.responses.Count; i++) {
			ResponseButton rb = (ResponseButton)ResponseButton.Instantiate();
			ResponseGroup.AddChild(rb);
			rb.aNumber = (int)dialogueSet.nextLines[i];
			rb.Pressed += _on_response_pressed;
			rb.Text = (string)dialogueSet.responses[i];
		} 
	}
	private void _on_response_pressed(int nextLine) {
		GD.Print("woohoo");
		GetNextDialogue(nextLine);
		dialogue.Visible = true;
		ResponseGroup.Visible = false;
		responseMode = false;
	}
	
}
