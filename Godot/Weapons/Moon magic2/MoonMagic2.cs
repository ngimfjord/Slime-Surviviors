using Godot;

public partial class MoonMagic2 : CharacterBody2D
{
	public override void _Process(double delta)
	{
		// Load the crescent scene
		PackedScene crescentScene = (PackedScene)ResourceLoader.Load("res://Weapons/Moon magic2/crescentMoon.tscn");
		CrescentMoon crescent = (CrescentMoon)crescentScene.Instantiate();
		crescent.Position = GlobalPosition;
		crescent.Rotation = GlobalRotation;

		crescent.Speed = 200;
		crescent.Damage = 4f;
		crescent.HP = 8f;
		crescent.Duration = 5f;

		// Add the scene to the world node.
		GetParent().GetParent().GetParent().AddChild(crescent);

		// Free the node
		QueueFree();
	}
}
