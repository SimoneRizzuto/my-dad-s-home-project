using Godot;
using Steamworks;

namespace MyFathersHomeProject.Scripts.Singletons.SteamManager;

public partial class SteamManager : Node
{
	// private getters
	private static bool initialized;
	private static bool achievementStatus;

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

		SteamUserStats.GetAchievement(achievementId, out achievementStatus);

		if (achievementStatus)
		{
			GD.Print("Already Unlocked achievement: " + achievementId);
			return;
		}

		SteamUserStats.SetAchievement(achievementId);
		SteamUserStats.StoreStats();
	}


	// Implement with SteamManager.UnlockAchievement("COMPLETED_SET_1");
}