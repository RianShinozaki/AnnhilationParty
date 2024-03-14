using Godot;
using System;

public partial class OfficeSpeaker : Speaker
{
	bool DoIntro = false;
	public static OfficeSpeaker Instance;
	public int lastCalledDay = 0;

	public bool[] suspectsChosen = new bool[4];
	string choiceOne;
	string choiceTwo;
	
	public override void _Ready()
    {
        base._Ready();
		GameController.Instance.BeginIntroSequence += IntroSeq;
		if(DoIntro) {
			GameController.Instance.DoIntro();
		} else {
			BGMPlayer.Instance.BeginPlaying();
		}
		Instance = this;
		//Init();
    }
    public override void _Process(double delta)
    {
		if(!initialized) {
			initialized = true;
		}
    }
	public void IntroSeq() {
		GameController.theSpeaker = this;
		textbox_system.Instance.Initialize(0);
	}

	public void Init() {
		GameController.theSpeaker = this;
		textbox_system.Instance.Initialize(0);
	}

	public void PickupCall() {
		GameController.theSpeaker = this;
		if(lastCalledDay == GameController.currentDay) {
			textbox_system.Instance.Initialize(99);
		} else {
	
			switch(GameController.timesCalledOldGuard) {
				case 0:
					textbox_system.Instance.Initialize(100);
					break;
				case 1:
					textbox_system.Instance.Initialize(110);
					break;
				case 2:
					textbox_system.Instance.Initialize(111);
					break;
				case 3:
					textbox_system.Instance.Initialize(112);
					break;
				case 4:
					textbox_system.Instance.Initialize(115);
					break;
				case 5:
					textbox_system.Instance.Initialize(116);
					break;
				case 6:
					textbox_system.Instance.Initialize(117);
					break;
				case 7:
					textbox_system.Instance.Initialize(118);
					break;
				case 8:
					textbox_system.Instance.Initialize(119);
					break;
				case 9:
					textbox_system.Instance.Initialize(120);
					break;
				case 10:
					textbox_system.Instance.Initialize(121);
					break;
				case 11:
					textbox_system.Instance.Initialize(122);
					break;
				case 12:
					textbox_system.Instance.Initialize(123);
					break;
				case 13:
					textbox_system.Instance.Initialize(124);
					break;
				case 14:
					textbox_system.Instance.Initialize(125);
					break;
				case 15:
					textbox_system.Instance.Initialize(126);
					break;
				default:
					textbox_system.Instance.Initialize(113);
					break;
			}
			GameController.timesCalledOldGuard++;
		}
		lastCalledDay = GameController.currentDay;
		
	}

	public void SpecialCall(int ind) {
		GameController.theSpeaker = this;
		textbox_system.Instance.Initialize(ind);
		
	}

