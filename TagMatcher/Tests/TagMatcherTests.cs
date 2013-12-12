using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagMatcher.Tests
{
	[TestFixture]
    public class TagMatcherTests
    {
		[Test]
		public void EmptyStringIsCorrectlyTagged()
		{
			var tagMatcher = new TagMatcher();
			tagMatcher.Match("").Should().Be(TagMatcher.CorrectlyTagged);
		}

		[Test]
		public void StringWithNoTagsIsCorrectlyTagged()
		{
			var tagMatcher = new TagMatcher();
			tagMatcher.Match("abcd").Should().Be(TagMatcher.CorrectlyTagged);
		}

		[Test]
		public void StringWithSimpleTagsIsCorrectlyTagged()
		{
			var tagMatcher = new TagMatcher();
			tagMatcher.Match("<B>abcd</B>").Should().Be(TagMatcher.CorrectlyTagged);
		}

		[Test]
		public void OrphanClosingTag()
		{
			var tagMatcher = new TagMatcher();
			tagMatcher.Match("</B>").Should().Be("Expected # found </B>");
			tagMatcher.Match("<B>abcd</B></B>").Should().Be("Expected # found </B>");
		}

		[Test]
		public void OrphanOpeningTag()
		{
			var tagMatcher = new TagMatcher();
			tagMatcher.Match("<B>").Should().Be("Expected </B> found #");
			tagMatcher.Match("<B><B>abcd</B>").Should().Be("Expected </B> found #");
		}
	
		[TestCaseSource(typeof(TestData), "TestCases")]
		public void TestSampleData(Tuple<string, string> testCase)
		{
			var tagMatcher = new TagMatcher();
			tagMatcher.Match(testCase.Item1).Should().Be(testCase.Item2);
		}
    }
}
