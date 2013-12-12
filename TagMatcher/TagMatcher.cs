using System.Collections.Generic;

namespace TagMatcher
{
	public class TagMatcher
	{
		public const string CorrectlyTagged = "Correctly tagged paragraph";

		public string Match(string value)
		{
			var tags = new Stack<Tag>();
			foreach (var tag in TagBuilder.Tags(value))
			{
				if (!tag.IsClosingTag)
				{
					tags.Push(tag);
					continue;
				}

				if (tags.Count > 0)
				{
					var match = tags.Pop();
					if (tag.IsMatch(match))
					{
						continue;
					}

					return string.Format("Expected {0} found {1}", match.AsClosingTag(), tag);
				}

				return string.Format("Expected # found {0}", tag);
			}

			if (tags.Count > 0)
			{
				return string.Format("Expected {0} found #", tags.Pop().AsClosingTag());
			}

			return CorrectlyTagged;
		}
	}
}