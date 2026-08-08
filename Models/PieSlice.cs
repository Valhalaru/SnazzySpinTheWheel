using System.Windows.Media;

namespace SnazzySpinTheWheel.Models;

/// <summary>
/// Represents one numbered slice on the wheel.
/// </summary>
public sealed class PieSlice
{
    public PieSlice(int number, Color color)
    {
        if (number < 1 || number > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "The slice number must be between 1 and 20.");
        }

        Number = number;
        Color = color;
    }

    /// <summary>
    /// The displayed slice number.
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// The unique color used to draw the slice.
    /// </summary>
    public Color Color { get; }

    /// <summary>
    /// The text shown inside the slice.
    /// </summary>
    public string DisplayText => Number.ToString();
}
