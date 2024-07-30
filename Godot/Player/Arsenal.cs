using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Godot;


public partial class Arsenal : Node2D
{
	// Weapons
	public List<Weapon> Weapons { get; set; }

	// Coins
	public int Coins { get; set; }
	public int NextUpgradeCost { get; set; }

	// Random number generator
	private RandomNumberGenerator rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Weapons = new List<Weapon>
		{
			new Weapon { Name = "Moon magic", SceneName = "moonMagic", Level = 0, MaxLevel = 2, Cooldown = 3f, CooldownLeft = 0f },
			new Weapon { Name = "Fire magic", SceneName = "fireMagic", Level = 0, MaxLevel = 2, Cooldown = 10f, CooldownLeft = 0f },
			new Weapon { Name = "Grenade", SceneName = "grenade", Level = 0, MaxLevel = 2, Cooldown = 2f, CooldownLeft = 0f }
		};

		Coins = 0;
		NextUpgradeCost = 20;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool hasWeapon = false;

		for (int i = 0; i < Weapons.Count; i++)
		{
			if(Weapons[i].Level > 0)
			{
				hasWeapon = true;
				if (Weapons[i].CooldownLeft > 0)
				{
					Weapons[i].CooldownLeft -= (float)delta;
				}
				else if (Weapons[i].Level > 0)
				{
					UseWeapon(Weapons[i], GetClosestEnemyPosition());
					Weapons[i].CooldownLeft = Weapons[i].Cooldown;
				}
			}
		}

		if (!hasWeapon)
		{
			// Open the upgrade menu if the player has no weapons.
			OpenUpgradeMenu();
		}

		if (Coins >= NextUpgradeCost)
		{
			// Remove the coins required for the upgrade and calculate the next upgrade cost.
			Coins -= NextUpgradeCost;
			NextUpgradeCost = (int)((float)NextUpgradeCost * 1.5);

			// Open the upgrade menu.
			OpenUpgradeMenu();
		}
	}

	private Vector2 GetClosestEnemyPosition()
	{
		Vector2 slime_position = new Vector2(0, 0);
		float slime_distance = float.MaxValue;

		foreach (Node2D node in GetParent().GetParent().GetChildren())
		{
			if (node is Slime)
			{
				if (node.Position.DistanceSquaredTo(GlobalPosition) < slime_distance)
				{
					slime_distance = node.Position.DistanceSquaredTo(GlobalPosition);
					slime_position = node.Position;
				}
			}
		}

		return slime_position;
	}

	private void UseWeapon(Weapon weapon, Vector2 target)
	{
		// Load the weapon scene.
		// Example path: "res://Weapons/Moon magic1/moonMagic1.tscn"
		string scene_path = "res://Weapons/" + weapon.Name + weapon.Level.ToString() + "/" + weapon.SceneName + weapon.Level.ToString() + ".tscn";

		PackedScene weapon_scene = GD.Load<PackedScene>(scene_path);

		CharacterBody2D weapon_instance = (CharacterBody2D)weapon_scene.Instantiate();

		// Rotate the weapon to face the target.
		Vector2 direction = target - GlobalPosition;
		Rotation = direction.Angle();

		// Add an instance of the weapon to the arsenal node.
		AddChild(weapon_instance);
	}

	public void AddCoins(int coins)
	{
		Coins += coins;
		
		// Debugging
		GD.Print("Coins: ", Coins);
	}

	private void OpenUpgradeMenu()
	{
		// Pause the game
		GetTree().Paused = true;

		// Load the LevelUpPanel Node
		CanvasLayer canvasLayer = GetNode<CanvasLayer>("GraphicInterface");
		Control control = canvasLayer.GetNode<Control>("GUI");
		Panel levelUpPanel = control.GetNode<Panel>("LevelUpPanel");
		VBoxContainer options = levelUpPanel.GetNode<VBoxContainer>("UpgradeOptions");
		
		// Get the upgrade options
		List<UpgradeTypes> upgradeTypes = GetUpgrades();

		// Add three upgrade options
		for (int i = 0; i < 3; i++)
		{
			// Get a random upgrade index and type from the list of upgrades
			int randomIndex = rng.RandiRange(0, upgradeTypes.Count - 1);
			int index = upgradeTypes[randomIndex].Index;
			string type = upgradeTypes[randomIndex].Type;
			
			// Remove the upgrade option from the list so it doesn't appear twice.
			upgradeTypes.RemoveAt(randomIndex);

			// Load the upgrade option scene
			PackedScene upgradeOptionScene = GD.Load<PackedScene>("res://Player/upgradeOption.tscn");
			UpgradeOption upgradeOption = (UpgradeOption)upgradeOptionScene.Instantiate();

			// Set the upgrade option properties
			// Set the index of the weapon to upgrade
			upgradeOption.UpgradeName = Weapons[index].Name;
			upgradeOption.UpgradeLevel = Weapons[index].Level + 1;
			upgradeOption.Type = type;
			upgradeOption.Index = index;

			// Update the text in the box
			upgradeOption.UpdateText();

			// Add the upgrade option to the list of options
			options.AddChild(upgradeOption);
		}

		levelUpPanel.Visible = true;
	}

	internal void UpgradeWeapon(int index, string type)
	{
		if (type == "speed up")
		{
			// Speed up the weapon
			Weapons[index].Cooldown /= 1.5f;
		}
		if (type == "upgrade")
		{
			// Increase the weapon level
			Weapons[index].Level++;
		}
		
		// Load the LevelUpPanel Node and make it invisible
		CanvasLayer canvasLayer = GetNode<CanvasLayer>("GraphicInterface");
		Control control = canvasLayer.GetNode<Control>("GUI");
		Panel levelUpPanel = control.GetNode<Panel>("LevelUpPanel");
		levelUpPanel.Visible = false;

		// Remove all children from the UpgradeOptions Node
		VBoxContainer options = levelUpPanel.GetNode<VBoxContainer>("UpgradeOptions");
		foreach (Node child in options.GetChildren())
		{
			child.QueueFree();
		}

		// Unpause the game
		GetTree().Paused = false;
	}

	private List<UpgradeTypes> GetUpgrades()
	{
		List<UpgradeTypes> upgradeTypes = new List<UpgradeTypes>();

		foreach (Weapon weapon in Weapons)
		{
			if (weapon.Level < weapon.MaxLevel)
			{
				// The weapon level is lower than its maximum and can be upgraded
				upgradeTypes.Add(new UpgradeTypes { Index = Weapons.IndexOf(weapon), Type = "upgrade" });
			}
			if (weapon.Level > 0)
			{
				// The weapon level is owned and can be sped up
				upgradeTypes.Add(new UpgradeTypes { Index = Weapons.IndexOf(weapon), Type = "speed up" });
			}
		}

		return upgradeTypes;
	}
}

public class Weapon 
{
	public string Name { get; set; }
	public string SceneName { get; set; }
	public int Level { get; set; }
	public int MaxLevel { get; set; }
	public float Cooldown { get; set; }
	public float CooldownLeft { get; set; }
}

public class UpgradeTypes
{
	public int Index { get; set; }
	public string Type { get; set; }
}
