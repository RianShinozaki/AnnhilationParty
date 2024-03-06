using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
	[Export] string name;
	[Export] Texture2D image;
	[Export] string description;
}
