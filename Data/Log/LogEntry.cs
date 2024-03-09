using Godot;
using System;

public partial class LogEntry : Resource
{
	[Export] public string EngineerLog;
	[Export] public string TeacherLog;
	[Export] public string ButcherLog;
	[Export] public string OccultistLog;
	[Export] public int day;
}
