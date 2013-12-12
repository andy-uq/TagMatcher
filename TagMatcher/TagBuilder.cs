using System;
using System.Collections.Generic;

namespace TagMatcher
{
	public class TagBuilder
	{
		private bool _isClosingTag;
		private State _state;
		private string _tagName;

		public static IEnumerable<Tag> Tags(string value)
		{
			if (string.IsNullOrEmpty(value))
				yield break;

			var tagBuilder = new TagBuilder();
			foreach (char c in value)
			{
				if (tagBuilder.Add(c))
				{
					yield return tagBuilder.Current();
				}
			}
		}

		public bool Add(char c)
		{
			switch (_state)
			{
				case State.Text:
					Text(c);
					break;

				case State.InTag:
					if (InTag(c))
						break;
					goto case State.Text;

				case State.ExpectEndOfTag:
					if (EndOfTag(c))
						return true;
					goto case State.Text;

				case State.Finished:
					goto case State.Text;
			}

			return false;
		}

		public Tag Current()
		{
			return new Tag(_tagName, _isClosingTag);
		}

		private bool EndOfTag(char c)
		{
			if (c == '>')
			{
				_state = State.Finished;
				return true;
			}

			return false;
		}

		private bool InTag(char c)
		{
			if (c == '/')
			{
				_isClosingTag = true;
				return true;
			}
			
			if (char.IsUpper(c))
			{
				_tagName = Convert.ToString(c);
				_state = State.ExpectEndOfTag;

				return true;
			}

			return false;
		}

		private void Text(char nextChar)
		{
			_isClosingTag = false;
			_state = nextChar == '<'
				? State.InTag
				: State.Text;
		}

		private enum State
		{
			Text,
			InTag,
			ExpectEndOfTag,
			Finished
		}
	}
}