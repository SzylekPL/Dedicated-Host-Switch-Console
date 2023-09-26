class Program
{
	static class SaveFile
	{
		private static readonly FileInfo SaveFileInfo = new(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "save.csv"));
		private static string? LuaFilePath;
		public static string[]? LuaContent;
		static SaveFile()
		{
			if (!SaveFileInfo.Exists)
				SaveFileInfo.Create();
			if (SaveFileInfo.Length == 0)
			{
				while (!File.Exists(LuaFilePath))
				{
					Console.WriteLine("Save file is empty or Lua file doesn't exist, insert Lua file path.");
					LuaFilePath = Console.ReadLine();
				}
				File.WriteAllText(SaveFileInfo.FullName, LuaFilePath);
			}
			LuaFilePath = File.ReadAllText(SaveFileInfo.FullName);
			LuaContent = File.ReadAllLines(LuaFilePath);		}
		public static void LuaSave() => File.WriteAllLines(LuaFilePath, LuaContent);
		public static bool Save(string? path)
		{
			if (!File.Exists(path))
				return false;
			else
			{
				LuaFilePath = path;
				File.WriteAllText(SaveFileInfo.FullName, LuaFilePath);
				return true;
			}
		}
	}
	static void Main()
	{
		string[] Line = { @"	DedicatedHost = false,", @"	DedicatedHost = true,", "Dedicated host has been set to \"false\".", "Dedicated host has been set to \"true\"." };
		byte MainLoop = 0;
		bool LoadedState = SaveFile.LuaContent[11] == Line[1];
		Console.WriteLine("Dedicated Host Switch\n1.True\n2.False\n3.Toggle\n4.Change Lua file path\n5.Exit");
		while (MainLoop < 5)
		{
			while (!byte.TryParse(Console.ReadLine(), out MainLoop))
				Console.WriteLine("Incorrect value, type again:");

			switch (MainLoop)
			{
				case 1:
					SaveFile.LuaContent[11] = Line[1];
					SaveFile.LuaSave();
					Console.WriteLine(Line[3]);
					break;
				case 2:
					SaveFile.LuaContent[11] = Line[0];
					SaveFile.LuaSave();
					Console.WriteLine(Line[2]);
					break;
				case 3:
					LoadedState = !LoadedState;
					if (LoadedState)
					{
						SaveFile.LuaContent[11] = Line[1];
						Console.WriteLine(Line[3]);
					}
					else
					{
						SaveFile.LuaContent[11] = Line[0];
						Console.WriteLine(Line[2]);
					}
					SaveFile.LuaSave();
					break;
				case 4:
					Console.WriteLine("Insert new path:");
					while (!SaveFile.Save(Console.ReadLine()))
						Console.WriteLine("File with this path doesn't exist, try again:");
					Console.WriteLine("Lua file path has been updated.");
					break;
			}
		}
	}
}