using System;
using Godot;
using System.Text.Json;

namespace MyFathersHomeProject.SaveFiles;

public partial class SaveManager : Node
{
	private const string SavePath = "res://SaveFiles//SaveData.json";

	public static void SaveGameData(SaveFiles.SaveData saveData)
	{
		var jsonSaveDataString = JsonSerializer.Serialize(saveData);
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
		SaveData? deserializedData = null;
		try
		{
			deserializedData = JsonSerializer.Deserialize<SaveData>(jsonData);
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Deserialization failed for {nameof(SaveData)}: {ex.Message}");
		}

		return deserializedData;
	}
}