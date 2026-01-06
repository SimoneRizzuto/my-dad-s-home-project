using System.Collections.Generic;

namespace MyFathersHomeProject.SaveFiles;

public class SaveData
{
	public int TestIndex { get; set; } = 0;
	public string TestUid { get; set; } = "";
	public List<string> TestNames { get; set; } = new();

	public class MyClass
	{
		public int TestInt2 { get; set; } = 100;
		private int TestInt3 { get; set; } = 200;
		public List<string> TestNames2 { get; set; } = new();
	}

	public MyClass TestMtClass { get; set; } = new();
}