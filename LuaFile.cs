﻿namespace DedicatedHostSwitch
{
	internal static class LuaFile
	{
		static string[] Content { get; set; }
		internal static bool DedicatedHost
		{
			get => Content[11] == @"	DedicatedHost = true,";
			set
			{
				if (value)
					Content[11] = @"	DedicatedHost = true,";
				else
					Content[11] = @"	DedicatedHost = false,";
			}
		}
		static string? Path { get; set; }
		internal static void Initialize()
		{
			Path = SaveFile.Content;
			Content = File.ReadAllLines(Path);
		}
		internal static void Save() => File.WriteAllLines(Path, Content);
	}
}