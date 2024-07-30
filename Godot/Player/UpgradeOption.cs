using Godot;
using System;

public partial class UpgradeOption : ColorRect
{
	public bool MouseOver { get; set; }
	public int Index { get; set; }
	public string UpgradeName { get; set; }
	public int UpgradeLevel { get; set; }
	public string Type { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MouseOver = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (MouseOver && Input.IsActionJustPressed("click"))
		{
			Arsenal arsenal = (Arsenal)GetParent().GetParent().GetParent().GetParent().GetParent();

			arsenal.UpgradeWeapon(Index, Type);
		}
	}

	private void OnMouseEntered()
	{
		MouseOver = true;

		// Debugging
		GD.Print("Mouse entered");
	}


	private void OnMouseExited()
	{
		MouseOver = false;
	}

	public void UpdateText()
	{
		Label nameLabel = GetNode<Label>("Name");
		nameLabel.Text = UpgradeName;


		if (Type == "upgrade")
		{
			Label levelLabel = GetNode<Label>("Level");
			levelLabel.Text = "Level: " + UpgradeLevel.ToString();
		}
		else if (Type == "speed up")
		{
			Label levelLabel = GetNode<Label>("Level");
			levelLabel.Text = "Speed up 50%";
		}
	}
}


