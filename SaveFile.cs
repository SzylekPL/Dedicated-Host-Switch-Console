namespace DedicatedHostSwitch
{
	internal static class SaveFile
	{
		private static readonly FileInfo SaveFileInfo = new("save.csv");
		internal static bool Empty { get => SaveFileInfo.Length == 0; }
		internal static string Content
		{
			get => File.ReadAllText(SaveFileInfo.FullName);
			set => File.WriteAllText(SaveFileInfo.FullName, value);
		}
		static SaveFile()
		{
			if (!SaveFileInfo.Exists)
				SaveFileInfo.Create();
		}
	}
}
