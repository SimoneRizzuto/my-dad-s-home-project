namespace MyFathersHomeProject.SaveFiles;
public class SaveData
{
	public SaveData(int setId)
	{
		SetId = setId;
		SaveTime = null;
	}

	public string SaveTime { get; set; } = null;

	public int SetId { get; set; } = 0;
}