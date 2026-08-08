using SnazzySpinTheWheel.Models;

namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Produces a complete spin plan for the wheel.
/// </summary>
public interface IWheelSpinService
{
    WheelSpinResult CreateSpin(Wheel wheel, double currentRotationDegrees = 0);
}
