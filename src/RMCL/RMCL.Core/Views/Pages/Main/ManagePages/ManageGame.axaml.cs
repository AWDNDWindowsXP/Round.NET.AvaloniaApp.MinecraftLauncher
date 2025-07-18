﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using OverrideLauncher.Core.Modules.Classes.Account;
using OverrideLauncher.Core.Modules.Classes.Launch.Client;
using OverrideLauncher.Core.Modules.Classes.Version;
using OverrideLauncher.Core.Modules.Entry.GameEntry;
using OverrideLauncher.Core.Modules.Entry.JavaEntry;
using OverrideLauncher.Core.Modules.Entry.LaunchEntry;
using OverrideLauncher.Core.Modules.Enum.Launch;
using RMCL.Base.Entry;
using RMCL.Base.Interface;
using RMCL.Controls.Item;
using RMCL.Core.Models.Classes;
using RMCL.Core.Models.Classes.Launch;
using RMCL.Core.Views.Pages.ChildFramePage.Game;
using RMCL.Core.Views.Windows.Main.ManageWindows;

namespace RMCL.Core.Views.Pages.Main.ManagePages;

public partial class ManageGame : ISetting
{
    public List<VersionParse> Versions = new();
    public ManageGame()
    {
        InitializeComponent();
        
        Refuse();
    }

    public void UpdateUI(string searchText = null)
    {
        IsEdit = false;
        VersionsList.Items.Clear();
        VersionsList.IsVisible = false;
        LoadingBox.IsVisible = true;
        Versions.Clear();
        
        NullBox.IsVisible = false;
        Task.Run(() =>
        {
            var path = Path.GetFullPath(Path.Combine(
                Config.Config.MainConfig.GameFolders[Config.Config.MainConfig.SelectedGameFolder].Path, "versions"));
            Directory.CreateDirectory(path);

            var vers = Directory.GetDirectories(path);
            vers.ToList().ForEach(x => Versions.Add(new VersionParse(new ClientInstancesInfo()
            {
                GameCatalog =
                    Config.Config.MainConfig.GameFolders[Config.Config.MainConfig.SelectedGameFolder].Path,
                GameName = Path.GetFileName(x)
            })));

            // 如果有搜索文本，则进行过滤
            if (!string.IsNullOrEmpty(searchText))
            {
                Versions = Versions.Where(version =>
                {
                    if (version.GameJson.Id != null) return version.GameJson.Id.ToLower().Contains(searchText.ToLower());
                    else return false;
                }).ToList();
            }

            foreach (var ver in Versions)
            {
                // if (string.IsNullOrEmpty(ver.GameJson.Type)) continue;
                Dispatcher.UIThread.Invoke(() =>
                {
                    var item = new ManagerGameItem(ver);
                    item.OnLaunch = parse =>
                    {
                        Task.Run(() =>
                        {
                            try
                            {
                                LaunchService.LaunchTask(new LaunchClientInfo()
                                {
                                    GameFolder = ver.ClientInstances.GameCatalog,
                                    GameName = ver.ClientInstances.GameName
                                });
                            }
                            catch { }
                        });
                    };
                    item.OnSetting = parse =>
                    {
                        Models.Classes.Core.ChildFrame.Show(new GameClientSetting(ver), () =>
                        {
                            Models.Classes.Core.ImageResourcePool.Cleanup();
                        });
                    };
                    VersionsList.Items.Add(item);
                });
            }

            Dispatcher.UIThread.Invoke(() =>
            {
                VersionsList.SelectedIndex = Config.Config.MainConfig
                    .GameFolders[Config.Config.MainConfig.SelectedGameFolder]
                    .SelectedGameIndex;
                VersionsList.IsVisible = true;
                LoadingBox.IsVisible = false;
                if (Versions.Count == 0)
                {
                    NullBox.IsVisible = true;
                }
            });

            IsEdit = true;
        });
    }

    public void Refuse()
    {
        IsEdit = false;
        VersionChoseBox.Items.Clear();
        Config.Config.MainConfig.GameFolders.ForEach(x =>
        {
            VersionChoseBox.Items.Add(new ComboBoxItem() { Content = x.Name });
        });
        VersionChoseBox.SelectedIndex = Config.Config.MainConfig.SelectedGameFolder;

        IsEdit = true;
        UpdateUI(SearchBox.Text);
    }
    private void RefuseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Refuse();
    }

    private void SearchBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (IsEdit)
        {
            // 获取搜索框的内容
            var searchText = SearchBox.Text;
            /*if (!string.IsNullOrEmpty(searchText))
            {
                SearchDocBox.Margin = new Thickness(10, 50, 10, -40);
                BodyBox.Margin = new Thickness(0, 50, 0,0);
            }
            else
            {
                SearchDocBox.Margin = new Thickness(10);
                BodyBox.Margin = new Thickness(0);
                
            }*/
            UpdateUI(searchText);
        }
    }

    private void ManageTheGameCatalog_OnClick(object? sender, RoutedEventArgs e)
    {
        new ManageGameDirectory().ShowDialog(Models.Classes.Core.MainWindow);
        Refuse();
    }

    private void VersionChoseBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (IsEdit)
        {
            Config.Config.MainConfig.SelectedGameFolder = VersionChoseBox.SelectedIndex;
            Config.Config.SaveConfig();
            
            UpdateUI(SearchBox.Text);
        }
    }

    private void OpenNowGameRoot_OnClick(object? sender, RoutedEventArgs e)
    {
        SystemHelper.SystemHelper.FileExplorer.OpenFolder(Config.Config.MainConfig.GameFolders[Config.Config.MainConfig.SelectedGameFolder].Path);
    }

    private void VersionsList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (IsEdit)
        {
            Config.Config.MainConfig.GameFolders[Config.Config.MainConfig.SelectedGameFolder].SelectedGameIndex =
                VersionsList.SelectedIndex;
            
            Config.Config.SaveConfig();
        }
    }
}