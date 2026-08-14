using System.Windows.Media;
using SnazzySpinTheWheel.Models;

namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Factory for creating wheel instances with randomly colored slices.
/// Uses IRandomService to generate a unique color palette for each wheel.
/// </summary>
public sealed class WheelFactory : IWheelFactory
{
    private readonly IReadOnlyList<Color> _palette;
    private int _numSlices;

    /// <summary>
    /// Initializes a new WheelFactory with a specified number of slices.
    /// </summary>
    /// <param name="numSlices">The number of slices for the wheel.</param>
    /// <param name="randomService">Service used to generate random colors for the palette.</param>
    public WheelFactory(int numSlices, IRandomService randomService)
    {
        _numSlices = numSlices;
        _palette = randomService.GenerateRandomPalette(numSlices);
    }

    /// <summary>
    /// Creates a wheel with the specified number of randomly colored slices.
    /// </summary>
    /// <returns>A new Wheel instance containing the configured number of slices with random colors.</returns>
    public Wheel CreateDefaultWheel()
    {
        var slices = Enumerable.Range(1, _numSlices)
            .Select(index => new PieSlice(index, _palette[index - 1]))
            .ToList();

        return new Wheel(slices);
    }
}
