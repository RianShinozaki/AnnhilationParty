using Godot;
using System;

public partial class Butcher : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;

	float trustAtStartOfMeeting;

	public Godot.Collections.Array questionOptions = new Godot.Collections.Array{
		"You take care, now.",
		"So how’s life, Chief? ",
		"What got you into butchery?",
		"You ever wanna get out of the city?",
		"*Something odd you saw in the logs...",
	};
	public Godot.Collections.Array questionIndices = new Godot.Collections.Array{
		108,
		110,
		150,
		200,
		300
	};
	public Godot.Collections.Array relationshipGates = new Godot.Collections.Array{
		0,
		2,
		3,
		4,
		5,
	};

	public override void _Ready()
    {
		trustAtStartOfMeeting = GameController.trustLevels[GameController.BUTCHER];

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
			textbox_system.Instance.Initialize(50);
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
				GameController.butcherQuestionFlags[0] = true;
				GameController.butcherQuestionFlags[1] = true;
				GameController.butcherQuestionFlags[2] = true;
				GameController.butcherQuestionFlags[3] = true;
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
							"Sounds good. [$60]",
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
						"Sounds good. [$60]",
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
						"Sounds good. [$60]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						5,
						1,
					}
				);
				break;
			case 5:
				if(GameController.money >= 60) {
					GameController.Instance.ChangeMoney(-60);
					GameController.trustLevels[GameController.BUTCHER] += 0.75f;
					GameController.Instance.steaks++;
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
							"Sounds good. [$40]",
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
						"Sounds good. [$40]",
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
						"Sounds good. [$40]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						9,
						1,
					}
				);
				break;
			case 9:
				if(GameController.money >= 40) {
					GameController.Instance.ChangeMoney(-40);
					GameController.trustLevels[GameController.BUTCHER] += 0.75f;
					GameController.hams++;
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
						"But I'd only ever serve the proper stuff to you, boss.",
						"Yes, good meat is a wonderful thing."
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
					},
					new Godot.Collections.Array{
						11,
						12,
						13
					}
				);
				break;
			
			case 11:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Knew you'd feel the same way, boss.",
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
			
			case 50:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Boss! How are we today?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"*Make a purchase.",
						"Just saying hi, chief."
					},
					new Godot.Collections.Array{
						51,
						56
					}
				);
				break;

			case 51:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"My pleasure, big boss.",
						"What can I get for you?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"Let's do a rib-eye steak. [$60]",
						"How about some ham? [$40]",
						"Actually, I've changed my mind..."
					},
					new Godot.Collections.Array{
						52,
						53,
						55
					}
				);
				break;
			case 52:
				if(GameController.money >= 60) {
					GameController.Instance.steaks++;
					GameController.Instance.ChangeMoney(-60);
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*The Butcher cuts a prime chunk of steak down and wraps it up. It takes seconds.",
							"The finest for you, boss.",
							"*Acquired Rib-Eye Steak."
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							54
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"You can't afford that..."
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							51,
						}
					);
				}
				break;
			case 53:
				if(GameController.money >= 40) {
					GameController.Instance.ChangeMoney(-40);
					GameController.hams++;
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*The Butcher slams on the counter a hefty sack of deli ham.",
							"To your good health.",
							"*Acquired Deli Ham."
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							54
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"You can't afford that..."
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							51,
						}
					);
				}
				break;
			case 54:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*Your purchase put the Butcher in a good mood..."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						100
					}
				);
				break;
			case 55:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Ah. So it goes.",
						"*...You'd better leave."
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
			case 56:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Of course, boss. You're always a friend here."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						100
					}
				);
				break;

			case 100:

				Godot.Collections.Array theDialogue = new Godot.Collections.Array{
				};
				if(GameController.trustLevels[GameController.BUTCHER] < 1) {
					theDialogue.Add("*The Butcher clears his throat.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 1 && GameController.trustLevels[GameController.BUTCHER] < 2) {
					theDialogue.Add("*The Butcher contendedly examines his wares.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 2 && GameController.trustLevels[GameController.BUTCHER] < 4) {
					theDialogue.Add("*The Butcher looks happy to see you.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 4 && GameController.trustLevels[GameController.BUTCHER] < 5) {
					theDialogue.Add("*The Butcher is grinning at you from ear to ear.");
				}
				if(GameController.trustLevels[GameController.BUTCHER] >= 5) {
					theDialogue.Add("*You sense a good deal of trust from the Butcher.");
				}

				Godot.Collections.Array theQuestions = new Godot.Collections.Array{};
				Godot.Collections.Array theIndices = new Godot.Collections.Array{};

				bool foundAGate = false;

				for(int i = 0; i < questionOptions.Count; i++) {
					if(GameController.butcherQuestionFlags[i] == true) {
						if (GameController.trustLevels[GameController.BUTCHER] >= (float)relationshipGates[i]) {
							theQuestions.Add(questionOptions[i]);
							theIndices.Add(questionIndices[i]);
						} else {
							foundAGate = true;
						}
					}
				}
				if(foundAGate) {
					theDialogue.Add("*You sense that if you were closer, you would have more to talk about...");
				}

				theDialogue.Add("*Now's a good time to get to know him better.");

		
				dialogueSet = new DialogueSet(
					theDialogue,
					new Godot.Collections.Array{
						
					},
					theQuestions,
					theIndices
				);
				break;
			
			case 106:
				Godot.Collections.Array byeDialogue = new Godot.Collections.Array{};
				if(GameController.trustLevels[GameController.BUTCHER] >= 5) {
					byeDialogue.Add("*Your trust with the Butcher has maxxed out...");
				} else if(trustAtStartOfMeeting == GameController.trustLevels[GameController.BUTCHER] ) {
					byeDialogue.Add("*You didn't grow much closer today...");
				} else if (Mathf.FloorToInt(trustAtStartOfMeeting) < Mathf.FloorToInt(GameController.trustLevels[GameController.BUTCHER]) ) {
					byeDialogue.Add("*The Butcher definitely trusts you more after today.");
				} else if (trustAtStartOfMeeting < GameController.trustLevels[GameController.BUTCHER] ) {
					byeDialogue.Add("*You think you grew a little closer to the Butcher today.");
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
			case 108:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You head back out...",
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

			case 110:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				GameController.butcherQuestionFlags[1] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Life? Oh, life’s good, big boss. Why, I live in a wonderful city, and I feed hungry people each day.",
						"That’s the secret to happiness, boss. Did you know?",
						"Work that feeds the soul. Yes, life is full of thankless jobs. But nobody ever forgets to say ‘thank you’ to their butcher!",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Good food makes life worth living.",
						"Well, don’t bite the hand that feeds…",

					},
					new Godot.Collections.Array{
						111,
						112
					}
				);
				break;
			case 111:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
					
						"Yes, boss. Precisely.",
						"How many lives do I make ever brighter with my delicious cuts? And truly, is there anything worth more than that?",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						113
					}
				);
				break;
			case 112:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Butcher gives you a pitying smile",
						"Oh, no, boss. I don’t believe in pessimistic outlooks like that.",
						"One should live life FOR things, never just to avoid them!",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						113
					}
				);
				break;
			case 113:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yes, my life is just fine.",
						"*…The Butcher’s smile fades a little, for a moment.",
						"Although, boss, sometimes I find myself losing a bit of joy as I live in this city.",
						"Perhaps it is in the air? The city air spoils the meat.",
						"You should take care to leave the city too, once in a while.",
						"*…The Butcher has finished his thoughts on this topic.",

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
			
			case 150:
				GameController.butcherQuestionFlags[2] = false;
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Butcher smiles with nostalgia.",
						"Oh, no, boss. I’ve been in butchery from the moment I was born!",
						"My family, you see, owned farmland on the far side of the planet.",
						"I grew up around wonderful friends – the pigs, the goats, the cows…",
						"You’d best believe I enjoyed each day to the fullest.",
						"*A slight tint of sadness crosses his face.",
						"I often wish for those days back, you understand.",
						"No more farming exists on this planet – it has all been converted into city, yes?",
						"*...That's all the Butcher has to say on that."
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
			case 200:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				GameController.butcherQuestionFlags[3] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh, no, boss. My life is here, and I’m happy with it. I take trips out when the shop is closed for weekends, though.",
						"You’ve been up to the nature reservations up North, yes?",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Of course – for fishing.",
						"Of course – for hiking.",
						"Nope…",

					},
					new Godot.Collections.Array{
						203,
						201,
						202
					}
				);
				break;
			case 201:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Sounds like a wonderful time.",
						"Frankly, I'm not much for hiking myself. Poor knees.",
						"And poor lungs, I suppose, from all this city air.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						204
					}
				);
				break;
			case 202:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh no, boss, that won't do.",
						"You stay here too long and your lungs grow black."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						204
					}
				);
				break;
			case 203:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yes, Boss! Where have you been all my life?",
						"Fishing is a wonderful time.",
						"It's one of the most wonderful things about the reservation."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						204
					}
				);
				break;
			case 204:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"But the air up North… why, it just cleans your lungs out.",
						"Reminds you of simpler days. Of the trees and the meadows and the animals...",
						"I wouldn't trade my upbringing on the farms for anything else.",
						"It taught me a true appreciation for the finer things...",
						"The things unspoiled in life...",
						"It gave me the eyes, you see.",
						"You have them too, don't you?",
						"Your eyes... they're hungry, like mine.",
						"Yes... fresh meat is a beautiful thing.",
						"Wouldn't you agree?",
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
			
			case 300:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*That’s right… something you saw didn’t seem to add up. What was it?",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"The Butcher ate a nice salad.",
						"The Butcher carved up a human.",
						"The Butcher destroyed all his belongings.",
					},
					new Godot.Collections.Array{
						301,
						301,
						302
					}
				);
				break;
			
			case 301:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"That doesn't seem right...",
						"You'd better check the logbook again..."
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
			case 302:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*...Yeah. How should you approach that with him?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"I spent a lot of money on a new phone the other day…",
						"So, how do you feel about the Buddhist lifestyle?",
						"Boy. Kinda trashy in here today, huh?",

					},
					new Godot.Collections.Array{
						305,
						303,
						304
					}
				);
				break;
			case 303:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"The Buddhist lifestyle?",
						"I... don't believe I have any particular opinion on that, boss.",
						"*The Butcher has no idea what you're talking about...",
						"*Maybe you need a different approach? You should try again later..."

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						302
					}
				);
				break;
			case 304:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Eh? Boss, you break my heart. It's not that bad, is it?",
						"*Your blatant insult did not seem to endear you to him.",
						"*Maybe you need a different approach? You should try again later..."

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						302
					}
				);
				break;
			case 305:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				GameController.butcherQuestionFlags[4] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Is that right, boss? Well, you enjoy what you enjoy.",
						"*…The Butcher is thinking.",
						"Always a new phone coming out, isn’t there? Or a new hover-car, perhaps. New Synthesized Reality discs.",
						"Do you ever wonder if the best things in life are the ones that were right from the start?",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Nope. I’m a Material Boy.",
						"Like… meat?",
					},
					new Godot.Collections.Array{
						306,
						307
					}
				);
				break;
			case 306:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Butcher chuckles.",
						"Ah, well, what makes you happy is what’s most important in the end. Who am I to judge?",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						308
					}
				);
				break;
			case 307:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Butcher laughs.",
						"Yes, boss, you understand. Like meat!",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						308
					}
				);
				break;
			
			case 308:
				GameController.trustLevels[GameController.BUTCHER] += 0.75f;
				GameController.brokenPhones++;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Personally, I think there’s senselessness in the way we search for the next best thing to make ourselves happy.",
						"Yet, we had it already. And we destroyed it.",
						"*…The Butcher pauses. Then, he laughs.",
						"Oh, look at me. I’m getting carried away with my thoughts.",
						"The world is how it is! I can accept that as well as anyone.",
						"But just imagine...",
						"If everything returned to the way it used to be.",
						"Why, imagine how fresh the meat could be...",
						"*The Butcher is lost in thought...",
						"...You decide to head out.",
						"*…? As you leave, you find something in the trash outside.",
						"*It’s a broken cellphone. You decide to pick it up.",
						"*Could somebody fix this…?",
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


			default:
				dialogueSet = null;
				break;
		}
		return dialogueSet;
	}
}
