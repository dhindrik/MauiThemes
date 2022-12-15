using System;
namespace MauiThemes
{
	public class Theme
	{
		public Theme(string name, string key)
		{
            Name = name;
            Key = key;
        }

		public string Name { get; set; }
		public string Key { get; set; }
	}
}

