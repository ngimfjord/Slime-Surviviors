using Godot;
using System;

public partial class Coin : Area2D
{
	public float Worth { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AnimateObject();
	}

	// Called if a monitored body enters the area. The coin scene monitors the player.
	private void _on_body_entered(Node2D body)
	{
		if (body is Player)
		{
			// Access the players arsenal.
			Player player = (Player)body;
			Arsenal arsenal = (Arsenal)player.GetNode("Arsenal");

			// Debugging
			GD.Print("Coin collected");

			// Add a coin to the players arsenal.
			arsenal.AddCoins((int)Worth);

			// Remove the coin from the scene.
			QueueFree();
		}
	}

	private void AnimateObject()
	{
		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite.Animation = "default";
		animatedSprite.Play();
	}
}


