using Godot;
using System;

public partial class Slime : CharacterBody2D
{
	public float SpeedPixelsPerSecond { get; set; }
	public float MaxHP { get; set; }
	public float CurrentHP { get; set; }
	public string TargetName { get; set; }
	private bool QueuedForDeletion { get; set; }
	public string Colour { get; set; }

	public override void _Ready()
	{
		// Load the hitpoint bar scene
		PackedScene hitpointBarScene = GD.Load<PackedScene>("res://HitpointsBar/hitpointsbar.tscn");
		HitpointsBar hitpointsBar = (HitpointsBar)hitpointBarScene.Instantiate();
		hitpointsBar.Name = "HitpointsBar";
		hitpointsBar.Position = new Vector2(0, 30);
		hitpointsBar.UpdateHealth(1,1);
		AddChild(hitpointsBar);

		QueuedForDeletion = false;
	}

	public override void _Process(double delta)
	{
		// Get the target node.
		Node2D target = GetNode<Node2D>("../" + TargetName);

		// Get the direction to the target.
		Vector2 direction = target.GlobalPosition - GlobalPosition;

		// Normalize the direction.
		direction = direction.Normalized();

		// Move towards the target.
		KinematicCollision2D collision = MoveAndCollide(direction * SpeedPixelsPerSecond * (float)delta);

		/*
		Check if the slime has collided with the player. The slimes are only able
		to collide with the player, so we can safely assume that the collision is
		with the player.
		*/

		if (collision != null)
		{
			// Get the Game node
			Game game = (Game)GetParent();

			game.PlayerDied();
		}

		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");

		if (direction.X < 0)
		{
			animatedSprite.FlipH = true;
		}
		else
		{
			animatedSprite.FlipH = false;
		}

		// Pick the animation based on the colour of the slime
		animatedSprite.Animation = Colour;

		// Play the animation.
		animatedSprite.Play();
	}

	public void TakeDamage(float damage)
	{
		// Update hitpoints variable
		CurrentHP -= damage;

		// Get the hitpoints bar.
		HitpointsBar hitpointsBar = GetNode<HitpointsBar>("HitpointsBar");

		// Decrease the hitpoints.
		hitpointsBar.UpdateHealth(CurrentHP, MaxHP);

		// Check if the slime is dead.
		if (CurrentHP <= 0 && !QueuedForDeletion)
		{
			// Queue the slime for deletion.
			QueueFree();
			QueuedForDeletion = true;

			// Drop a coin
			PackedScene coinScene = GD.Load<PackedScene>("res://Coin/coin.tscn");
			Coin coin = (Coin)coinScene.Instantiate();
			coin.Worth = MaxHP;
			coin.Position = Position;
			GetParent().AddChild(coin);
			
			// Increase score
			Game game = (Game)GetParent();
			game.Score += MaxHP;
		}
	}
}
