﻿using KeepKeeper.Framework;

namespace KeepKeeper.Common
{
	public class Picture : Value<Picture>
	{
		public string Description { get; }
		public string Url { get; }

		public Picture(string url, string description)
		{
			Url = url;
			Description = description;
		}
	}
}
