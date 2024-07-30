using Godot;
using System;
using System.IO;

public partial class Menu : Control
{
	int TopScore { get; set; }
	int Score { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Read the top score from the score file
		ReadScore();

		// Update the top score label
		GetNode<Label>("TopScoreLabel").Text = $"Top Score: {TopScore}";
	}

	// Called when the start button is pressed.
	private void OnStartButtonPressed()
	{
		// Load the game scene
		PackedScene gameScene = (PackedScene)ResourceLoader.Load("res://Game/game.tscn");
		Game game = (Game)gameScene.Instantiate();
		AddChild(game);

		// Hide buttons
		GetNode<Button>("StartButton").Hide();
		GetNode<Button>("QuitButton").Hide();
		GetNode<Label>("ScoreLabel").Hide();
		GetNode<Label>("TopScoreLabel").Hide();
	}

	private void OnQuitButtonPressed()
	{
		// Quit the game
		GetTree().Quit();
	}


	public void GameOver(int score)
	{
		// Update the top score if the current score is higher
		if (score > TopScore)
		{
			TopScore = score;
		}

		// Write the top score to the file
		WriteScores();

		// Read the top score from the score file
		ReadScore();

		// Update the score labels
		GetNode<Label>("ScoreLabel").Text = $"Score: {score}";
		GetNode<Label>("TopScoreLabel").Text = $"Top Score: {TopScore}";

		// Show the buttons
		GetNode<Button>("StartButton").Show();
		GetNode<Button>("QuitButton").Show();
		GetNode<Label>("ScoreLabel").Show();
		GetNode<Label>("TopScoreLabel").Show();

		// Free the game node
		GetNode<Game>("Game").QueueFree();
	}

	private void ReadScore()
	{
		// Get the path to the score file
		string scoreFilePath = "score.txt";

		// Check if the score file exists
		if (File.Exists(scoreFilePath))
		{
			// Read the score from the file
			using (StreamReader reader = new StreamReader(scoreFilePath))
			{
				string scoreString = reader.ReadLine();
				if (int.TryParse(scoreString, out int score))
				{
					// Update the top score
					TopScore = score;
				}
			}
		}
		else
		{
			// Default to zero if the score file doesn't exist
			TopScore = 0;
		}
	}
	
	private void WriteScores()
	{
		// Get the path to the score file
		string scoreFilePath = "score.txt";

		// Write the top score to the file
		using (StreamWriter writer = new StreamWriter(scoreFilePath))
		{
			writer.WriteLine(TopScore);
		}
	}
}




