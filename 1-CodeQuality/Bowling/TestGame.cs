using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Bowling
{
    [TestFixture]
    public class TestGame
    {
        [Test]
        public void Check_all_zero()
        {
            var game = new Game();

            foreach (var spinstepPin in Enumerable.Range(1,20))
                game.Roll(0);
                
            Assert.AreEqual(0, game.GetScore());
        }

        [Test]
        public void Check_zero_value()
        {
            var game = new Game();
            game.Roll(0);
            Assert.AreEqual(0, game.GetScore());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Chek_one_notLimit_value(int input)
        {
            var game = new Game();
            game.Roll(input);
            Assert.AreEqual(input,game.GetScore());

        }

        [TestCase(new int[] {1, 2, 3, 4}, ExpectedResult = 10)]
        [TestCase(new int[] {1,9,1,3}, ExpectedResult = 15)]
        public int Check_many_pins(int[] pins)
        {
            var game = new Game();
            for (int i = 0; i < pins.Length; i++)
            {
                game.Roll(pins[i]);
            }
            return game.GetScore();
        }
    }
}