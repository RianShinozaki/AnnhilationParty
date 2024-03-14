using Godot;
using System;

public partial class Occultist : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;
	[Export] public Sprite2D tempObscure;
	float trustAtStartOfMeeting;

	public Godot.Collections.Array questionOptions = new Godot.Collections.Array{
		"I’m just out for a walk tonight.",
		"Wanna sacrifice this rib-eye steak with me?",
		"How about a palm reading?",
		"Can I get another Tarot reading? [$10] ",
		"What’s YOUR fortune?",
		"*Something odd you saw in the logs...",
		"Any more thoughts about the future?"

	};
	public Godot.Collections.Array questionIndices = new Godot.Collections.Array{
		105,
		110,
		150,
		200,
		300,
		400,
		500
	};
	public Godot.Collections.Array relationshipGates = new Godot.Collections.Array{
		0,
		0,
		2,
		0,
		3,
		5,
		5
	};

	public override void _Ready()
    {
		trustAtStartOfMeeting = GameController.trustLevels[GameController.OCCULTER];
        base._Ready();
		GameController.theSpeaker = this;
		GameController.occultistQuestionFlags[0] = true;
		if(GameController.steaks > 0) GameController.occultistQuestionFlags[1] = true;

		if(GameController.currentTime != 1 
			|| GameController.GetDay(GameController.currentDay) == "Monday" 
			|| GameController.GetDay(GameController.currentDay) == "Wednesday") {
				textbox_system.Instance.Initialize(-100);
				NPCSprite.Visible = false;
				tempObscure.Visible = true;
				return;
			}
		if(GameController.occultistMemory[0] == 0) {
			textbox_system.Instance.Initialize(0);
		} else {
			animPlayer.Play("Intro");
		}
    }
    public override void _Process(double delta)
    {
		if(!initialized) {
			initialized = true;
		}
    }

	public void Init() {

		if(GameController.occultistMemory[0] == 0) {

		} else {
			textbox_system.Instance.Initialize(100);
		}
	}

	 public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			case -100:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*The Occultist isn't here.",
						"*Better go back."
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
				break;
			case 0:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You arrive at the park. The air is horribly chilly, and a light mist covers the ground.",
						"*In the center of the park, surrounded by candles, the Occultist sits at a table. She looks bored.",
						"*A sign on the table reads: Tarot readings: $10."
					},
					new Godot.Collections.Array{
						"test_1",
						"test_2",
						"test_3"
					},
					new Godot.Collections.Array{
						"*Time to learn my fortune.",
						"*Actually, let's not."
					},
					new Godot.Collections.Array{
						1,
						-1
					}
				);
				break;
			case 1:
				if(GameController.money > 10) {
					GameController.Instance.ChangeMoney(-10);
					animPlayer.Play("Intro");
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*As you approach the table, the sheer number of occult objects on display amazes you. The Occultist snaps out of her funk when she sees you coming.",
							"Evening, mister.",
							"You wanna get your fortune read, don't you?"
						},
						new Godot.Collections.Array{
							"test_1",
							"test_2",
							"test_3"
						},
						new Godot.Collections.Array{
							"Tell me how I'll die.",
							"Tell me if I'll be alone forever.",
							"Chef's choice."
						},
						new Godot.Collections.Array{
							2,
							5, 
							6
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*You can't afford that...",
							"*You'll have to go home."
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							-1
						}
					);
				}
				break;
			
			case 2:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The occultist gives a bubbly laugh.",
						"You'll be sacrificed at the stake in the middle of a freezing park by an expert on the occult arts.",
						"Your blood will summon the demon god Zulu and end the world as we know it.",
						"What do you think?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Knife me gently, please.",
						"Knife me hard, please."
					},
					new Godot.Collections.Array{
						3,
						4
					}
				);
				break;
			
			case 3:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"It'll be like a soft caress. Don't you worry.",
						"I'll even sing a little song for you as your soul leaves this plane. How does that sound?",
						"..."
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
			case 4:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Occultist laughs again.",
						"Hey. I'm the sadist here. Don't steal my thunder.",
						"..."
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
			case 5:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Occultist studies your face closely.",
						"I can give you an answer to that right now, but you might ask for your 5 dollars back.",
						"Prognosis not great. Sorry champ.",
						"...",
						"Kidding, prettyboy. Don't glare like that."
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
			case 6:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"My choice would be to stake you here in the park and sacrifice your blood to the demon god Zulu.",
						"How's that sound?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Knife me gently, please.",
						"Knife me hard, please."
					},
					new Godot.Collections.Array{
						3,
						4
					}
				);
				break;
			case 7:
				GameController.occultistMemory[0] = 1;
				GameController.occultistQuestionFlags[0] = true;
				GameController.occultistQuestionFlags[2] = true;
				GameController.occultistQuestionFlags[4] = true;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Anyway, that's not really how this works.",
						"I'm just gonna start you off with a past, present, and future reading. That's the basics.",
						"Come back another time if you want something deeper.",
						"*The Occultist shuffles cards with blinding speed as she says this, staring you in the eyes.",
						"So, I'll have you pick three cards now...",
						"Alright...",
						"Now flip them over.",
						"*The Occultist studies your reading.",
						"The past -- Judgement. The present -- the Devil. And the future -- Hierophant, upside down.",
						"Well.",
						"You're a troubled individual, that much is clear.",
						"You shame yourself for your past and live the present with reckless abandon.",
						"And whatever lifeline you have, whoever's looking out for you... they'll turn on you in the end, too.",
						"What an attrocious fortune. You must be totally great."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Can I have a new one?",
						"Can't say that was worth the fiver..."
					},
					new Godot.Collections.Array{
						8,
						9
					}
				);
				break;
			
			case 8:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Nope! Your fortune is forever-standing and utterly annhilating. Nothing you can do.",
						"Notwithstanding world-changing events, such as, for example, the rise of a demon god in this very park.",
						"That's my ultimate goal here, anyway. Your funds are off to a good cause.",
						"I could always use extra hands though.",
						"So if you have any, bring 'em so I can burn them at the altar. Demon god loves good meat.",
						"If you're interested in going out in a blaze of hellish glory instead of slowly withering away, come on back and see me.",
						"...",
						"*You've become acquainted with the Occultist. Time to head back."
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
			case 9:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Too late! Your funds are locked away forever to be used to summon the demon god.",
						"I could always use extra hands though.",
						"So if you have any, bring 'em so I can burn them at the altar. Demon god loves good meat.",
						"If you're interested in going out in a blaze of hellish glory instead of slowly withering away, come on back and see me.",
						"...",
						"*You've become acquainted with the Occultist. Time to head back."
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
					"*You see the Occultist at her fortune-telling stand, surrounded in mist."
				};
				if(GameController.trustLevels[GameController.OCCULTER] < 1) {
					theDialogue.Add("*She's shuffling her cards absentmindedly.");
				}
				if(GameController.trustLevels[GameController.OCCULTER] >= 1 && GameController.trustLevels[GameController.OCCULTER] < 2) {
					theDialogue.Add("*She looks up at you as you approach.");
				}
				if(GameController.trustLevels[GameController.OCCULTER] >= 2 && GameController.trustLevels[GameController.OCCULTER] < 4) {
					theDialogue.Add("*She smiles as she sees you coming near.");
				}
				if(GameController.trustLevels[GameController.OCCULTER] >= 4 && GameController.trustLevels[GameController.OCCULTER] < 5) {
					theDialogue.Add("*The Occultist seems like they're opening up to you.");
				}
				if(GameController.trustLevels[GameController.OCCULTER] >= 5) {
					theDialogue.Add("*You sense a good deal of trust from the Occultist.");
				}

				Godot.Collections.Array theQuestions = new Godot.Collections.Array{};
				Godot.Collections.Array theIndices = new Godot.Collections.Array{};

				bool foundAGate = false;

				for(int i = 0; i < questionOptions.Count; i++) {
					if(GameController.occultistQuestionFlags[i] == true) {
						if (GameController.trustLevels[GameController.OCCULTER] >= (float)relationshipGates[i]) {
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

				theDialogue.Add("*Now's a good time to get to know her better.");

				dialogueSet = new DialogueSet(
					theDialogue,
					new Godot.Collections.Array{
						
					},
					theQuestions,
					theIndices
				);
				break;
			
			case 105:
				GameController.trustLevels[GameController.OCCULTER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"My fortune was right about you. You’d have to be insane to walk in weather like this. ",
						"Who, me?",
						"Don’t worry. The Hellfire keeps me warm.",
						"*You and the Occultist make light conversation, and you go on your way.",

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
				if(GameController.trustLevels[GameController.OCCULTER] >= 5) {
					byeDialogue.Add("*Your trust with the Occultist has maxxed out...");
				} else if(trustAtStartOfMeeting == GameController.trustLevels[GameController.OCCULTER] ) {
					byeDialogue.Add("*You didn't grow much closer today...");
				} else if (Mathf.FloorToInt(trustAtStartOfMeeting) < Mathf.FloorToInt(GameController.trustLevels[GameController.OCCULTER]) ) {
					byeDialogue.Add("*The Occultist definitely trusts you more after today.");
				} else if (trustAtStartOfMeeting < GameController.trustLevels[GameController.OCCULTER] ) {
					byeDialogue.Add("*You think you grew a little closer to the Occultist today.");
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
				GameController.trustLevels[GameController.OCCULTER] += 1f;
				GameController.steaks--;
				GameController.occultistQuestionFlags[1] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh, DO I? That’s the best offer anyone’s made me in years!",
						"These days, nobody asks you if you want to sacrifice a bloody steak with them. They don’t even care.",
						"Sorry – I’m going on a tangent. Let’s do this.",
						"*You and the Occultist make a fire and burn the Rib-Eye Steak as an offering.",
						"*…The demon god Zulu better be pleased with his expensive meal.",
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
				GameController.trustLevels[GameController.OCCULTER] += 0.75f;
				GameController.occultistQuestionFlags[2] = false;
				GameController.occultistQuestionFlags[3] = true;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Eh? Does my sign say I offer palm readings?",
						"*…It does not.",
						"It does not. You just thought you were being smooth, weren’t you?",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Frankly, I’m just looking for some good news.",
						"Damn. Foiled again.",
					},
					new Godot.Collections.Array{
						151,
						152
					}
				);
				break;
			
			case 151:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Awww... poor you.",
						"*The Occultist smirks at you."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						153
					}
				);
				break;
			case 152:
				GameController.trustLevels[GameController.OCCULTER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Occultist laughs at your straight-forwardness."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						153
					}
				);
				break;
			case 153:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"…Oh, come here. I’ve been reading about this stuff anyway – might as well practice.",
						"*The Occultist grabs your hand and begins studying your palm.",
						"Let’s see… so this one that folds with your thumb is the life line.",
						"Hmm… it’s broken up. People say that means you’re headed for a short life, but actually, that says you’re a very independent person.",
						"I guess you rely on yourself more than anyone.",
						"Oooh, let’s do your fate line. Have your palms been itchy lately?",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{		
						"Very much so.",
						"Not really?",
					},
					new Godot.Collections.Array{
						154,
						155
					}
				);
				break;
			case 154:
				GameController.trustLevels[GameController.OCCULTER] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"You’re headed for some real big life changes, my guy.",
						"*The Occultist looks at you with renewed interest.",
						"Isn’t that exciting?",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{		
					},
					new Godot.Collections.Array{
						156
					}
				);
				break;
			case 155:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"I see… still, looks like you may not be entirely in control of your life.",
						"That must be rough for you, considering how you like to live.",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{		
					},
					new Godot.Collections.Array{
						156
					}
				);
				break;
			case 156:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"…",
						"Damn. That’s all I remember.",
						"Well, I’ll give you a make-up palm reading later. Free of charge.",
						"*The Occultist smirks at you.",
						"So I’m guessing you’re still not happy with your Tarot reading. Can’t blame you, but man, what a cool fortune.",
						"I used to dream about having a fortune like that. Unfortunately, my life turned out to be painfully mundane…",
						"So now I stand around in a park in the middle of the night and predict doom and gloom on others.",
						"It’s almost just as good. So, see – you can change your destiny. A little.",
						"I can’t change your fate for you, but I could try to divine some extra guidance for you.",
						"Why not come back for another Tarot reading?",
						"If you’re looking for answers – maybe it’ll help.",

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
				if(GameController.money >= 10) {
					GameController.trustLevels[GameController.OCCULTER] += 1f;
					GameController.Instance.ChangeMoney(-10);
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"What do you think I am, a master of the mystic arts?",
							"Of course I am! Let’s do this.",
							"*The Occultist shuffles her cards with blinding speed.",
							"Let’s see here..",
							"*The Occultist shuffles and pulls cards herself, organizing them into sections…",
							"…",
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{		
						},
						new Godot.Collections.Array{
							201
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
							100
						}
					);
				}
				break;
			case 201:
				string theFortune = "...Nothing's showing up. How odd.";
				RandomNumberGenerator rand = new RandomNumberGenerator();
				
				int fortune = 0;

				if(GameController.occultistMemory[1] > 2) {
					fortune = rand.RandiRange(0, 2);
				}
				else {
					fortune = GameController.occultistMemory[1];
				}

				switch(fortune) {
					case 0:
						if(GameController.currentDay >= 15) {
							theFortune = "...Something will happen on the 15th. Don't miss anything from that day.";
						} else {
							theFortune = "...Something happened on the 15th. Make sure you didn't miss anything from that day.";
						}
						break;
					case 1:
						if(GameController.currentDay >= 22) {
							theFortune = "...Something will happen on the 22nd. Don't miss anything from that day.";
						} else {
							theFortune = "...Something happened on the 22nd. Make sure you didn't miss anything from that day.";
						}
						break;
					case 2:
						if(GameController.currentDay >= 12) {
							theFortune = "...Something will happen on the 12th. Don't miss anything from that day.";
						} else {
							theFortune = "...Something happened on the 12th. Make sure you didn't miss anything from that day.";
						}
						break;
				}

				GameController.occultistMemory[1]++;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						theFortune
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
				GameController.trustLevels[GameController.OCCULTER] += 0.75f;
				GameController.occultistQuestionFlags[4] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"MY fortune, huh? I ought to stake you for a question like that… but I suppose it’s only fair.",
						"To be honest, the answer’s not all too interesting.",
						"See, I used to read my own fortunes all the time to see if I’d get anything exciting.",
						"And, well… I never did. It was always ‘look inwards.’ ‘find your own journey.’ Meanwhile, I read fortunes for anyone else and it was always death, Hellfire, etc. The good stuff.",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{	
						"That’s really what you wanted?",
						"Here, hand me the cards. I got this.",
	
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
						"Well... yeah."
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
				GameController.trustLevels[GameController.OCCULTER] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Occultist laughs at you.",
						"Yeah - nice try."
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
						"But who wouldn’t want things to suddenly change? Isn’t life just so… empty, sometimes?",
						"Things are always turning towards the worst, anyway.",
						"And I’ve never met a person that didn’t let me down.",
						"Might as well have it all go out in a catastrophic hellstorm, right?",
						"…",
						"And if that can’t happen, I can still pretend, right?",
						"*…The Occultist has no more to say on that topic.",

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
				GameController.occultistQuestionFlags[5] = false;
				GameController.occultistQuestionFlags[6] = true;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*Something odd you saw in the logs...",
						"*That’s right… something you saw didn’t seem to add up. What was it? ",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{	
						"The Occultist went to a rehab group downtown.",
						"The Occultist crashed a vehicle into a supermarket.",
						"The Occultist set fire to her house.",

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
						"So… what’s something fun to do in town?",
						"Do you think fortunes really change?",
						"Lots of people smoking around here, huh?",
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
						"In town? Man, I dunno. I… don’t go down there often.",
						"*...The Occultist has no more to say on the topic.",
						"*Maybe you need a different approach? You should try again later..."

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
						"Sure, I guess.",
						"*...The Occultist has no more to say on the topic.",
						"*Maybe you need a different approach? You should try again later..."

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
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"What, are we thinking about imminent doom again?",
						"*The Occultist smiles at you. A moment later, her face turns thoughtful.",
						"Nobody knows, really. Not unless you can actually look into the future.",
						"How do you know that what you do will have an impact? You don’t. There’s just no way.",
						"But I think…",
						"That’s not an excuse to avoid trying to be active in your own life.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"It’s better than just seeing what happens.",
						"Change is difficult.",
					},
					new Godot.Collections.Array{
						406,
						407
					}
				);
				break;
			
			case 406:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yeah, exactly.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						408
					}
				);
				break;
			case 407:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, yeah. It is. It really is… But still…",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						408
					}
				);
				break;
			
			case 408:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"You could wait your entire life for something to change if you just wait for things to happen.",
						"I guess I’ve been thinking about that a lot lately.",
						"It’s just about being honest with yourself, right? I’m not really satisfied with my life. And I’ve got good and… not so good ways of dealing with that.",
						"But all of that is avoiding the real problem.",
						"So I’m not sure what I’ll do next, but I’m gonna think real hard about it. No fortune telling this time.",

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
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"The future, huh? Yeah. Lots, actually",
						"I think I’m closing down my little operation here for a while.",
						"Why?",
						"Cause! Come next year, this occultist is… going to night school!",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Wait, no more fortunes…?",
						"Night school for what?",
					},
					new Godot.Collections.Array{
						501,
						502
					}
				);
				break;
			case 501:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Nope! I’ve been getting tired of this gig anyway.",
						"Nope, the next chapter of my life is…",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						503
					}
				);
				break;
			case 502:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Occultist gives you a sly smile.",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						503
					}
				);
				break;
			case 503:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Clinical psychology!",
						"*The Occultist does excited jazz hands.",
						"See, I was thinking… it’s fun and all to predict peoples’ misfortunes, but…",
						"Maybe there’s a better way of guiding people towards the future.",
						"Not with chance or magic or anything invisible…",
						"But by changing your way of life every single day until you’re living in the future you want.",
						"I’ll miss my fortunes of doom, but I think this is gonna be a hell of a lot more rewarding.",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"How about one last fortune, then?",
					},
					new Godot.Collections.Array{
						504
					}
				);
				break;
			
			case 504:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"One last fortune, huh? Easy, I’ll do one for the 31st, and I won’t even use the cards.",
						"I’m gonna take down my stand, burn the last of my offerings, and then I’m off to the New Year’s party.",
						"Gotta enjoy a new start with style, right?",
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
