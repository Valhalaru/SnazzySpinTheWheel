using SnazzySpinTheWheel.Models;

namespace SnazzySpinTheWheel.Services;

/// <summary>
/// Creates a fully configured wheel instance.
/// </summary>
public interface IWheelFactory
{
    Wheel CreateDefaultWheel();
}
