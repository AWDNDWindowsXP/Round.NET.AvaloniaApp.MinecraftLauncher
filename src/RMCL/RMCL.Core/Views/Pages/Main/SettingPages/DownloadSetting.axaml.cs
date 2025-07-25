﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RMCL.Base.Enum.Update;
using RMCL.Base.Interface;
using RMCL.Core.Models.Classes;

namespace RMCL.Core.Views.Pages.Main.SettingPages;

public partial class DownloadSetting : ISetting
{
    public DownloadSetting()
    {
        InitializeComponent();

        ChooseDownloadSource.SelectedIndex = Config.Config.MainConfig.UpdateModel.Proxy.GetHashCode();
        ChoosePublishSource.SelectedIndex = Config.Config.MainConfig.UpdateModel.Branch.GetHashCode();
        ChooseUpdateAPISource.SelectedIndex = Config.Config.MainConfig.UpdateModel.Route.GetHashCode();
        AutomaticUpdates.IsChecked = Config.Config.MainConfig.UpdateModel.IsAutoDetectUpdates;

        IsEdit = true;
    }

    private void ChoosePublishSource_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (IsEdit)
        {
            Config.Config.MainConfig.UpdateModel.Branch = (UpdateBranch)ChoosePublishSource.SelectedIndex;
            Config.Config.SaveConfig();
        }
    }

    private void ChooseDownloadSource_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (IsEdit)
        {
            Config.Config.MainConfig.UpdateModel.Proxy = (UpdateProxy)ChooseDownloadSource.SelectedIndex;
            Config.Config.SaveConfig();
        }
    }

    private void ChooseUpdateAPISource_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {

        if (IsEdit)
        {
            Config.Config.MainConfig.UpdateModel.Route = (UpdateRoute)ChooseUpdateAPISource.SelectedIndex;
            Config.Config.SaveConfig();
        }
    }

    private void AutomaticUpdates_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (IsEdit)
        {
            Config.Config.MainConfig.UpdateModel.IsAutoDetectUpdates = (bool)AutomaticUpdates.IsChecked;
            Config.Config.SaveConfig();
        }
    }
}