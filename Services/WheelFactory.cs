using System.Windows.Media;
using SnazzySpinTheWheel.Models;

namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Builds the default wheel with 20 uniquely colored slices.
/// </summary>
public sealed class WheelFactory : IWheelFactory
{
    private readonly IReadOnlyList<Color> _palette;

    public WheelFactory()
    {
        _palette = new List<Color>
        {
            Colors.Red,
            Colors.OrangeRed,
            Colors.Orange,
            Colors.Gold,
            Colors.Yellow,
            Colors.YellowGreen,
            Colors.Green,
            Colors.MediumSeaGreen,
            Colors.Teal,
            Colors.DeepSkyBlue,
            Colors.SkyBlue,
            Colors.RoyalBlue,
            Colors.MediumBlue,
            Colors.Indigo,
            Colors.Purple,
            Colors.MediumVioletRed,
            Colors.HotPink,
            Colors.Crimson,
            Colors.Chocolate,
            Colors.DarkCyan
        };
    }

    public Wheel CreateDefaultWheel()
    {
        var slices = Enumerable.Range(1, 20)
            .Select(index => new PieSlice(index, _palette[index - 1]))
            .ToList();

        return new Wheel(slices);
    }
}
