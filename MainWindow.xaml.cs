using System.Windows;
using SnazzySpinTheWheel.ViewModels;

namespace SnazzySpinTheWheel;

public partial class MainWindow : Window
{
    public MainWindow(SpinnerViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
