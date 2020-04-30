using HexagonalLib.Coordinates;
using HexagonalLib.Utility;
using UnityEngine;

namespace HexagonalLib
{
    public partial struct HexagonalGrid
    {
        #region ToOffset

        /// <summary>
        /// Convert point to offset coordinate
        /// </summary>
        public Offset ToOffset(Vector2 vector)
        {
            return ToOffset(vector.AsTuple());
        }

        /// <summary>
        /// Convert point to offset coordinate
        /// </summary>
        public Offset ToOffset(Vector3 vector)
        {
            return ToOffset(vector.AsTuple());
        }

        #endregion

        #region ToAxial

        /// <summary>
        /// Convert point to axial coordinate
        /// </summary>
        public Axial ToAxial(Vector2 vector)
        {
            return ToAxial(vector.AsTuple());
        }

        /// <summary>
        /// Convert point to axial coordinate
        /// </summary>
        public Axial ToAxial(Vector3 vector)
        {
            return ToAxial(vector.AsTuple());
        }

        #endregion

        #region ToCubic

        /// <summary>
        /// Convert point to cubic coordinate
        /// </summary>
        public Cubic ToCubic(Vector2 vector)
        {
            return ToCubic(vector.AsTuple());
        }

        /// <summary>
        /// Convert point to cubic coordinate
        /// </summary>
        public Cubic ToCubic(Vector3 vector)
        {
            return ToCubic(vector.AsTuple());
        }

        #endregion

        #region ToVector

        /// <summary>
        /// Convert hex based on its offset coordinate to it center position in 2d space
        /// </summary>
        public Vector2 ToVector2(Offset coord)
        {
            return ToPoint2(coord).AsVector2();
        }

        /// <summary>
        /// Convert hex based on its axial coordinate to it center position in 2d space
        /// </summary>
        public Vector2 ToVector2(Axial coord)
        {
            return ToPoint2(coord).AsVector2();
        }

        /// <summary>
        /// Convert hex based on its cubic coordinate to it center position in 2d space
        /// </summary>
        public Vector2 ToVector2(Cubic coord)
        {
            return ToPoint2(coord).AsVector2();
        }

        /// <summary>
        /// Convert hex based on its offset coordinate to it center position in 3d space OZ
        /// </summary>
        public Vector3 ToVector3(Offset coord, float y = 0)
        {
            return ToPoint2(coord).AsVector3(y);
        }

        /// <summary>
        /// Convert hex based on its axial coordinate to it center position in 3d space OZ
        /// </summary>
        public Vector3 ToVector3(Axial coord, float y = 0)
        {
            return ToPoint2(coord).AsVector3(y);
        }

        /// <summary>
        /// Convert hex based on its cubic coordinate to it center position in 3d space OZ
        /// </summary>
        public Vector3 ToVector3(Cubic coord, float y = 0)
        {
            return ToPoint2(coord).AsVector3(y);
        }

        #endregion
    }
}