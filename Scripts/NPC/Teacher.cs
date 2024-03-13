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
		"Do you think this war will ever end?"
	};
	public Godot.Collections.Array questionIndices = new Godot.Collections.Array{
		105,
		110,
		200,
		300,
		400,
		500,
	};
	public Godot.Collections.Array relationshipGates = new Godot.Collections.Array{
		0,
		0,
		2,
		3,
		4,
		5,
	};

	public override void _Ready()
    {
		trustAtStartOfMeeting = GameController.trustLevels[GameController.TEACHER];
        base._Ready();
		GameController.theSpeaker = this;

		if(GameController.hams > 0) GameController.teacherQuestionFlags[1] = true;

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
				GameController.trustLevels[GameController.TEACHER] += 0.75f;
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
				GameController.teacherQuestionFlags[0] = true;
				GameController.teacherQuestionFlags[1] = true;
				GameController.teacherQuestionFlags[2] = true;
				GameController.teacherQuestionFlags[3] = true;

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
				if(GameController.trustLevels[GameController.TEACHER] >= 2 && GameController.trustLevels[GameController.TEACHER] < 4) {
					theDialogue.Add("*The Teacher is humming as they eat their lunch.");
				}
				if(GameController.trustLevels[GameController.TEACHER] >= 4 && GameController.trustLevels[GameController.TEACHER] < 5) {
					theDialogue.Add("*The Teacher smiles at your gratefully.");
				}
				if(GameController.trustLevels[GameController.TEACHER] >= 5) {
					theDialogue.Add("*You sense a good deal of trust from the Teacher.");
				}

				theDialogue.Add("*...Now's your chance to get closer to them.");

				Godot.Collections.Array theQuestions = new Godot.Collections.Array{};
				Godot.Collections.Array theIndices = new Godot.Collections.Array{};

				for(int i = 0; i < questionOptions.Count; i++) {
					if(GameController.teacherQuestionFlags[i] == true &&
					GameController.trustLevels[GameController.TEACHER] >= (float)relationshipGates[i]) {
						theQuestions.Add(questionOptions[i]);
						theIndices.Add(questionIndices[i]);
					}
				}

				dialogueSet = new DialogueSet(
					theDialogue,
					new Godot.Collections.Array{
						
					},
					theQuestions,
					theIndices
				);
				break;
			
			case 105:
				GameController.trustLevels[GameController.TEACHER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You make light conversation with lunch.",
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
				if(GameController.trustLevels[GameController.TEACHER] >= 5) {
					byeDialogue.Add("*Your trust with the Teacher has maxxed out...");
				} else if(trustAtStartOfMeeting == GameController.trustLevels[GameController.TEACHER] ) {
					byeDialogue.Add("*You didn't grow much closer today...");
				} else if (Mathf.FloorToInt(trustAtStartOfMeeting) < Mathf.FloorToInt(GameController.trustLevels[GameController.TEACHER]) ) {
					byeDialogue.Add("*The Teacher definitely trusts you more after today.");
				} else if (trustAtStartOfMeeting < GameController.trustLevels[GameController.TEACHER] ) {
					byeDialogue.Add("*You think you grew a little closer to the Teacher today.");
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
			
			case 110:
				GameController.teacherQuestionFlags[1] = false;
				GameController.trustLevels[GameController.TEACHER] += 1f;
				GameController.hams--;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh my gosh!",
						"This is so gracious of you -- thank you.",
						"The kids will be so happy!",
						"*The Teacher beams at you."
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
				GameController.trustLevels[GameController.TEACHER] += 0.75f;
				GameController.teacherQuestionFlags[2] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well … must be around 5 years now.",
						"I’ve always worked with kids, though. I was a teacher still, long before this.",
						"It’s, well, my life’s calling. Do you know what I mean?",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Sure. I’ve got a calling, too.",
						"If life is calling, I think my ringer must be off…",

					},
					new Godot.Collections.Array{
						201,
						202
					}
				);
				break;
			case 201:
				GameController.trustLevels[GameController.TEACHER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Right! So, once you know your calling, the rest couldn’t be easier.",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						203
					}
				);
				break;
			case 202:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh no!",
						"*The Teacher is look at you with a kind of concern they reserve for the kids…",
						"Don’t worry about it… It can take time to figure it out!",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						203
					}
				);
				break;
			case 203:
				GameController.trustLevels[GameController.TEACHER] += 0.25f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"It was easy for me to find mine… I had seven younger siblings, you see.",
						"As the eldest child, I took care of all of them…",
						"I even dropped out of school to care for the youngest of my siblings. I would’ve had a difficult time finding work either way.",
						"But I never questioned if it was worth it. When I didn’t have to take care of my siblings anymore, I just moved on to caring for other kids.",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Your parents?",
						"It must have been difficult, doing all that.",

					},
					new Godot.Collections.Array{
						204,
						205
					}
				);
				break;
			case 204:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Ah, my parents.",
						"*The Teacher snorts, derisively.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						206
					}
				);
				break;
			case 205:
				GameController.trustLevels[GameController.TEACHER] += 0.25f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Often it was, yes…",
						"*The Teacher appreciates your empathy.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						206
					}
				);
				break;
			case 206:
			
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"My parents weren’t around much.",
						"Could you imagine that? Having eight children and just… leaving them to their own devices?",
						"When they were home, they were more focused on their fights with each other than meeting the needs of my siblings.",
						"One day they didn’t come home at all.",
						"*The Teacher sighs sadly.",
						"Children deserve better.",
						"I’m glad someone like you understands that, too.",

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
				GameController.trustLevels[GameController.TEACHER] += 0.75f;
				GameController.teacherQuestionFlags[3] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh… you noticed I didn’t seem thrilled about them?",
						"*The Teacher glares down at their meal. ",
						"Obviously, most of our support comes from the DoC. We couldn’t run these shelters without them.",
						"So…",
						"It’s too bad that particular establishment is rotten to the core.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Should still be grateful.",
						"The hell did they do?",
					},
					new Godot.Collections.Array{
						301,
						302
					}
				);
				break;
			case 301:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Teacher snorts.",
						"Yeah, well, if you knew, I doubt you’d say that.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						303
					}
				);
				break;
			case 302:
				GameController.trustLevels[GameController.TEACHER] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Teacher gives you a sad little smile.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						303
					}
				);
				break;
			case 303:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"The ghouls over at the Department skim the top off the budget whenever they can. What’s more, they used the current… lack of sympathy towards the Colony kids as ammo to pass new legislation letting them bump their numbers up.",
						"You wanna know how they get more numbers on their report? It’s not by building more shelters.",
						"It’s by letting the kids here stay four days a week, tops, and rotating them in and out.",
						"DoC couldn’t care less what’s happening to the kids between their stays here.",
						"Did you know that? I bet you didn’t.",
						"…",
						"Is there anything worse than a society that doesn’t take care of its children?",
						"*…The Teacher doesn’t seem to want to say more on this topic.",
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
			
			case 400:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*…That’s right. Something seemed off about the Teacher. What did you notice?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"The Teacher had a manic episode and vandalized the school.",
						"The Teacher visited a drug dealer.",
						"The Teacher didn’t come to work for two days.",

					},
					new Godot.Collections.Array{
						402,
						401,
						401
					}
				);
				break;
			case 401:
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
			case 402:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*...Yeah. How should you approach that with them?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Did you see that graffiti all over the school?",
						"You seem like you’re in a good mood.",
						"There’s a little paint in your hair.",

					},
					new Godot.Collections.Array{
						403,
						405,
						404
					}
				);
				break;
			case 403:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Graffiti…? Yeah, of course I saw it. I work there.",
						"It’s horrible, of course.",
						"*The Teacher has totally closed off from you…",
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
			case 404:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"…Ah. Is there?",
						"Well, painting activities with the kids can get a little wild. I have to remind them paint belongs on the canvas!",
						"*…The Teacher has become lost in thought.",

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
			case 405:
				GameController.trustLevels[GameController.TEACHER] += 0.75f;
				GameController.teacherQuestionFlags[4] = false;
				GameController.teacherQuestionFlags[5] = true;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, who wouldn’t be? It’s a lovely day, I’m at my favorite place on the planet…",
						"Hah. Tell you the truth, I’ve just been feeling a lot lighter, lately!",
						"I couldn’t tell you why.",
						"I’ve just been worried about so many things lately… the war, the DoC, what all these children will eat…",
						"But I think I’ve learned how to stop worrying. You just have to remember that nothing lasts forever.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"All bad things come to an end.",
						"I don’t know if things will change just like that.",

					},
					new Godot.Collections.Array{
						406,
						407
					}
				);
				break;
			
			case 406:
				GameController.trustLevels[GameController.TEACHER] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yeah. ", 
						"It’s almost like… a little voice in my head is saying, ‘Don’t worry! This’ll all be over soon.’",
						"…The Teacher smiles to themselves.",

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
			case 407:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"I guess that’s true. We’ve just got to keep at it, right?",
						"But I just have this feeling it’ll all be over soon.",
						"…The Teacher smiles to themselves.",

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
			
			case 500:
				GameController.trustLevels[GameController.TEACHER] += 1f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"I don’t have any good reason to think that way. I’m certain this planet’s leadership will keep the war going as long as it’s profitable for them.",
						"And it’ll keep being profitable as long as people have the Colony to direct their anger toward.",
						"It’s a wonderful lie, isn’t it? This government telling us the Colony is to blame for our misfortune, while dealing out pain and suffering to us both?",
						"And no, I don’t believe people will ever see the truth.",
						"In 50 years, of course, someone will look back and say: ‘I knew this was wrong all along’.",
						"And it might even be true.",
						"But still…",
						"There might just be an end to all the suffering soon.",
						"It’s just this feeling that I have.",
						"The New Year is coming up, after all..."

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

			default:
				dialogueSet = null;
				break;
		}
		return dialogueSet;
	}
}
