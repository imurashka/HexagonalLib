using HexagonalLib;
using NUnit.Framework;

namespace HexagonalLib.Tests
{
    [TestFixture(TestOf = typeof(HexagonalMath))]
    public class HexagonalMathTests
    {
        [Test(Author = "Ivan Murashka", Description = "Check HexagonalMath.Rotate method")]
        [TestCase(0, 1, 0, 0, 1)]
        public void RotateTest(float x, float y, float degrees, float resultX, float resultY)
        {
            (float X, float Y) vector = (x, y);
            (float X, float Y) result = (resultX, resultY);
            Assert.AreEqual(result, vector.Rotate(degrees));
        }
    }
}