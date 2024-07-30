using Godot;
using System;

public partial class HitpointsBar : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Play the animation
		Play();
	}

	public void UpdateHealth(float health, float maxHealth)
	{
		// Calculate the health percentage
		float healthPercentage = health / maxHealth;

		// Set animation based on health percentage
		if (healthPercentage > 0.99f)
		{
			Animation = "1000";
		}
		else if (healthPercentage > 0.875f)
		{
			Animation = "0875";
		}
		else if (healthPercentage > 0.75f)
		{
			Animation = "0750";
		}
		else if (healthPercentage > 0.625f)
		{
			Animation = "0625";
		}
		else if (healthPercentage > 0.5f)
		{
			Animation = "0500";
		}
		else if (healthPercentage > 0.375f)
		{
			Animation = "0375";
		}
		else if (healthPercentage > 0.25f)
		{
			Animation = "0250";
		}
		else
		{
			Animation = "0125";
		}
	}
}