	 public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			
			case 0:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*Four suspects -- Two aberrants.",
						"*aberrants... Eldritch entities that have infested the city.",
						"*Beings that hide in the shadow of the psyche...",
						"*Of the four suspects before you, two of them are unknowingly nursing a nightmare virus that hides in the cloak of their own minds.",
						"*The longer they fester, the more power they gain...",
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						4
					}
				);
				break;
			
			case 4:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You have one month to find them... That is all your briefing said."
					},
					new Godot.Collections.Array{
						
					},
					new Godot.Collections.Array{
						"*Ponder on the idea of Eldritch entities.",
						"Well, this is some bullshit.",
						"Better get to work."
					},
					new Godot.Collections.Array{
						1,
						5,
						6
					}
				);
				break;
			
			case 1:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*Eldritch entities.",
						"*By definition, unknowable and unspeakable horrors.",
						"*...This is not a particularly satisfying definition for your detective mind."
					},
					new Godot.Collections.Array{
						
					},
					new Godot.Collections.Array{
						"They don't really know what it is.",
						"Perchance the true horror is the human mind.",
						"*Give up pondering."
					},
					new Godot.Collections.Array{
						2,
						3,
						4
					}
				);
				break;
			case 7:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"..."
					},
					new Godot.Collections.Array{
						
					},
					new Godot.Collections.Array{
						"They don't really know what it is.",
						"Perchance the true horror is the human mind.",
						"*Give up pondering."
					},
					new Godot.Collections.Array{
						2,
						3,
						4
					}
				);
				break;
			
			case 2:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*Truly, they don't.",
						"*Autopsies coming back with more questions than answers. Lethal blows struck to long-standing theories of physics, psychology, biology...",
						"*It's not simply that they don't know what these horrors are. The existence of these horrors seem to imply we don't know what anything is.",
						"*Cities leveled by these anomalies.",
						"*As if the world needed to be more messed up than it already is."
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
			
			case 3:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*Of all the unknowable, theory-defying branches of science...",
						"*None have given a particularly satisfying answer to the question of the human mind.",
						"*So perhaps this is karma for mankind's ignorance.",
						"*...As you consider this idea with your human mind, you begin to feel reality slipping away...",
						"*You should know better, Mr. Detective. These kinds of thoughts aren't good for you."
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
						"*And truly, it is.",
						"*Shunted into the far rings of the United Solar System, told your very survival rests on this case...",
						"*Only for the case briefing to spout some Fruedian... bullshit.",
						"*You wish you had a better word. You wish they had better words."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						4
					}
				);
				break;
			
			case 6:
				GameController.SetSplitX(225);
				BGMPlayer.Instance.BeginPlaying();
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*No time to mope about the meaning of aberrations and Eldritch beings and whatnot.",
						"*Work calls.",
						"...You've arrived in the office, and the office is still standing, and tentacled multi-eyed horrors are not peeping through the window.",
						"*That much is right with the world so far.",
						"*A previous 'aberration detective,' a fellow sufferer of the Eldritch Bullshit, should be calling you soon...",
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
				Phone.Instance.StartRinging();
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*Ah."
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
			case 99:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You've already called today."
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
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Mornin'. ",
						"This the new blood?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"New blood present.",
						"Chief bullshit detective, reporting for duty.",
						"Good grief. What did this job do to you?"
					},
					new Godot.Collections.Array{
						101,
						102,
						103
					}
				);
				break;
			
			case 101:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Sound just a bit less chipper for me.",
						"Optimistic energy is bad for my blood pressure. My doc told me to avoid it."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						104
					}
				);
				break;
			case 102:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh, good. I was worried I'd have to warn you that this job was criminally stupid.",
						"Looks like we're skipping that part of the welcome call."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						104
					}
				);
				break;
			case 103:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"The hell? I ain't even said anything yet."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						104
					}
				);
				break;
			case 104:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"So, by now you're settled into the old office and the case briefing is nice and settled in your brain, yeah?",
						"I got nothin' for ya on the metaphysics of this whole thing...",
						"But I can tell you how you're gonna do this. So listen up.",
						"I did this gig before you. See, I'm a pretty big shot detective. The last city this happened to, well... the United Solar System actually cared to keep that one.",
						"I think they give just a little more than a super-rat's ass about your city.",
						"So you're good, but you're probably not great. Sorry, champ.",
						"But I still wanna get you through this."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"I feel I'm worth more than a super-rat's ass.",
						"What's the success rate of this operation?",
						"Alright, give me the details."
					},
					new Godot.Collections.Array{
						106,
						107,
						108
					}
				);
				break;
			case 105:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"..."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"I feel I'm worth more than a super-rat's ass.",
						"What's the success rate of this operation?",
						"Alright, give me the details."
					},
					new Godot.Collections.Array{
						106,
						107,
						108
					}
				);
				break;
			case 106:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Said you were.",
						"...Just a little bit.",
						"Frankly, what with the general size of this planet, and that war going on...",
						"I think they see it as more trouble than it's worth.",
						"Easy for them not to give it their all, given half the populace doesn't even believe in... whatever's allegedly going on here. The Aberrants.",
						"So if a city gets annhilated, that's one less headache, and more funding for weaponry.",
						"Some people we put our trust in..."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						105
					}
				);
				break;
			case 107:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"We've lost three colonies over it, but I was able to save one of the central planets.",
						"That puts us at... 25%?",
						"Pretty nice odds."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						105
					}
				);
				break;
			
			case 108:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright, so the details are: You're basically alone out there.",
						"You've gotta find the two aberrants by the end of the month or it's hello, annhilation.",
						"Now, you've got a few tools at your disposal. Thanks to WiFi signal reconstruction and digital surveillance, we can essentially tell you what everyone did each day.",
						"That won't be enough, because the Aberrant rarely reveals itself when it's alone.",
						"However, the presence of the Aberrant can manifest in outbursts of unusual behavior.",
						"Read the logs each day to get a measure of everyone's daily activities, and look for anything that stands out.",
						"However, where the Aberrant really reveals itself is in contact between humans.",
						"You'll need to get close to the suspects.",
						"Befriend them. Throw them off guard. Make them attached to you. Use your information.",
						"Do whatever it takes to get them to open up to you.",
						"You have until the last two days of the month. Then, you'll have to make your decision.",
						"Get either of the Aberrants wrong, and it's over.",
						"Get it right and it's a happy New Year's Day.",
						"Let's recap. Spend time with suspects, and earn their trust. Check the logbook on your desk everyday and learn their habits.",
						"If you see some strange behavior, try to get them to talk to you about it.",
						"...Listen, that's probably enough for today.",
						"I can tell you more if you call again another time. Go ahead and pick up that phone whenever. I'll be there.",
						"Meanwhile, try to make contact with the suspects. Your briefing should have told you where to find them.",
						"It's Friday morning now, yeah? Maybe go see the Butcher -- he's one of your suspects. Grab some lunch, too.",
						"Rooting for ya.",
						"*The phone clicks off.",
						"*...Daily suspect logs on your desk, and photos of the suspects on the wall...",
						"*You can use the phone again tomorrow to learn more. But for now, it could be best to take a look around.",
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
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Hey there. You gone and visited any folks yet?",
						"I guess the briefing told ya where to find them... not necessarily when.",
						"Hope you haven't wasted any time running around and not finding anyone...",
						"Check that logbook. Most people operate on a schedule, and that book should make it pretty obvious when to find someone.",
						"Or, if that doesn't sit right by ya... just hope you get lucky.",
						"*The phone clicks off."
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
			case 111:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"This job requires you to be a real people-person.",
						"When ya talk to people, you're gonna have to break through their reservations.",
						"Most people don't act their true selves around someone they just met...",
						"Doesn't mean you have to appease them. You can even try putting them off balance.",
						"Be a loon for all I care. Makes people think they can say whatever they want to you.",
						"Don't be reserved yourself, though.",
						"*The phone clicks off.",
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
			
			case 112:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Oh yeah, how's the war-fund taking care of ya?",
						"Two-hundred big-ones a week. Imagine that.",
						"...Still, that's all you get.",
						"...",
						"Are you really meant to succeed here? Hell if I know.",
						"I wouldn't be in your position if they paid me a million times that.",
						"*The phone clicks off.",
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
			
			case 113:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*Nobody responds.",
						
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
			
			case 114:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Hey there. You never called back, so I figured I had to check on you.",
						"You're still alive, right?",
						"Yes? Good.",
						"Well, I'm not going to mother-hen you. If you want more information, pick up that phone and call.",
						"If you want to do this yourself, have fun.",
						"*The phone clicks off."
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
			
			case 115:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"This job is a slow burn. You're not gonna get your answers right away, y'know?",
						"Sometimes you meet a person, and there's nothing to say. Well, that's just how it is.",
						"Spending time with them will help them open up to you anyway.",
						"Keep it up, and you'll have options to learn more about them for certain.",
						"*The phone clicks off."
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
			case 116:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"You checking that logbook each day?",
						"If you see somethin' real weird in there... make sure to remember.",
						"Then, when you're close enough with the suspect, you could try learning more about it.",
						"There could be a totally reasonable explanation. Or, there might not be.",
						"You're the judge. That's your job.",
						"*The phone clicks off."
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
			case 117:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Something I've found is people will become closer to you more easily if you're candid.",
						"That being said, try not to be a complete ass either...",
						"But when you want information? Best not to be too direct sometimes.",
						"You have to find a way to have them approach you with the information.",
						"People are bursting to share their secrets when they're not being prompted to...",
						"*The phone clicks off."
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
			case 118:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Hey, you wanna know why the aberration becomes more obvious when you get closer to someone?",
						"It's because that way, the aberration can infect you too.",
						"You won't realize at first. Time will pass without your knowledge. You'll do things you have no recollection of.",
						"If you notice that starting to happen to you...",
						"Well... you have your gun, don't you?",
						"*The phone clicks off."
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
			case 119:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"...I was trying to think of some advice to give you today.",
						"...",
						"I'm not sure if you are, but I feel inclined to tell you...",
						"You shouldn't carry raw meat around everywhere with you...",
						"*The phone clicks off."
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
			case 120:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"What do you think an aberration is anyway?",
						"Most scientific explanation we have is that it's some kinda virus.",
						"The aberration, that is, is the virus. The infected is the aberrant.",
						"By the time the aberration has consumed enough of the host's psyche to become noticeable, it's already too late.",
						"Used to be an old Earth disease like that. Rabies. You ever heard of that?",
						"What a strange universe...",
						"*The phone clicks off."
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
			case 121:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*You hear feedback.",
						"*The phone clicks off."
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
			case 122:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Hey, kid...",
						"You hanging in alright?",
						"*The phone clicks off."
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
			case 123:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"You're well on your way through this case, huh?",
						"Don't forget, when the final day comes... you'll have to make your choice.",
						"Wish we had more tools to give you, kid.",
						"We had roughly two weeks to pull this operation together in the first place...",
						"December 31... once you pick the two aberrants, a team will come in and extract them.",
						"*The phone clicks off."
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
			case 124:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Hey kid...",
						"Are you having a good time?",
						"Don't forget to have some fun. Life's not all work.",
						"*The phone clicks off."
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
			case 125:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"If you can't figure it out, it's alright.",
						"Just close the game and try again.",
						"You'll know what to do next time, right?",
						"*The phone clicks off."
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
			case 126:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Hey kid, I might not be able to answer the phone much longer.",
						"Some stuff came up. Perks of being a world-class detective.",
						"You got it from here, yeah?",
						"*The phone clicks off."
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
			
			case 900:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You pick up the archaic phone.",
						"*...A tired voice comes through.",
						"Hey, kid... you know what day it is, right?",
						"Time to make your choice. It all comes down to this, so no pressure.",
						"Once you make your choice, the suspects in question will be taken away for rehabilitation.",
						"So, I'll ask you now... and in no particular order...",
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						901,
					}
				);
				break;
			case 901:
				suspectsChosen[0] = false;
				suspectsChosen[1] = false;
				suspectsChosen[2] = false;
				suspectsChosen[3] = false;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Who is the first suspect?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"The Butcher.",
						"The Occultist.",
						"The Engineer.",
						"The Teacher."
					},
					new Godot.Collections.Array{
						902,
						903,
						904,
						905
					}
				);
				break;
			case 902:
				suspectsChosen[0] = true;
				choiceOne = "the Butcher";
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright... I understand.",
						"Now, who is the second suspect?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"The Occultist.",
						"The Engineer.",
						"The Teacher."
					},
					new Godot.Collections.Array{
						907,
						908,
						909
					}
				);
				break;
			case 903:
				suspectsChosen[1] = true;
				choiceOne = "the Occultist";

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright... I understand.",
						"Now, who is the second suspect?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"The Butcher.",
						"The Engineer.",
						"The Teacher."
					},
					new Godot.Collections.Array{
						906,
						908,
						909
					}
				);
				break;
			case 904:
				suspectsChosen[2] = true;
				choiceOne = "the Engineer";

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright... I understand.",
						"Now, who is the second suspect?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"The Butcher.",
						"The Occultist.",
						"The Teacher."
					},
					new Godot.Collections.Array{
						906,
						907,
						909
					}
				);
				break;
			case 905:
				suspectsChosen[3] = true;
				choiceOne = "the Teacher";

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright... I understand.",
						"Now, who is the second suspect?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"The Butcher.",
						"The Occultist.",
						"The Engineer.",
					},
					new Godot.Collections.Array{
						906,
						907,
						908
					}
				);
				break;
			
			case 906:
				suspectsChosen[0] = true;
				choiceTwo = "the Butcher";

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						910
					}
				);
				break;
			case 907:
				suspectsChosen[1] = true;
				choiceTwo = "the Occultist";

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						910
					}
				);
				break;
			case 908:
				suspectsChosen[2] = true;
				choiceTwo = "the Engineer";

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						910
					}
				);
				break;
			case 909:
				suspectsChosen[3] = true;
				choiceTwo = "the Teacher";

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						910
					}
				);
				break;
			
			case 910:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"So your choices are... " + choiceOne + " and " + choiceTwo + ".",
						"That right?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"Yep",
						"No, wait..."
					},
					new Godot.Collections.Array{
						911,
						901
					}
				);
				break;
			
			case 911:
				EndSequenceObject.active = true;
				GameController.goodEnding = suspectsChosen[0] == true && suspectsChosen[3] == true;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Alright. We're trusting you on this. We'll be taking the individuals in for rehabilitation shortly.",
						"As for you... we can't transport you out of there today. You're to sit tight until tomorrow, if there is one.",
						"Talk to you later.",
						"*The phone clicks off.",
						"...",
						"*You've made your choice.",
						"*There's nowhere else to go now...",
						"*...Come to think of it, the city's holding a New Years party...",
						"*If you're going to watch the world end, might as well see it there..."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1,
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
