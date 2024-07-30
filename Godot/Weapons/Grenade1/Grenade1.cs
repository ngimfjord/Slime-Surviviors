using Godot;
using System;

public partial class Grenade1 : CharacterBody2D
{
	public override void _Process(double delta)
	{
		// Load the grenade scene
		PackedScene grenadeScene = (PackedScene)ResourceLoader.Load("res://Weapons/Grenade1/grenade.tscn");
		Grenade grenade = (Grenade)grenadeScene.Instantiate();
		grenade.Position = GlobalPosition;
		grenade.Rotation = GlobalRotation;
		
		grenade.Speed = 200;
		grenade.Duration = 2f;
		grenade.NumberOfFlames = 50;
		grenade.FlamesPerFrame = 4;
		grenade.FlameSpeed = 100;
		grenade.FlameDamage = 0.005f;
		grenade.FlameFriction = 0.05f;

		// Add the scene to the world node.
		GetParent().GetParent().GetParent().AddChild(grenade);

		// Free the node
		QueueFree();
	}
}
