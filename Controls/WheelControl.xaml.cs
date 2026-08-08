using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SnazzySpinTheWheel.Models;

namespace SnazzySpinTheWheel.Controls;

/// <summary>
/// Renders the spinner wheel as 20 colored pie slices.
/// </summary>
public partial class WheelControl : UserControl
{
    public static readonly DependencyProperty WheelProperty = DependencyProperty.Register(
        nameof(Wheel),
        typeof(Wheel),
        typeof(WheelControl),
        new PropertyMetadata(null, OnVisualPropertyChanged));

    public static readonly DependencyProperty RotationDegreesProperty = DependencyProperty.Register(
        nameof(RotationDegrees),
        typeof(double),
        typeof(WheelControl),
        new PropertyMetadata(0d, OnVisualPropertyChanged));

    public WheelControl()
    {
        InitializeComponent();
        Loaded += (_, _) => Redraw();
        SizeChanged += (_, _) => Redraw();
    }

    /// <summary>
    /// Wheel data to draw.
    /// </summary>
    public Wheel? Wheel
    {
        get => (Wheel?)GetValue(WheelProperty);
        set => SetValue(WheelProperty, value);
    }

    /// <summary>
    /// Clockwise rotation applied to the whole wheel.
    /// </summary>
    public double RotationDegrees
    {
        get => (double)GetValue(RotationDegreesProperty);
        set => SetValue(RotationDegreesProperty, value);
    }

    private static void OnVisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((WheelControl)d).Redraw();
    }

    private void Redraw()
    {
        if (PART_Canvas == null || Wheel is null || ActualWidth <= 0 || ActualHeight <= 0)
        {
            return;
        }

        PART_Canvas.Children.Clear();
        PART_Canvas.RenderTransform = new RotateTransform(RotationDegrees, ActualWidth / 2, ActualHeight / 2);

        var centerX = ActualWidth / 2;
        var centerY = ActualHeight / 2;
        var radius = Math.Min(ActualWidth, ActualHeight) / 2 - 18;
        var sliceAngle = 360.0 / Wheel.Slices.Count;

        for (var i = 0; i < Wheel.Slices.Count; i++)
        {
            var slice = Wheel.Slices[i];
            var startAngle = i * sliceAngle;
            var endAngle = startAngle + sliceAngle;

            PART_Canvas.Children.Add(CreateSlicePath(centerX, centerY, radius, startAngle, endAngle, slice.Color));
            PART_Canvas.Children.Add(CreateSliceLabel(centerX, centerY, radius, startAngle, slice.DisplayText));
        }

        var border = new Ellipse
        {
            Width = radius * 2,
            Height = radius * 2,
            Stroke = Brushes.White,
            StrokeThickness = 4,
            Fill = Brushes.Transparent
        };
        Canvas.SetLeft(border, centerX - radius);
        Canvas.SetTop(border, centerY - radius);
        PART_Canvas.Children.Add(border);
    }

    private static Path CreateSlicePath(double centerX, double centerY, double radius, double startAngle, double endAngle, Color color)
    {
        var figure = new PathFigure
        {
            StartPoint = new Point(centerX, centerY),
            IsClosed = true
        };

        figure.Segments.Add(new LineSegment(GetPoint(centerX, centerY, radius, startAngle), true));
        figure.Segments.Add(new ArcSegment
        {
            Point = GetPoint(centerX, centerY, radius, endAngle),
            Size = new Size(radius, radius),
            SweepDirection = SweepDirection.Clockwise,
            IsLargeArc = false
        });
        figure.Segments.Add(new LineSegment(new Point(centerX, centerY), true));

        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);

        return new Path
        {
            Data = geometry,
            Fill = new SolidColorBrush(color),
            Stroke = Brushes.White,
            StrokeThickness = 1
        };
    }

    private static TextBlock CreateSliceLabel(double centerX, double centerY, double radius, double startAngle, string text)
    {
        var angle = startAngle + 9;
        var point = GetPoint(centerX, centerY, radius * 0.58, angle);

        var label = new TextBlock
        {
            Text = text,
            FontSize = 22,
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.White,
            TextAlignment = TextAlignment.Center,
            Width = 40
        };
        Canvas.SetLeft(label, point.X - 20);
        Canvas.SetTop(label, point.Y - 14);
        return label;
    }

    private static Point GetPoint(double centerX, double centerY, double radius, double angleDegrees)
    {
        var radians = Math.PI / 180.0 * angleDegrees;
        return new Point(
            centerX + (radius * Math.Sin(radians)),
            centerY - (radius * Math.Cos(radians)));
    }
}
