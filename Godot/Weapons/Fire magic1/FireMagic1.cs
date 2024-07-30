using Godot;
using System;

public partial class FireMagic1 : CharacterBody2D
{
	private float FlameSpeed { get; set; }
	private float FlamesPerFrame { get; set; }
	private float FlameDamage { get; set; }
	private float LifeTime { get; set; }
	private float TimeAlive { get; set; }
	public float Angle { get; set; }


	public override void _Ready()
	{
		FlameSpeed = 300;
		FlamesPerFrame = 1;
		FlameDamage = 0.005f;
		LifeTime = 0.5f;
		TimeAlive = 0;
	}

	public override void _Process(double delta)
	{
		for (int i = 0; i < FlamesPerFrame*delta; i++)
		{
			PackedScene flameScene = (PackedScene)ResourceLoader.Load("res://Weapons/Fire magic1/fire.tscn");
			Fire flame = (Fire)flameScene.Instantiate();
			flame.Position = GlobalPosition;
			float directionVariation = (float)GD.RandRange(-0.1, 0.1) + GlobalRotation;
			flame.Velocity = new Vector2((float)Math.Cos(directionVariation), (float)Math.Sin(directionVariation)) * FlameSpeed;
			flame.Damage = FlameDamage;
			flame.Friction = 0.02f;

			GetParent().GetParent().GetParent().AddChild(flame);
		}

		TimeAlive += (float)delta;
		if (TimeAlive > LifeTime)
		{
			QueueFree();
		}
	}

	public void Construct(Vector2 position, float angle)
	{
		Position = position;
	}
}
