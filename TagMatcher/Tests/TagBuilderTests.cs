using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagMatcher.Tests
{
	[TestFixture]
	public class TagBuilderTests
	{
		[Test]
		public void AddTag()
		{
			var t = new TagBuilder();
			t.Add('>').Should().BeFalse();
		}

		[Test]
		public void SimpleLiteral()
		{
			var t = new TagBuilder();
			Add("abcd").Should().Be(null);
		}

		[Test]
		public void AddCharacter()
		{
			var t = new TagBuilder();
			Add("abcd<B>").ToString().Should().Be("<B>");
		}

		[Test]
		public void PartialTag()
		{
			var t = new TagBuilder();
			Add("abcd<B<C>").ToString().Should().Be("<C>");
		}

		[Test]
		public void PartialClosingTag()
		{
			Add("abcd</B<C>").ToString().Should().Be("<C>");
			Add("abcd</B</C>").ToString().Should().Be("</C>");
		}

		[Test]
		public void StateMachineResets()
		{
			var t = new TagBuilder();
			"</B>".Any(t.Add).Should().BeTrue();
			"<C>".Any(t.Add).Should().BeTrue();
			t.Current().ToString().Should().Be("<C>");
		}

		[Test]
		public void GetTagsFromEmptyString()
		{
			TagBuilder.Tags(null).Should().BeEmpty();
			TagBuilder.Tags("").Should().BeEmpty();
		}

		[Test]
		public void TagsFromString()
		{
			ExpectTags(0, "C", "B", "/B", "/C");
			ExpectTags(1, "B", "B", "/B", "/B");
			ExpectTags(2, "B", "C", "/B", "/C");
			ExpectTags(3, "B", "/B", "/C");
			ExpectTags(4, "B", "C", "/C");
		}

		private void ExpectTags(int index, params string[] tags)
		{
			var expected = tags.Select(t => string.Concat("<", t, ">"));
			var results = TagBuilder.Tags(TestData.TestCases[index].Item1).Select(Convert.ToString);

			results.Should().BeEquivalentTo(expected);
		}

		private Tag Add(string maybeTag)
		{
			var t = new TagBuilder();
			return maybeTag.Any(t.Add) 
				? t.Current() 
				: null;
		}
	}
}