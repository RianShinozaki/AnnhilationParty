using Godot;
using System;

public partial class Teacher : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;

	float trustAtStartOfMeeting;

	public Godot.Collections.Array questionOptions = new Godot.Collections.Array{
		"*Make light conversation with lunch.",
		"By the way, I've got some food to donate...",
		"How long have you been working here?",
		"What do you think about the Department for Children?",
		"*Something odd you saw in the logs...",
		"What do you think we can do about this war?"
	};
	public Godot.Collections.Array questionIndices = new Godot.Collections.Array{
		105,
		110,
		200,
		300,
		400,
		500,
		600,
	};

	public override void _Ready()
    {
        base._Ready();
		GameController.theSpeaker = this;
		if(GameController.currentTime != 0 
			|| GameController.GetDay(GameController.currentDay) == "Thursday" 
			|| GameController.GetDay(GameController.currentDay) == "Saturday") {
				textbox_system.Instance.Initialize(-100);
				NPCSprite.Visible = false;
				return;
			}
		animPlayer.Play("Intro");
    }
    public override void _Process(double delta)
    {
		if(!initialized) {
			initialized = true;
		}
    }

	public void Init() {
		
		
		if(GameController.teacherMemory[1] == 0) {
			textbox_system.Instance.Initialize(0);
			return;
		} else {
			textbox_system.Instance.Initialize(100);
		}
	}

	 public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			case -100:
				if(GameController.teacherMemory[1] == 0) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"...",
							"*The Teacher isn't here.",
							"*You're not sure what to do. Better go back."
						},
						new Godot.Collections.Array{
							"test_1",
							"test_2",
							"test_3"
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							-1
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"...",
							"*The Teacher isn't here.",
							"...",
							"You spend some time performing the duties they showed you."
						},
						new Godot.Collections.Array{
							"test_1",
							"test_2",
							"test_3"
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							-1
						}
					);
				}
				break;
			case 0:
				GameController.teacherMemory[1] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You see an adult surrounded by young children. The oldest of them can't be much more than 5 years old.",
						"*Their faces are dirty, their clothes old, and their knees scraped. Yet, what stands out are their widely smiling faces.",
						"*The object of their smiles: The Teacher, beaming down at them in turn.",
						"*As they see you, the Teacher turns the smile towards you as well.",
						"...",
						"Ah! What have we here?"
					},
					new Godot.Collections.Array{
						"test_1",
						"test_2",
						"test_3"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						1
					}
				);
				break;
			case 1:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The teacher is standing between you and the children."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"What is this place?",
						"Lots of kids here...",
						"I'd like to join the volunteer effort."

					},
					new Godot.Collections.Array{
						2,
						3,
						6
					}
				);
				break;
			
			case 2:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, we're one of the many branches of the Unobhel House Refugee Center.",
						"Built from an old armory, actually. Explains all the big towers and such. Haha...",
						"Um. But thanks to the efforts of the Department for Children and our gracious volunteers, we've been able to support many refugee children here."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						1
					}
				);
				break;
			
			case 3:
				if(GameController.teacherMemory[0] == 0) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"Yes, well... it is a shelter.",
							"Most of them are refugees from the war on Colony Theta-68.",
							"...",
							"They're all good kids. I promise.",
							"I know some people have reservations... um...",
							"But every child deserves a home.",
							"...",
							"Do you have children of your own?"
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							"Yes.",
							"Never did."
						},
						new Godot.Collections.Array{
							4,
							5
						}
					);
				}
				else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"Most of them? Refugees from the war on Colony Theta-68.",
							"...",
							"They're all good kids. I promise.",
							"I know some people have reservations... um...",
							"Every child deserves a home.",
							"...",
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							1
						}
					);
				}
				break;
			
			case 4:
				GameController.teacherMemory[0] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*...There's an odd flicker behind the Teacher's eyes.",
						"We get lots of empty nesters here, haha... some folks are really in the parenting business for life!",
						"I mean -- not to presume...",
						"Well, in either case... are you considering being a volunteer? We could use someone with the experience."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						1
					}
				);
				break;
			
			case 5:
				GameController.teacherMemory[0] = 2;
				GameController.trustLevels[GameController.TEACHER] += 0.5f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The teacher gives you a small smile.",
						"But still interested in caring for others, perhaps?",
						"It's understandable -- it's tough to start a family with the world as it is.",
						"But it's hard to ignore so much need.",
						"Are you considering being a volunteer? We could use someone with that kind of heart."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						1
					}
				);
				break;
			
			case 6:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"How nice! Look kids, this nice man over here wants to help take care of you all!",
						"*...Many pairs of eyes turn towards you.",
						"Doesn't he look kind and responsible? Just what we need around here!",
						"...",
						"We appreciate all the help we can get. Really. Of all the branches of Unobhel House, we don't get a lot of support here...",
						"With so many of the Colony T-68 kids here... well, all of them really...",
						"Lots of people would rather support their own, I suppose...",
						"But every child needs love and care, don't they? Doesn't matter where they're from.",
						"I'm glad we can count on your support too. Come, let me show you around..."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						7
					}
				);
				break;
			
			case 7:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You spend a busy morning learning the ropes of the small refugee center.",
						"*Everywhere you go, curious eyes follow."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						8
					}
				);
				break;
			
			case 8:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, I think that ought to be enough for today. I have to run soon -- I have a night job at the public school.",
						"Please, do come back when you find the time. As volunteers, we don't have strict schedules, but I'll always be here any day but Thursday and Saturday.",
						"I hope I see you again!",
						"...",
						"*You've become acquainted with the Teacher. Time to head back."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;
			case 100:

				Godot.Collections.Array theDialogue = new Godot.Collections.Array{
					"*You find the Teacher surrounded by smiling faces, as usual.",
					"*They turn to you.",
					"Hi! It's good to see you again.",
					"There's a lot I could use your help with today...",
					"*You spend the morning helping the Teacher around the center.",
					"...",
					"*Before you know it, it's noon. You and the Teacher take a moment for some lunch.",
					
				};
				if(GameController.trustLevels[GameController.TEACHER] < 1) {
					theDialogue.Add("*The two of you sit in silence.");
				}
				if(GameController.trustLevels[GameController.TEACHER] >= 1 && GameController.trustLevels[GameController.TEACHER] < 2) {
					theDialogue.Add("*The two of you sit in a companionable silence.");
				}
				if(GameController.trustLevels[GameController.TEACHER] >= 2 && GameController.trustLevels[GameController.TEACHER] < 3) {
					theDialogue.Add("*The Teacher is humming as they eat their lunch.");
				}
				if(GameController.trustLevels[GameController.TEACHER] >= 3 && GameController.trustLevels[GameController.TEACHER] < 4) {
					theDialogue.Add("*The Teacher smiles at your gratefully.");
				}
				if(GameController.trustLevels[GameController.TEACHER] >= 4) {
					theDialogue.Add("*You sense a good deal of trust from the Teacher.");
				}

				theDialogue.Add("*...Now's your chance to get closer to them.");

				Godot.Collections.Array theQuestions = new Godot.Collections.Array{};
				Godot.Collections.Array theIndices = new Godot.Collections.Array{};

				for(int i = 0; i < questionOptions.Count; i++) {
					if(GameController.teacherQuestionFlags[i] == true) {
						theQuestions.Add(questionOptions[i]);
						theIndices.Add(questionIndices[i]);
					}
				}

				animPlayer.Play("Intro");
				dialogueSet = new DialogueSet(
					theDialogue,
					new Godot.Collections.Array{
						
					},
					theQuestions,
					theIndices
				);
				break;
			
			case 105:
				GameController.trustLevels[GameController.SOFTWARE] += 0.5f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The two of you work together in a companionable silence.",
						"*Some time passes. Time for you to head home."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						106
					}
				);
				break;
			case 106:
				Godot.Collections.Array byeDialogue = new Godot.Collections.Array{};
				if(trustAtStartOfMeeting == GameController.trustLevels[GameController.SOFTWARE] ) {
					byeDialogue.Add("*You didn't grow much closer today...");
				} else if (Mathf.FloorToInt(trustAtStartOfMeeting) < Mathf.FloorToInt(GameController.trustLevels[GameController.SOFTWARE]) ) {
					byeDialogue.Add("*The Engineer definitely trusts you more after today.");
				} else if (trustAtStartOfMeeting < GameController.trustLevels[GameController.SOFTWARE] ) {
					byeDialogue.Add("*You think you grew a little closer to the Engineer today.");
				} else {
					byeDialogue.Add("*Something is wrong here.");
				}
				dialogueSet = new DialogueSet(
					byeDialogue,
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;

			default:
				dialogueSet = null;
				break;
		}
		return dialogueSet;
	}
}
