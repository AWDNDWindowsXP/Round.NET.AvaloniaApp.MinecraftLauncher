using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using RMCL.Controls.ControlHelper;

namespace RMCL.Core.Views.Windows.Main.ManageWindows;

public partial class ManagerJava : Window
{
    public bool IsEdit { get; set; } = false;
    public ManagerJava()
    {
        InitializeComponent();
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        var brush = DisplayPath.Fill;
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        var btn = (Button)sender;
        btn.Content = WindowState == WindowState.Maximized
            ? new PathIcon()
            {
                Width = 12,
                Height = 12,
                Margin = new Thickness(0, 3, 0, 0),
                Foreground = brush,
                Data = StreamGeometry.Parse(
                    "M832 704H704v128c0 70.692-57.308 128-128 128H192c-70.692 0-128-57.308-128-128V448c0-70.692 57.308-128 128-128h128V192c0-70.692 57.308-128 128-128h384c70.692 0 128 57.308 128 128v384c0 70.692-57.308 128-128 128zM192 384c-35.346 0-64 28.654-64 64v384c0 35.346 28.654 64 64 64h384c35.346 0 64-28.654 64-64V448c0-35.346-28.654-64-64-64H192z m704-192c0-35.346-28.654-64-64-64H448c-35.346 0-64 28.654-64 64v128h192c70.692 0 128 57.308 128 128v192h128c35.346 0 64-28.654 64-64V192z")
            }
            : new PathIcon()
            {
                Margin = new Thickness(0, 1, 0, 0),
                Foreground = brush,
                Width = 12,
                Height = 12,
                Data = StreamGeometry.Parse(
                    "M233.301333 128A105.301333 105.301333 0 0 0 128 233.301333v557.397334A105.301333 105.301333 0 0 0 233.301333 896h557.397334A105.301333 105.301333 0 0 0 896 790.698667V233.301333A105.301333 105.301333 0 0 0 790.698667 128H233.301333z m-18.602666 105.301333c0-10.24 8.32-18.602667 18.602666-18.602666h557.397334c10.24 0 18.602667 8.32 18.602666 18.602666v557.397334c0 10.24-8.32 18.602667-18.602666 18.602666H233.301333a18.56 18.56 0 0 1-18.602666-18.602666V233.301333z")
            };
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }

    public void UpdateUI()
    {
        IsEdit = false;
        
        JavaManager.JavaManager.JavaRoot.Javas.ForEach(x =>
        {
            
        });
        
        IsEdit = true;
    }
}