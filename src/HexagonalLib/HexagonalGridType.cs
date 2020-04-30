namespace HexagonalLib
{
    /// <summary>
    /// The typical layouts and orientations for hex grids
    /// </summary>
    public enum HexagonalGridType : byte
    {
        /// <summary>
        /// Vertical layout shoves odd columns down
        /// </summary>
        FlatOdd,

        /// <summary>
        /// Vertical layout shoves even columns down
        /// </summary>
        FlatEven,

        /// <summary>
        /// Horizontal layout shoves odd rows right
        /// </summary>
        PointyOdd,

        /// <summary>
        /// Horizontal layout shoves even rows right
        /// </summary>
        PointyEven,
    }
}
