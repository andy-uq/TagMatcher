using System;
using System.Collections.Generic;

namespace TagMatcher.Tests
{
	public static class TestData
	{
		public static readonly List<Tuple<string, string>> TestCases = new List<Tuple<string, string>>
		{
			new Tuple<string, string>("The following text<C><B>is centred and in boldface</B></C>", TagMatcher.CorrectlyTagged),
			new Tuple<string, string>(@"<B>This <\g>is <B>boldface</B> in <<*> a</B> <\6> <<d>sentence", TagMatcher.CorrectlyTagged),
			new Tuple<string, string>("<B><C> This should be centred and in boldface, but the tags are wrongly nested </B></C>", "Expected </C> found </B>"),
			new Tuple<string, string>("<B>This should be in boldface, but there is an extra closing tag</B></C>", "Expected # found </C>"),
			new Tuple<string, string>("<B><C>This should be centred and in boldface, but there is a missing closing tag</C>", "Expected </B> found #"),
		};		
	}
}