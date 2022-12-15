using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiThemes
{
	public class ThemeAddedMessage : ValueChangedMessage<string>
	{
		public ThemeAddedMessage(string value) : base(value)
		{
		}
	}
}

