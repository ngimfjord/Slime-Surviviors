using Godot;

public partial class MoonMagic1 : CharacterBody2D
{
	public override void _Process(double delta)
	{
		// Load the crescent scene
		PackedScene crescentScene = (PackedScene)ResourceLoader.Load("res://Weapons/Moon magic1/crescent.tscn");
		Crescent crescent = (Crescent)crescentScene.Instantiate();
		crescent.Position = GlobalPosition;
		crescent.Rotation = GlobalRotation;

		crescent.Speed = 200;
		crescent.Damage = 0.8f;
		crescent.Duration = 5f;

		// Add the scene to the world node.
		GetParent().GetParent().GetParent().AddChild(crescent);

		// Free the node
		QueueFree();
	}
}
