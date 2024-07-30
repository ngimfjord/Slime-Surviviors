using Godot;
using System;

public partial class CoinLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = "Next upgrade: " + GetCoins().ToString();
	}

	private int GetCoins()
	{
		Arsenal arsenal = (Arsenal)GetParent().GetParent().GetParent();

		return arsenal.NextUpgradeCost - arsenal.Coins;
	}
}
