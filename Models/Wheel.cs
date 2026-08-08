using System.Collections.ObjectModel;

namespace SnazzySpinTheWheel.Models;

/// <summary>
/// Represents the full spinner wheel and its current rotation state.
/// </summary>
public sealed class Wheel
{
    /// <summary>
    /// The slices that make up the wheel.
    /// </summary>
    public ReadOnlyCollection<PieSlice> Slices { get; }

    /// <summary>
    /// The current clockwise rotation in degrees.
    /// </summary>
    public double RotationDegrees { get; set; }

    public Wheel(IEnumerable<PieSlice> slices)
    {
        var sliceList = slices?.ToList() ?? throw new ArgumentNullException(nameof(slices));

        if (sliceList.Count != 20)
        {
            throw new ArgumentException("The wheel must contain exactly 20 slices.", nameof(slices));
        }

        Slices = new ReadOnlyCollection<PieSlice>(sliceList);
    }

}
