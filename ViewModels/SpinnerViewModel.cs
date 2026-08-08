using SnazzySpinTheWheel.Models;
using SnazzySpinTheWheel.Services;
using System.Windows.Input;

namespace SnazzySpinTheWheel.ViewModels;

/// <summary>
/// Main view model for the spinner screen.
/// </summary>
public sealed class SpinnerViewModel : ViewModelBase
{
    private const int AnimationFrames = 120;

    private readonly IWheelFactory _wheelFactory;
    private readonly IWheelSpinService _wheelSpinService;
    private readonly AsyncRelayCommand _spinCommand;

    private Wheel _wheel;
    private double _wheelRotationDegrees;
    private string _resultText;
    private bool _isSpinning;

    public SpinnerViewModel(IWheelFactory wheelFactory, IWheelSpinService wheelSpinService)
    {
        _wheelFactory = wheelFactory;
        _wheelSpinService = wheelSpinService;
        _wheel = _wheelFactory.CreateDefaultWheel();

        // Align initial wheel position so slice 0 is centered under the top arrow.
        var sliceAngle = 360.0 / _wheel.Slices.Count;
        _wheelRotationDegrees = 360.0 - (sliceAngle / 2.0);

        _resultText = $"Landed on {_wheel.Slices.First().Number}";
        _spinCommand = new AsyncRelayCommand(SpinAsync, () => !IsSpinning);
    }

    /// <summary>
    /// The wheel model used by the view.
    /// </summary>
    public Wheel Wheel
    {
        get => _wheel;
        private set => SetProperty(ref _wheel, value);
    }

    /// <summary>
    /// Rotation applied to the wheel visual in degrees.
    /// </summary>
    public double WheelRotationDegrees
    {
        get => _wheelRotationDegrees;
        private set => SetProperty(ref _wheelRotationDegrees, value);
    }

    /// <summary>
    /// Message shown below the wheel after each spin.
    /// </summary>
    public string ResultText
    {
        get => _resultText;
        private set => SetProperty(ref _resultText, value);
    }

    /// <summary>
    /// Prevents the button from being clicked while the wheel is spinning.
    /// </summary>
    public bool IsSpinning
    {
        get => _isSpinning;
        private set
        {
            if (SetProperty(ref _isSpinning, value))
            {
                _spinCommand.RaiseCanExecuteChanged();
            }
        }
    }

    /// <summary>
    /// Command used by the Spin! button.
    /// </summary>
    public ICommand SpinCommand => _spinCommand;

    private async Task SpinAsync()
    {
        IsSpinning = true;

        try
        {
            var result = _wheelSpinService.CreateSpin(Wheel);
            var startRotation = WheelRotationDegrees;
            var targetRotation = result.FinalRotationDegrees;

            for (var frame = 1; frame <= AnimationFrames; frame++)
            {
                var progress = frame / (double)AnimationFrames;
                var easedProgress = 1 - Math.Pow(1 - progress, 3);
                WheelRotationDegrees = startRotation + ((targetRotation - startRotation) * easedProgress);
                await Task.Delay(16);
            }

            WheelRotationDegrees = targetRotation;
            ResultText = $"Landed on {result.SelectedSlice.Number}";
        }
        finally
        {
            IsSpinning = false;
        }
    }
}
