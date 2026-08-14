using System.Windows.Media;

namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Default random-number source and color palette generator for the app.
/// Registered as a singleton to ensure random generation across all operations.
/// Uses Random.Shared for thread-safe randomization.
/// </summary>
public sealed class RandomService : IRandomService
{
    private readonly Random _random = Random.Shared;

    /// <summary>
    /// Generates a random integer between minValue (inclusive) and maxValue (exclusive).
    /// </summary>
    public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);

    /// <summary>
    /// Generates a list of random fully-opaque RGB colors.
    /// </summary>
    /// <param name="count">The number of random colors to generate.</param>
    /// <returns>A read-only list containing the specified number of randomly generated colors.</returns>
    public IReadOnlyList<Color> GenerateRandomPalette(int count)
    {
        var palette = new List<Color>();

        for (int i = 0; i < count; i++)
        {
            var color = Color.FromArgb(
                255, // Alpha (opaque)
                (byte)_random.Next(0, 256), // Red
                (byte)_random.Next(0, 256), // Green
                (byte)_random.Next(0, 256)  // Blue
            );
            palette.Add(color);
        }

        return palette.AsReadOnly();
    }
}
