using SnazzySpinTheWheel.Models;

namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Chooses a random slice and computes the final clockwise rotation.
/// </summary>
public sealed class WheelSpinService : IWheelSpinService
{

    private const int SliceCount = 20;
    private const double SliceAngle = 360.0 / SliceCount;
    private const int MinRevolutions = 5;
    private const int MaxRevolutionsExclusive = 21;

    private readonly IRandomService _randomService;

    public WheelSpinService(IRandomService randomService)
    {
        _randomService = randomService;
    }

    private static double Normalize(double a) => (a % 360 + 360) % 360;

    public WheelSpinResult CreateSpin(Wheel wheel, double currentRotationDegrees = 0)
    {
        int count = wheel.Slices.Count;
        double sliceAngle = 360.0 / count;
        const double pointerAngle = 0.0; // top arrow in WheelControl convention

        int targetIndex = _randomService.Next(0, count);

        // center angle of target slice in wheel-local coordinates
        double targetCenter = targetIndex * sliceAngle + sliceAngle / 2.0;

        // wheel angle needed so target center is under top pointer
        double desiredRotationNorm = Normalize(pointerAngle - targetCenter);

        double currentNorm = Normalize(currentRotationDegrees);
        double delta = Normalize(desiredRotationNorm - currentNorm);

        double fullSpins = _randomService.Next(4, 7) * 360.0;
        double finalRotation = currentRotationDegrees + fullSpins + delta;

        // Use originally selected target index to avoid boundary/offset drift
        return new WheelSpinResult(wheel.Slices[targetIndex], finalRotation, 5);
    }
}
