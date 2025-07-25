using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RMCL.Base.Interface;
using RMCL.Core.Views.Windows.Main.ManageWindows;
using RMCL.Core.Models.Classes;

namespace RMCL.Core.Views.Pages.Main.SettingPages;

public partial class JavaSetting : ISetting
{
    public JavaSetting()
    {
        InitializeComponent();
        UpdateUI();
    }

    public void UpdateUI()
    {
        IsEdit = false;
        ChooseDefaultJava.Items.Clear();

        ChooseDefaultJava.Items.Add(new ComboBoxItem() { Content = "[Auto] 让 RMCL 自动选择 Java", Tag = "Auto" });
        JavaManager.JavaManager.JavaRoot.Javas.ForEach(x =>
        {
            if (!string.IsNullOrEmpty(x.Version))
            {
                ChooseDefaultJava.Items.Add(new ComboBoxItem() { Content = $"[{x.Version}] {x.JavaWPath}", Tag = "" });
            }
        });
        ChooseDefaultJava.SelectedIndex = JavaManager.JavaManager.JavaRoot.SelectIndex;

        IsEdit = true;
    }

    private void ChooseDefaultJava_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (IsEdit)
        {
            if (((ComboBoxItem)ChooseDefaultJava.SelectedItem).Tag.ToString() == "Auto")
            {
                JavaManager.JavaManager.JavaRoot.SelectIndex = 0;
                JavaManager.JavaManager.JavaRoot.IsAutomaticSelection = true;
            }
            else
            {
                JavaManager.JavaManager.JavaRoot.SelectIndex = ChooseDefaultJava.SelectedIndex;
                JavaManager.JavaManager.JavaRoot.IsAutomaticSelection = false;
            }
            JavaManager.JavaManager.SaveConfig();
        }
    }

    private void SettingsExpanderItem_OnClick(object? sender, RoutedEventArgs e)
    {
        new ManagerJava().ShowDialog(Models.Classes.Core.MainWindow);
    }
}