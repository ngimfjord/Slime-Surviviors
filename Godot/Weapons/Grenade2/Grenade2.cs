using Godot;
using System;

public partial class Grenade2 : CharacterBody2D
{
	public override void _Process(double delta)
	{
		// Creates five grenades in a spread pattern
		for (int i = -2; i < 3; i++)
		{
			// Load the grenade scene
			PackedScene grenadeScene = (PackedScene)ResourceLoader.Load("res://Weapons/Grenade1/grenade.tscn");
			Grenade grenade = (Grenade)grenadeScene.Instantiate();

			// Set the position and rotation of the grenade
			grenade.Position = GlobalPosition;
			grenade.Rotation = GlobalRotation + i * 0.3f;

			// Set the properties of the grenade
			grenade.Speed = 300;
			grenade.Duration = 2f;
			grenade.NumberOfFlames = 100;
			grenade.FlamesPerFrame = 6;
			grenade.FlameSpeed = 100;
			grenade.FlameDamage = 0.005f;
			grenade.FlameFriction = 0.04f;

			GetParent().GetParent().GetParent().AddChild(grenade);
		}

		// Free the node
		QueueFree();
	}
}
