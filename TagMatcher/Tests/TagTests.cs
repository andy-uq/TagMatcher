using FluentAssertions;
using NUnit.Framework;

namespace TagMatcher.Tests
{
	[TestFixture]
	public class TagTests
	{
		[Test]
		public void Tag()
		{
			var tag = new Tag("B", isClosingTag: false);
			tag.Name.Should().Be("B");
			tag.IsClosingTag.Should().BeFalse();
			tag.ToString().Should().Be("<B>");
		}

		[Test]
		public void ClosingTag()
		{
			var tag = new Tag("B", isClosingTag: true);
			tag.Name.Should().Be("B");
			tag.IsClosingTag.Should().BeTrue();
			tag.ToString().Should().Be("</B>");
		}

		[Test]
		public void TagDoesNotMatch()
		{
			var open = new Tag("B", isClosingTag: false);
			var close = new Tag("C", isClosingTag: true);

			open.IsMatch(close).Should().BeFalse();
			close.IsMatch(open).Should().BeFalse();
		}

		[Test]
		public void TagDoesNotMatchSelf()
		{
			var open = new Tag("B", isClosingTag: false);
			open.IsMatch(open).Should().BeFalse();
		}

		[Test]
		public void TagIsMatched()
		{
			var open = new Tag("B", isClosingTag: false);
			var close = new Tag("B", isClosingTag: true);

			open.IsMatch(close).Should().BeTrue();
			close.IsMatch(open).Should().BeTrue();
		}
	}
}