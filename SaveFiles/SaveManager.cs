using System;
using Godot;
using System.Text.Json;

namespace MyFathersHomeProject.SaveFiles;

public partial class SaveManager : Node
{
	private const string SavePath = "user://SaveData.json";

	public static void SaveGameData(int setId)
	{
		var jsonSaveDataString = JsonSerializer.Serialize(new SaveData(setId));
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
			// var timeSinceSave = TimeSinceSave();
			var saveTime = ModifiedTime();
			if (deserializedData != null) deserializedData.SaveTime = saveTime;
			// load a scene
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Deserialization failed for {nameof(SaveData)}: {ex.Message}");
		}

		return deserializedData;
	}

	private static string ModifiedTime()
	{
		var timezone = TimeZoneInfo.Local;
		DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeSeconds((long)FileAccess.GetModifiedTime(SavePath));
		DateTime saveTime = TimeZoneInfo.ConvertTime(utcTime, timezone).DateTime;
		
		return saveTime.ToString();
		
	}

	/*private static string TimeSinceSave()
	{
		var modifiedTime = FileAccess.GetModifiedTime(SavePath);
		var currentTime = (ulong)Math.Round(Time.GetUnixTimeFromSystem());
		var timeSinceSave = (currentTime - modifiedTime); // secs

		var hours = timeSinceSave / 3600;
		var minutes = (timeSinceSave % 3600) / 60;
		var seconds = timeSinceSave % 60;
		
		var timeSinceSaveString = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
		
		
		
		return timeSinceSaveString;
	}*/
}