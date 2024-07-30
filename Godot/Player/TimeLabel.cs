using Godot;
using System;

public partial class TimeLabel : Label
{
	ulong StartTime { get; set; }
	ulong TimePassed { get; set; }
	public override void _Ready()
	{
		// Get the current time
		StartTime = Time.GetTicksMsec();
	}

	public override void _Process(double delta)
	{
		// Calculate the time passed
		TimePassed = (Time.GetTicksMsec() - StartTime);

		// Calculate minutes and seconds
		ulong minutes = TimePassed / 60000;
		ulong seconds = (TimePassed % 60000) / 1000;

		// Create strings for the minutes and seconds
		string minutesString = minutes.ToString();
		string secondsString = seconds.ToString();

		// Add a leading zero if the seconds are less than 10
		if (seconds < 10)
		{
			secondsString = "0" + secondsString;
		}

		Text = (minutesString + ":" + secondsString);
	}
}
