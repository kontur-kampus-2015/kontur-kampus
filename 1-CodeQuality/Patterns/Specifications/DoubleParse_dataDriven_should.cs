using System.Globalization;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	[TestFixture]
	public class DoubleParse_should
	{
		[TestCase("123", Result = 123)]
		[TestCase("0", Result = 0)]
		[TestCase("-1", Result = -1)]
		public double parse_integer(string input)
		{
			return double.Parse(input, CultureInfo.InvariantCulture);
		}

        [TestCase("1.1", Result = 1.1)]
        [TestCase("-1.1", Result = -1.1)]
        [TestCase("0.1", Result = 0.1)]
        [TestCase("-0.123", Result = -0.123)]
        public double parse_simpleFraction(string input)
		{
			return double.Parse(input, CultureInfo.InvariantCulture);
		}

        [TestCase("1.1e1", Result = 1.1e1)]
        [TestCase("1.1e-1", Result = 1.1e-1)]
        [TestCase("1.1e+1", Result = 1.1e1)]
        public double parse_scientificForm(string input)
		{
			return double.Parse(input, CultureInfo.InvariantCulture);
		}
	}
}