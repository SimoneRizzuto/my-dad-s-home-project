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
			var saveTime = ModifiedTime();
			if (deserializedData != null) deserializedData.SaveTime = saveTime;
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Deserialization failed for {nameof(SaveData)}: {ex.Message}");
		}

		return deserializedData;
	}

	private static string? ModifiedTime()
	{
		var timezone = TimeZoneInfo.Local;
		var utcTime = DateTimeOffset.FromUnixTimeSeconds((long)FileAccess.GetModifiedTime(SavePath));
		var saveTime = TimeZoneInfo.ConvertTime(utcTime, timezone).DateTime;
		var saveTimeString = saveTime.ToString();

		return saveTimeString;
	}
}