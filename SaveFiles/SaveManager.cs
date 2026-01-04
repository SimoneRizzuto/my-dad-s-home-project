using Godot;
using Godot.Collections;
using System.Text.Json;

namespace MyFathersHomeProject.SaveFiles;

public partial class SaveManager : Node
{
	private const string SavePath = "res://SaveFiles//SaveData.json";

	public static void SaveGameData(SaveFiles.SaveData saveData)
	{
		string jsonSaveDataString = JsonSerializer.Serialize(saveData);
		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
		file.StoreString(jsonSaveDataString);
		file.Close();
	}

	public static SaveData? LoadGameData()
	{
		// Check if file exists and if not then create the data
		if (!FileAccess.FileExists(SavePath))
		{
			GD.PrintErr("Save file not found.");
			return null;
		}

		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
		var jsonData = file.GetAsText();
		var parseResult = Json.ParseString(jsonData);

		var gameData = (Dictionary)parseResult;

		// Loop through keys in Dictionary and update properties of a new SaveData object
		var saveData = new SaveData();
		foreach (var kvp in gameData)
		{
			switch ((string)kvp.Key)
			{
				case "SceneIndex":
					saveData.SceneIndex = (int)kvp.Value;
					break;
				case "SceneUID":
					saveData.SceneUID = (string)kvp.Value;
					break;
				case "SceneNames":
					saveData.SceneNames = (Godot.Collections.Array<string>)kvp.Value;
					break;
			}
		}

		return saveData;
	}
}