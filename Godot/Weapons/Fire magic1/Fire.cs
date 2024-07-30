using Godot;
using System;
using System.Collections.ObjectModel;

public partial class Fire : Area2D
{
	public float Damage { get; set; }
	public float Friction { get; set; }
	public Vector2 Velocity { get; set; }

	public override void _Process(double delta)
	{
		// Play the animation
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();

		if (GetNode<AnimatedSprite2D>("AnimatedSprite2D").Frame == 29)
		{
			QueueFree();
		}

		// Move the fire
		Position += Velocity * (float)delta;

		// Slow down the fire
		Velocity = Velocity * (1f - Friction);

		// Debugging
		// GD.Print("Friction: " + Friction);

		// Create a "list" of all the objects that are overlapping with the fire
		Godot.Collections.Array<Node2D> overlappingObjects = GetOverlappingBodies();

		// Loop through all the overlapping objects and damage them
		foreach (Node2D overlappingObject in overlappingObjects)
		{
			if (overlappingObject is Slime)
			{
				Slime slime = (Slime)overlappingObject;

				slime.TakeDamage(Damage);
			}
		}
	}
}


