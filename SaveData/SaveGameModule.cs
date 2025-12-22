using Godot;
using System;
using Godot.Collections;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class SaveGameModule : Node
{
	private const string SavePath = "res://SaveData//SaveData.json";

	public static void SaveGameData(string dictionaryKey, Variant newValue)
	{
		if (dictionaryKey == "ProgressScene")
		{
			if ((int)newValue == 1)
			{
				newValue = SceneSwitcher.Set1_OnlineWorld;
			}
			else if ((int)newValue == 2)
			{
				newValue = SceneSwitcher.Set2_SashaBedroom;
			}
			// etc. for all starting sets
		}
		
		// This saves individual pieces of information 
		var data = LoadGameData();

		if (data.ContainsKey(dictionaryKey))
		{
			data[dictionaryKey] = newValue;
		}
		
		SaveJsonData(data);

	}

	public static Dictionary LoadGameData()
	{
		// Check if file exists and if not then create the data
		if (!FileAccess.FileExists(SavePath))
		{
			GD.PrintErr("Save file not found: " + SavePath + ". Creating New File");
			var initialData = new Dictionary
			{
				{ "ProgressScene", SceneSwitcher.Set1_OnlineWorld } // Change this to the initial scene
			};
			SaveJsonData(initialData);
			return initialData;
		}
		
		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
		string jsonData = file.GetAsText();
		var parseResult = Json.ParseString(jsonData);

		var gameData = (Dictionary)parseResult;
		if (gameData != null)
		{
			return gameData;
		}
		else
		{
			GD.Print("Failed to read json save data");
			return new Dictionary();
		}
	}
	private static void SaveJsonData(Dictionary data)
	{
		string jsonString = Json.Stringify(data, indent: "\t", sortKeys: true);
		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
		file.StoreString(jsonString);
	}
}
