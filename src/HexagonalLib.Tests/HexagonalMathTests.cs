using NUnit.Framework;

namespace HexagonalLib.Tests
{
    [TestFixture(TestOf = typeof(HexagonalMath))]
    public class HexagonalMathTests
    {
        [Test(Author = "Ivan Murashka", Description = "Check HexagonalMath.Rotate method")]
        [TestCase(0, 1, 0, 0, 1)]
        [TestCase(0, 1, 45, 0.5f, 0.5f)]
        [TestCase(0, 1, 90, 1, 0)]
        [TestCase(0, 1, 135, 0.5f, -0.5f)]
        [TestCase(0, 1, 180, 0, -1)]
        [TestCase(0, 1, 225, -0.5f, -0.5f)]
        [TestCase(0, 1, 270, -1, 0)]
        [TestCase(0, 1, 315, -0.5f, 0.5f)]
        [TestCase(0, 1, 360, 0, 1)]
        public void RotateTest(float x, float y, float degrees, float resultX, float resultY)
        {
            var vector = (x, y).Rotate(degrees);
            var result = (resultX, resultY).Normalize();
            Assert.IsTrue(vector.X.Approximately(result.X) && vector.Y.Approximately(result.Y), $"Expected: {result.X}:{result.Y}; Actual: {vector.X}:{vector.Y}");
        }
    }
}