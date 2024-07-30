using Godot;
using System;

public partial class Game : Node2D
{
	private Random random;
	private float SlimeHPToGenerate { get; set; }
	private float BaseSlimeHPPerSecond { get; set; }
	private float EnemySpawnExponent { get; set; }
	private double SecondsSinceStart { get; set; }
	SlimeType[] SlimeTypes { get; set; }
	public float Score { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initialize the random number generator.
		random = new Random();

		// Create a new instance of the player scene.
		PackedScene playerScene = GD.Load<PackedScene>("res://Player/player.tscn");
		Player player = (Player)playerScene.Instantiate();
		player.Name = "Player";
		player.Position = new Vector2(0, 0);
		player.SpeedPixelsPerSecond = 100;
		player.MaxHP = player.CurrentHP = 100;
		player.HPPerSecond = 1;
		AddChild(player);

		// Set slime generation variables
		SlimeHPToGenerate = 0;
		BaseSlimeHPPerSecond = 0.2f;
		EnemySpawnExponent = 1.01f;
		SecondsSinceStart = 0;

		// Set slime types
		SlimeTypes = new SlimeType[]
		{
			new SlimeType { Colour = "purple", Speed = 70, HP = 125 },
			new SlimeType { Colour = "red", Speed = 80, HP = 25 },
			new SlimeType { Colour = "yellow", Speed = 50, HP = 5 },
			new SlimeType { Colour = "blue", Speed = 40, HP = 1 }
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SecondsSinceStart += delta;

		bool newSecond = (int)(SecondsSinceStart + delta) > (int)SecondsSinceStart;

		// Run generate enemies every second
		if (newSecond)
		{
			GenerateEnemies();
		}
	}

	private void GenerateEnemies()
	{
		// Calculate how much HP should be generated
		SlimeHPToGenerate += BaseSlimeHPPerSecond * Mathf.Pow(EnemySpawnExponent, (float)SecondsSinceStart);

		int SlimesGeneratedThisSecond = 0;

		// Calculate how many slimes should be generated based on type to fill quota
		foreach (SlimeType slimeType in SlimeTypes)
		{
			/* 
			Break if more than three slimes has been generated. 

			As the array is sorted descending by HP, the first slimes has the
			highest HP. Besides, the HP is saved until the next second and will
			potentially generate a slime with an even higher HP.
			*/
			
			if (SlimeHPToGenerate >= slimeType.HP)
			{
				while (SlimeHPToGenerate >= slimeType.HP && SlimesGeneratedThisSecond < 3)
				{
					SlimesGeneratedThisSecond++;
					SlimeHPToGenerate -= slimeType.HP;
					GenerateSlime(slimeType, 1);
				}
			}
		}
	}

	private void GenerateSlime(SlimeType slimeType, int numberOfSlimes)
	{
		for (int i = 0; i < numberOfSlimes; i++)
		{
			// Create a new instance of the slime scene.
			PackedScene slimeScene = GD.Load<PackedScene>("res://Slime/slime.tscn");
			Slime slime = (Slime)slimeScene.Instantiate();

			// Set the target for the slime
			slime.TargetName = "Player";

			// Get a random position 1000 pixels away from the player
			slime.Position = GetRandomPosition(1000);

			// Get properties from the slimeType
			slime.SpeedPixelsPerSecond = slimeType.Speed;
			slime.MaxHP = slime.CurrentHP = slimeType.HP;
			slime.Colour = slimeType.Colour;

			// Add the slime to the scene
			AddChild(slime);
		}
	}

	private Vector2 GetRandomPosition(float distance)
	{
		// Generate a random angle between 0 and 2 * pi rad, which is 0 and 360 degrees.
		float angle = (float)random.NextDouble() * 2 * Mathf.Pi;

		// Calculate the x and y coordinates of the random position.
		float x = distance * Mathf.Cos(angle);
		float y = distance * Mathf.Sin(angle);

		// Add the player's position to the random position.
		x += GetNode<Player>("Player").Position.X;
		y += GetNode<Player>("Player").Position.Y;

		return new Vector2(x, y);
	}

	public void PlayerDied()
	{
		Menu menu = (Menu)GetParent();
		
		// Debugging
		GD.Print("Player died! Score: ", Score);

		menu.GameOver((int)Score);

	}
}

struct SlimeType
{
	public string Colour;
	public float Speed;
	public float HP;
}
