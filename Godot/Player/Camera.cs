using Godot;

public partial class Camera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check if scrolling
		if (Input.IsActionPressed("+"))
		{

			Vector2 newZoom = Zoom;

			newZoom.X += 0.01f;
			newZoom.Y += 0.01f;

			Zoom = newZoom;
		}

		if (Input.IsActionPressed("-"))
		{

			Vector2 newZoom = Zoom;

			newZoom.X -= 0.01f;
			newZoom.Y -= 0.01f;

			Zoom = newZoom;
		}
	}
}
