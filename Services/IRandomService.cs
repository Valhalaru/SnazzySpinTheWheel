namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Wraps random-number generation so the rest of the app can be tested and reused easily.
/// </summary>
public interface IRandomService
{
    int Next(int minValue, int maxValue);
}
