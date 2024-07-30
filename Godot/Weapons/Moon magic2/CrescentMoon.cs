using Godot;
using System;
using System.Data.Common;
using System.Linq.Expressions;

public partial class CrescentMoon : CharacterBody2D
{
	public float Speed {get; set;}
	public float Damage {get; set;}
	public float HP {get; set;}
	public float Duration {get; set;}
	public float ElapsedTime {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Calculate how long the projectile has been alive.
		ElapsedTime += (float)delta;

		// Check if the projectile has been alive for longer than its lifetime.
		if (ElapsedTime >= Duration)
		{
			QueueFree();
		}

		Vector2 angle = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));

		KinematicCollision2D collisionInfo = MoveAndCollide(angle * Speed * (float)delta);

		if (collisionInfo != null)
		{

			if (collisionInfo.GetCollider() is Slime)
			{
				Slime slime = (Slime)collisionInfo.GetCollider();

				// The projectile is damaged when it hits the slime and vice versa
				HP -= slime.CurrentHP;

				slime.TakeDamage(Damage);

				// Check if the projectile has been destroyed
				if (HP <= 0)
				{
					QueueFree();
				}
			}
		}
	}

	public void AnimateSprite ()
	{
		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		animatedSprite.Animation = "default";

		animatedSprite.Play();
	}
}
