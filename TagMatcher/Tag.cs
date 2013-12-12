namespace TagMatcher
{
	public class Tag
	{
		private readonly string _name;
		private readonly bool _isClosingTag;

		public Tag(string name, bool isClosingTag)
		{
			_name = name;
			_isClosingTag = isClosingTag;
		}

		public string Name
		{
			get { return _name; }
		}

		public bool IsClosingTag
		{
			get { return _isClosingTag; }
		}

		public bool IsMatch(Tag pair)
		{
			return Name == pair.Name && IsClosingTag != pair.IsClosingTag;
		}

		public override string ToString()
		{
			var inner = IsClosingTag 
				? ("/" + Name) 
				: Name;

			return string.Concat("<", inner, ">");
		}

		public Tag AsClosingTag()
		{
			return new Tag(Name, isClosingTag: true);
		}
	}
}