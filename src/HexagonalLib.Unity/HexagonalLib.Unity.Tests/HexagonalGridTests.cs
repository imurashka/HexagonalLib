using HexagonalLib.Coordinates;
using NUnit.Framework;

namespace HexagonalLib.Tests
{
    [TestFixture]
    public partial class HexagonalGridTests
    {
        [Test(Author = "Ivan Murashka", Description = "Check coordinate conversion from Offset to Unity vectors")]
        public void UnityCoordinateConversionTest(
            [Values] HexagonalGridType type,
            [Values(-13, -8, 0, 15, 22)] int offsetX,
            [Values(-13, -8, 0, 15, 22)] int offsetY)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            var offset = new Offset(offsetX, offsetY);
            var vector2 = grid.ToVector2(offset);
            var vector3 = grid.ToVector3(offset);
            Assert.AreEqual(offset, grid.ToOffset(vector2));
            Assert.AreEqual(offset, grid.ToOffset(vector3));
        }
    }
}