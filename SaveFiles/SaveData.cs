namespace MyFathersHomeProject.SaveFiles;

public class SaveData
{
	public int SceneIndex { get; set; } = 0;
	public string SceneUid { get; set; } = "";
	public Godot.Collections.Array<string> SceneNames { get; set; } = new();
}