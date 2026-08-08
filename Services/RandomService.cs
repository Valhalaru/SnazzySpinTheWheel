namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Default random-number source used by the app.
/// Registered as a singleton so every spin uses the same shared generator.
/// </summary>
public sealed class RandomService : IRandomService
{
    private readonly Random _random = Random.Shared;

    public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);
}
