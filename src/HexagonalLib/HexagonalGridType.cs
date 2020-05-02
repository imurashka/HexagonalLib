namespace HexagonalLib
{
    /// <summary>
    /// The typical layouts and orientations for hex grids
    /// </summary>
    public enum HexagonalGridType : byte
    {
        /// <summary>
        /// Horizontal layout shoves odd rows right [odd-r]
        /// </summary>
        PointyOdd,

        /// <summary>
        /// Horizontal layout shoves even rows right [even-r]
        /// </summary>
        PointyEven,

        /// <summary>
        /// Vertical layout shoves odd columns down [odd-q]
        /// </summary>
        FlatOdd,

        /// <summary>
        /// Vertical layout shoves even columns down [even-q]
        /// </summary>
        FlatEven,
    }
}