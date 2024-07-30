using Godot;

public partial class Player : CharacterBody2D
{
	// Variables.
	public float SpeedPixelsPerSecond { get; set; }

	public float MaxHP { get; set; }
	public float CurrentHP { get; set; }
	public float HPPerSecond { get; set; }

	// Private variables

	// Whether the player is walking, determines if the walking animation should play.
	private bool IsWalkingLeft { get; set; }
	private bool IsWalkingRight { get; set; }

	// Ready function, called when the node is added to the scene.


	public override void _Ready()
	{
		
	}

	// Process function, called every frame.
	public override void _Process(double delta)
	{
		MoveAndCollide(GetMovement(delta));
		AnimateSprite();
		UpdateHealth(delta);
	}

	private Vector2 GetMovement(double delta)
	{
		// Create a new Vector2 to store the movement.
		Vector2 movement = new Vector2();

		// Check if the player is pressing the d key.
		if (Input.IsActionPressed("d"))
		{
			// Set the x value of the movement to -1.
			movement.X = 1;
		}

		// Check if the player is pressing the a key.
		if (Input.IsActionPressed("a"))
		{
			// Set the x value of the movement to 1.
			movement.X = -1;
		}

		// Check if the player is pressing the s key.
		if (Input.IsActionPressed("s"))
		{
			// Set the y value of the movement to -1.
			movement.Y = 1;
		}

		// Check if the player is pressing the w key.
		if (Input.IsActionPressed("w"))
		{
			// Set the y value of the movement to 1.
			movement.Y = -1;
		}

		if (movement.LengthSquared() > 0)
		{
			// Normalize the movement.
			movement = movement.Normalized() * SpeedPixelsPerSecond * (float)delta;

			if (movement.X < 0 || (movement.X == 0 && movement.Y < 0) )
			{
				IsWalkingLeft = true;
				IsWalkingRight = false;
			}
			else
			{
				IsWalkingLeft = false;
				IsWalkingRight = true;
			}
		}
		else
		{
			IsWalkingLeft = false;
			IsWalkingRight = false;
		}

		// Return the movement.
		return movement;
	}

	// Animation function
	private void AnimateSprite()
	{
		// Get the node of the animated sprite.
		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");

		if (IsWalkingLeft)
		{
			// Play the walking left animation.
			animatedSprite.Animation = "walk";
			animatedSprite.FlipH = true;
		}
		else if (IsWalkingRight)
		{
			// Play the walking right animation.
			animatedSprite.Animation = "walk";
			animatedSprite.FlipH = false;
		}
		else
		{
			// Play the idle animation.
			animatedSprite.Animation = "idle";
		}

		animatedSprite.Play();
	}

	private void UpdateHealth (double delta)
	{
		CurrentHP += HPPerSecond * (float)delta;
		if (CurrentHP > MaxHP)
		{
			CurrentHP = MaxHP;
		}
	}
}
