using Godot;
using System;

public partial class UpgradeProgress : ProgressBar
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Value = GetProgress();
	}

	private float GetProgress()
	{
		Arsenal arsenal = (Arsenal)GetParent().GetParent().GetParent();

		// Return the progress in percent.
		return 100 * (float)arsenal.Coins / (float)arsenal.NextUpgradeCost;
	}
}
