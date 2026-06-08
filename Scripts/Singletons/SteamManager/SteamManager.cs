using Godot;
using Steamworks;

public partial class SteamManager : Node
{
	// private getters
	private static bool initialized;
	// public getters
	public static bool IsInitialized => initialized;

	public override void _Ready()
	{
		if (!SteamAPI.IsSteamRunning())
		{
			GD.PrintErr("Steam is not running!");
			return;
		}

		try
		{
			initialized = SteamAPI.Init();
			if (initialized)
				GD.Print("Steam initialized successfully!");
			else
				GD.PrintErr("Steam failed to initialize.");
		}
		catch (System.Exception e)
		{
			GD.PrintErr("Steam init exception: " + e.Message);
		}
	}

	public override void _Process(double delta)
	{
		if (initialized)
			SteamAPI.RunCallbacks();
	}

	public override void _ExitTree()
	{
		if (initialized)
			SteamAPI.Shutdown();
	}

	public static void UnlockAchievement(string achievementId)
	{
		if (!SteamManager.IsInitialized) return;

		SteamUserStats.SetAchievement(achievementId); // e.g. "FIRST_WIN"
		SteamUserStats.StoreStats(); // Must call this to persist it
	}
	
	// Implement with SteamManager.UnlockAchievement("COMPLETED_SET_1");
	
}