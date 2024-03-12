using Godot;
using System;

public partial class Butcher : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;

	float trustAtStartOfMeeting;

	public Godot.Collections.Array questionOptions = new Godot.Collections.Array{
		"*Spend the night working silently.",
		"Besides 'Auta', what else are you into?",
		"You looking forward to anything?",
		"How'd you end up in militia work?",
		"So, you seeing anyone?",
		"Everything been alright with you?",
		"Listen, about that girl..."
	};
	public Godot.Collections.Array questionIndices = new Godot.Collections.Array{
		105,
		110,
		-1,
		-1,
		-1,
		-1,
		-1
	};

	public override void _Ready()
    {
        base._Ready();
		GameController.theSpeaker = this;
		if(GameController.currentTime != 0 
			|| GameController.GetDay(GameController.currentDay) == "Saturday" 
			|| GameController.GetDay(GameController.currentDay) == "Sunday") {
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
		if(GameController.butcherMemory[1] == 0) {
			textbox_system.Instance.Initialize(0);
			return;
		} else {
			textbox_system.Instance.Initialize(100);
		}

	}
		/*GameController.Instance.ChangeMoney(-20);
		GameController.trustLevels[GameController.BUTCHER] += 1;
		GameController.AddToInventory(steak);
		*/
	public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			case -100:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*The Butcher isn't here.",
						"*Better go back."
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
			case 0:
				GameController.butcherMemory[1] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Butcher's shop.",
						"*The range of meats on display is staggering. You're not sure you could name what animals they came from...",
						"*A large, wisened looking man with an air of self-assurance turns to you with a grin.",
						"...",
						"Well, well! There's the fresh meat I ordered.",
						"Little bit fresher than I was expecting, but we'll make do!",
						"--Hah! The look on your face. It's just my little joke."
					},
					new Godot.Collections.Array{
						"butcher_0_1",
						"butcher_0_2",
						"butcher_0_3"
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
						"So, what can I do for you?",
						"*A wide range of cuts hang from the ceiling or lie beneath the counter's glass.",
						"*The prices are at a premium.",
						"*...But window-shopping won't win you his favor."
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Looking for something nice.",
						"Maybe some deli ham?",
						"How expensive."
					},
					new Godot.Collections.Array{
						2,
						6,
						20
					}
				);
				break;
			case 2:
				if(GameController.butcherMemory[0] != 0) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"And why not? I could cut you a rib-eye -- that's real nice.",
							"Good, yes?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Sounds good. [$120]",
							"Actually, on second thought..."
						},
						new Godot.Collections.Array{
							5,
							1
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"And why not? I could cut you a rib-eye -- that's real nice.",
							"You have a nice little somebody to impress?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Yep.",
							"Nope."
						},
						new Godot.Collections.Array{
							3,
							4
						}
					);
				}
				break;
			case 3:
				GameController.butcherMemory[0] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Hoho! Well, you came to the right man. My artistic cuts of steak are known to capture hearts.",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$120]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						5,
						1,
					}
				);
				break;
			case 4:
				GameController.butcherMemory[0] = 3;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, a man has to treat themselves too.",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$120]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						5,
						1,
					}
				);
				break;
			case 5:
				if(GameController.money >= 120) {
					GameController.Instance.ChangeMoney(-120);
					GameController.trustLevels[GameController.BUTCHER] += 1;
					GameController.AddToInventory(steak);
					if(GameController.butcherMemory[0] == 1) {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher cuts a prime chunk of steak down and wraps it up. It takes seconds.",
								"To a fine evening for you, eh?",
								"*Acquired Rib-Eye Steak."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					} else {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher cuts a prime chunk of steak down and wraps it up. It takes seconds.",
								"Here you are.",
								"*Acquired Rib-Eye Steak."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					}
					
				}
				else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*You can't afford that..."
						},
						new Godot.Collections.Array{
							"nan"
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							1,
						}
					);
				}
				break;
			
			case 6:
				if(GameController.butcherMemory[0] != 0) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"Yes, yes, good choice. Healthy stuff.",
							"Good, yes?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Sounds good. [$80]",
							"Actually, on second thought..."
						},
						new Godot.Collections.Array{
							9,
							1
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"Yes, yes, good choice. Healthy stuff. You have kids?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Yep.",
							"Nope."
						},
						new Godot.Collections.Array{
							7,
							8
						}
					);
				}
				break;
			case 7:
				GameController.butcherMemory[0] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Doesn't matter what they say -- the real stuff's always better.",
						"Hard to find out here, but you'd always want the best for your kids, right?",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$80]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						9,
						1,
					}
				);
				break;
			case 8:
				GameController.butcherMemory[0] = 2;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well then, this'll keep you in good health for a while.",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$80]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						9,
						1,
					}
				);
				break;
			case 9:
				if(GameController.money >= 80) {
					GameController.Instance.ChangeMoney(-80);
					GameController.trustLevels[GameController.BUTCHER] += 1;
					GameController.AddToInventory(steak);
					if(GameController.butcherMemory[0] == 1) {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher slams on the counter a hefty sack of deli ham.",
								"To their good health.",
								"*Acquired Deli Ham."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					} else {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher slams on the counter a hefty sack of deli ham.",
								"Enjoy.",
								"*Acquired Deli Ham."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					}
					
				}
				else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*You can't afford that..."
						},
						new Godot.Collections.Array{
							"nan"
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							1,
						}
					);
				}
				break;
			
			case 10:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"The Butcher studies you carelessly.",
						"You're a new face around here.",
						"Well, you picked the right butcher. These other guys -- half the time it's synth meat. Poor quality synth meat.",
						"Sure, why learn to cut a pork shoulder when you can just grow one?",
						"I hold no grudge against others with personal beliefs or dietary restrictions, of course. Don't mistake my devotion to the real deal for intolerance.",
						"But...",
						"Ah, but I see it in your eyes. They're hungry.",
						"Like mine.",
						"You see it too, yes?",
						"Meat is a beautiful thing."
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sure is.",
						"...For eating.",
						"You sound insane."
					},
					new Godot.Collections.Array{
						11,
						12,
						13
					}
				);
				break;
			
			case 11:
				GameController.trustLevels[GameController.BUTCHER] += 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Knew you had the eyes.",
						"*The Butcher seems genuinely delighted to hear that.",
						"Not many of us left anymore, are there?",
						"Humans these days would rather be fed whatever's given to them than choose a meal of their own accord. Insanity.",
						"As long as you have an appreciation for the finer things, you'll always be free, yes?",
						"Hope you'll become a regular.",
						"Don't give me the cold shoulder -- I already have plenty!"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						14
					}
				);
				break;
			
			case 12:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yes, yes. For eating.",
						"*The Butcher isn't too interested in that response...",
						"Synth meat may capture the flavor, but it'll never capture the heart.",
						"I can sell you a heart too, if you like.",
						"Ha! Just my little joke."
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						14
					}
				);
				break;
			
			case 13:
				GameController.trustLevels[GameController.BUTCHER] += 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Ha!",
						"*The Butcher seems oddly delighted to hear that.",
						"I hope so! What sort of artist is better than the insane one, after all?",
						"Ah, if only you could see the world the way I do... you'd be the richer for it.",
						"Come by often, yes? I'll fix you the good stuff until you're insane for it too."
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						14
					}
				);
				break;
			
			case 14:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*You've become acquainted with the Butcher. Time to head back."
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
			
			case 20:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, you know how it is these days.",
						"Meat is grown in labs en masse. Not many still appreciate the finer things, I'm afraid...",
						"The personal touch keeps me in business, though.",
						"Not getting cold feet, are you?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"I'll go for something nice.",
						"Maybe some deli ham?",
						"I'll have to pass for now."
					},
					new Godot.Collections.Array{
						2,
						6,
						21
					}
				);
				break;
			
			case 21:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...Alright then. Suppose you'd better move along."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						14
					}
				);
				break;

			case 100:
				GameController.Instance.ChangeMoney(-8);

				Godot.Collections.Array theDialogue = new Godot.Collections.Array{
					"*You see the Engineer and say hello. Briefly, you chat about your respective days."
				};
				if(GameController.trustLevels[GameController.BUTCHER] < 1) {
					theDialogue.Add("*A momentary awkward silence ensues.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 1 && GameController.trustLevels[GameController.BUTCHER] < 2) {
					theDialogue.Add("*The Engineer settles comfortably back into his work.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 2 && GameController.trustLevels[GameController.BUTCHER] < 3) {
					theDialogue.Add("*The Engineer looks happy to see you.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 3 && GameController.trustLevels[GameController.BUTCHER] < 4) {
					theDialogue.Add("*The Engineer seems like they're opening up to you.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 4) {
					theDialogue.Add("*You sense a good deal of trust from the Engineer.");
				}

				theDialogue.Add("*Now's a good time to get to know him better.");

				Godot.Collections.Array theQuestions = new Godot.Collections.Array{};
				Godot.Collections.Array theIndices = new Godot.Collections.Array{};

				for(int i = 0; i < questionOptions.Count; i++) {
					if(GameController.butcherQuestionFlags[i] == true) {
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
