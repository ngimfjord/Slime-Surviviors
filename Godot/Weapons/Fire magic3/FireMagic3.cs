// using Godot;
// using System;

// public partial class FireMagic3 : CharacterBody2D
// {
//     int Stage { get; set; }

//     public override void _Ready()
//     {
//         Stage = 0;
//     }

//     public override void _Process(double delta)
//     {
//         // Check what stage of the spell we are in
//         if (Stage == 0)
//         {
//             // The player creates a small circle of fire
//             CreateFireCircle();
//         }

//         // Load the flame scene
//         PackedScene flameScene = (PackedScene)ResourceLoader.Load("res://Weapons/Fire magic1/fire.tscn");
//         Fire fireMagic = (Fire)fireMagicScene.Instantiate();
//         fireMagic.Position = GlobalPosition;
//         fireMagic.Rotation = GlobalRotation;
        
//         fireMagic.Speed = 200;
//         fireMagic.Duration = 2f;
//         fireMagic.NumberOfFlames = 50;
//         fireMagic.FlamesPerFrame = 4;
//         fireMagic.FlameSpeed = 100;
//         fireMagic.FlameDamage = 0.005f;
//         fireMagic.FlameFriction = 0.05f;

//         // Add the scene to the world node.
//         GetParent().GetParent().GetParent().AddChild(fireMagic);

//         // Free the node
//         QueueFree();
//     }

//     CreateFireCircle(int amountOfFlames)
//     {
//         for (int i = 0; i < amountOfFlames; i++)
//         {
//             // Load the flame scene
//             PackedScene flameScene = (PackedScene)ResourceLoader.Load("res://Weapons/Fire magic1/fire.tscn");
//             Fire flame = (Fire)flameScene.Instantiate();
//             flame.Position = GlobalPosition;
//             float directionVariation = (float)GD.RandRange(0, Mathf.Pi);
//             flame.Velocity = new Vector2((float)Math.Cos(directionVariation), (float)Math.Sin(directionVariation)) * FlameSpeed;
//             flame.Damage = FlameDamage;
//             flame.Friction = 0.02f;

//             // Debugging
//             // GD.Print("Friction: ", flame.Friction);

//             GetParent().GetParent().GetParent().AddChild(flame);
//         }
//     }
// }