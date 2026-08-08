namespace SnazzySpinTheWheel.Models;

/// <summary>
/// Represents the outcome of a wheel spin.
/// </summary>
public sealed record WheelSpinResult(PieSlice SelectedSlice, double FinalRotationDegrees, int Revolutions);
