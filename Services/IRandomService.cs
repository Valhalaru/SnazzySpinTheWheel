using System.Windows.Media;

namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Provides random-number generation and color palette creation for the app.
/// Registered as a singleton for consistency across the application.
/// </summary>
public interface IRandomService
{
    /// <summary>
    /// Generates a random integer between minValue (inclusive) and maxValue (exclusive).
    /// </summary>
    int Next(int minValue, int maxValue);

    /// <summary>
    /// Generates a read-only list of random RGB colors.
    /// </summary>
    /// <param name="count">The number of random colors to generate.</param>
    /// <returns>A read-only list containing the specified number of random colors with full opacity.</returns>
    IReadOnlyList<Color> GenerateRandomPalette(int count);
}
