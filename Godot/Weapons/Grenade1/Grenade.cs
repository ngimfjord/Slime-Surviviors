using Godot;
using System;

public partial class Grenade : CharacterBody2D
{
	public int SpawnedFlames { get; set; }
	public int NumberOfFlames { get; set; }
	public int FlamesPerFrame { get; set; }
	public float FlameSpeed { get; set; }
	public float FlameDamage { get; set; }
	public float FlameFriction { get; set; }
	public bool IsExploding { get; set; }

	public float Speed { get; set; }
	public float Duration { get; set; }
	public float ElapsedTime { get; set; }

	public override void _Ready()
	{
		SpawnedFlames = 0;
		IsExploding = false;
		ElapsedTime = 0;
	}

	public override void _Process(double delta)
	{
		// Play the animation
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();

		// Calculate how long the projectile has been alive.
		ElapsedTime += (float)delta;

		if (!IsExploding)
		{
			// Move the grenade and check for collisions
			KinematicCollision2D collisionInfo = MoveAndCollide(new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * Speed * (float)delta);

			if (collisionInfo != null || ElapsedTime >= Duration)
			{
				IsExploding = true;
			}
		}

		if (IsExploding)
		{
			for (int i = 0; i < FlamesPerFrame; i++)
			{
				if (SpawnedFlames >= NumberOfFlames)
				{
					QueueFree();
					return;
				}

				SpawnedFlames++;
				PackedScene flameScene = (PackedScene)ResourceLoader.Load("res://Weapons/Fire magic1/fire.tscn");
				Fire flame = (Fire)flameScene.Instantiate();
				flame.Position = GlobalPosition;
				float directionVariation = (float)GD.RandRange(0, 2*Mathf.Pi);
				flame.Velocity = new Vector2((float)Math.Cos(directionVariation), (float)Math.Sin(directionVariation)) * FlameSpeed;
				flame.Damage = FlameDamage;
				flame.Friction = FlameFriction;
				AnimatedSprite2D animatedSprite = flame.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
				animatedSprite.SpeedScale = 8f;
				GetParent().AddChild(flame);
			}
		}
	}
}
