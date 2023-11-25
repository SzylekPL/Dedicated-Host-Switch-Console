namespace DedicatedHostSwitch
{
	class Program
	{
		enum UserInput { True = 1, False, Toggle, ChangePath, Exit }
		static void Main()
		{
			UserInput userInput = new();
			string[] Line = { @"	DedicatedHost = false,", @"	DedicatedHost = true,", "Dedicated host has been set to \"false\".", "Dedicated host has been set to \"true\"." }, LuaFileStructure;
			string userString;

			if (SaveFile.Empty)
			{
				Console.WriteLine("Lua file path not configured, insert it:");
				while (!File.Exists(userString = Console.ReadLine()))
					Console.WriteLine("File with this path doesn't exist, try again:");
				Console.WriteLine("Lua file path has been updated.");
			}
			LuaFile.Initialize();

			Console.WriteLine("Dedicated Host Switch\n1.True\n2.False\n3.Toggle\n4.Change Lua file path\n5.Exit");
			while (userInput != UserInput.Exit)
			{
				while (!Enum.TryParse(Console.ReadLine(), out userInput))
					Console.WriteLine("Incorrect value, type again:");

				switch (userInput)
				{
					case UserInput.True:
						LuaFile.DedicatedHost = true;
						LuaFile.Save();
						Console.WriteLine(Line[3]);
						break;
					case UserInput.False:
						LuaFile.DedicatedHost = false;
						LuaFile.Save();
						Console.WriteLine(Line[2]);
						break;
					case UserInput.Toggle:
						if (LuaFile.DedicatedHost)
						{
							LuaFile.DedicatedHost = false;
							Console.WriteLine(Line[3]);
						}
						else
						{
							LuaFile.DedicatedHost = true;
							Console.WriteLine(Line[2]);
						}
						LuaFile.Save();
						break;
					case UserInput.ChangePath:
						Console.WriteLine("Insert new path:");
						while (!File.Exists(userString = Console.ReadLine()))
							Console.WriteLine("File with this path doesn't exist, try again:");
						SaveFile.Content = userString;
						Console.WriteLine("Lua file path has been updated.");
						break;
				}
			}
		}
	}
}