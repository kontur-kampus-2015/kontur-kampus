using System.Globalization;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	[TestFixture]
	public class DoubleParse_simple_should
	{
		[Test]
		public void withInvariantCulture_parse_integer()
		{
			Assert.AreEqual(123.0, double.Parse("123", CultureInfo.InvariantCulture));
		}

		[Test]
		public void withInvariantCulture_parse_scientific()
		{
			Assert.AreEqual(123.1e+1, double.Parse("123.1e+1", CultureInfo.InvariantCulture));
		}

		// ...
	}
}