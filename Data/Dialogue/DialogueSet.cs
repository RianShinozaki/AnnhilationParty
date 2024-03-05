using Godot;
using System;

[GlobalClass]
public partial class DialogueSet : Resource
{
	public Godot.Collections.Array lines;
	public Godot.Collections.Array ids;
	public Godot.Collections.Array responses;
	public Godot.Collections.Array nextLines;

	public DialogueSet(Godot.Collections.Array theLines, Godot.Collections.Array theIds, Godot.Collections.Array theResponses, Godot.Collections.Array theNextLines) {
		lines = theLines;
		ids = theIds;
		responses = theResponses;
		nextLines = theNextLines;
	}

}
