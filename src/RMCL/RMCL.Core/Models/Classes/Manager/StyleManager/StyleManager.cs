using System;
using System.IO;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using FluentAvalonia.UI.Controls;
using RMCL.Base.Entry.Style;
using RMCL.Base.Enum;
using RMCL.Base.Enum.BackCall;
using RMCL.Config;

namespace RMCL.Core.Models.Classes.Manager.StyleManager;

public class StyleManager
{
    public static void UpdateSystemColor(string htmlColor = null)
    {
        if (!string.IsNullOrEmpty(htmlColor))
        {
            Config.Config.MainConfig.ThemeColors.ThemeColors = htmlColor;
            Config.Config.SaveConfig();
        }
        else
        {
            htmlColor = Config.Config.MainConfig.ThemeColors.ThemeColors;
        }

        if (Config.Config.MainConfig.ThemeColors.ColorType == ColorType.System)
        {
            Core.FluentAvaloniaTheme.PreferUserAccentColor = true;
        }
        else
        {
            Core.FluentAvaloniaTheme.PreferUserAccentColor = false;
            Core.FluentAvaloniaTheme.CustomAccentColor = Color.Parse(htmlColor);
        }
    }
    public static void UpdateBackground()
    {
        Core.MainWindow.Background = Config.Config.MainConfig.ThemeColors.Theme == ThemeType.Dark ? Brush.Parse("#161616") : Brushes.AliceBlue;
        Core.MainWindow.TransparencyLevelHint = new[] { WindowTransparencyLevel.Transparent };
        Core.MainWindow.InvalidateVisual();
        Core.MainWindow.BackOpacity.Opacity = 0;

        UpdateSystemColor();

        switch (Config.Config.MainConfig.Background.ChooseModel)
        {
            case BackgroundModelEnum.None:
                break;
            case BackgroundModelEnum.Mica:
                Core.MainWindow.Background = Brushes.Transparent;
                Core.MainWindow.TransparencyLevelHint = new[] { WindowTransparencyLevel.Mica };
                break;
            case BackgroundModelEnum.AcrylicBlur:
                Core.MainWindow.Background = Brushes.Transparent;
                Core.MainWindow.TransparencyLevelHint = new[] { WindowTransparencyLevel.AcrylicBlur };
                break;
            case BackgroundModelEnum.Glass:
                //Core.MainWindow.TransparencyLevelHint = new[] { WindowTransparencyLevel.Blur };
                Core.MainWindow.Background = new SolidColorBrush()
                {
                    Color = Color.Parse(Config.Config.MainConfig.Background.ColorGlassEntry.HtmlColor)
                };
                break;
            case BackgroundModelEnum.Image:
                if (Config.Config.MainConfig.Background.ImageEntry.ChooseIndex == -1) return;
                Core.MainWindow.Background = new ImageBrush()
                {
                    Source = new Bitmap(Config.Config.MainConfig.Background.ImageEntry.ImagePaths[Config.Config.MainConfig.Background.ImageEntry.ChooseIndex]),
                    Stretch = Stretch.UniformToFill
                };
                Core.MainWindow.BackOpacity.Opacity =
                    (double)(100 - Config.Config.MainConfig.Background.ImageEntry.Opacity) / 100;
                break;
            case BackgroundModelEnum.Pack:
                if (Config.Config.MainConfig.Background.PackEntry.SelectedIndex == -1) return;
                try
                {
                    LoadSkin.ImportStyleConfigFile(
                        Directory.GetFiles(PathsDictionary.PathDictionary.SkinFolder)[
                            Config.Config.MainConfig.Background.PackEntry.SelectedIndex]);
                }
                catch
                {
                    Models.Classes.Core.MessageShowBox.AddInfoBar("加载错误", $"当前选择的主题文件无效", InfoBarSeverity.Error);
                }
                break;
        }
        
        BackCallManager.BackCallManager.Call(BackCallType.UpdateBackground);
    }

    public static Type GetObjectType(object obj)
    {
        return obj.GetType();
    }
}