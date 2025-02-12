﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using HeroIconsAvalonia.Controls;
using HeroIconsAvalonia.Enums;

namespace Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Mange.TilesMange;

public class TilesMange
{
    public static List<StackPanel> TileGroups = new();
    public static WrapPanel TilesPanel = new();
    public class TileItem
    {
        public enum TiteStyleType
        {
            Big,
            Small,
            Long
        }
        public enum TiteEventType
        {
            Example,
            Nav,
            LinkButton
        }
        
        public TiteStyleType TiteStyle { get; set; } = TiteStyleType.Small;
        public Action TiteEvent { get; set; }
        public string Text { get; set; } = string.Empty;
        public ContentControl Content { get; set; } = new HeroIcon()
        {
            Foreground = Brushes.White,
            Type = IconType.Home
        };
    }
    public static StackPanel RegisterTileGroup()
    {
        var tileGroup = new StackPanel();
        TilesPanel.Children.Add(tileGroup);
        return tileGroup;
    }
    public static void RegisterTile(StackPanel tileGroup,TileItem tileItem)
    {
        var tile = new Button()
        {
            Margin = new Thickness(4)
        };
        tile.Click += (_, __) => tileItem.TiteEvent();
        if (tileItem.TiteStyle != TileItem.TiteStyleType.Small)
        {
            tile.Content = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Width = 128,
                Height = 128,
                Margin = new Thickness(0),
                Children =
                {
                    new Label()
                    {
                        Content = tileItem.Text,
                        Margin = new Thickness(4),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Bottom,
                    },
                    tileItem.Content
                }
            };
        }
        switch (tileItem.TiteStyle)
        {
            case TileItem.TiteStyleType.Small:
                tile.Width = 60;
                tile.Height = 60;
                tile.Content = tileItem.Content;
                break;
            case TileItem.TiteStyleType.Big:
                tile.Width = 128;
                tile.Height = 128;
                break;
            case TileItem.TiteStyleType.Long:
                tile.Width = 128;
                tile.Height = 60;
                ((Grid)(tile.Content)).Height = 60;
                break;
        }

        if (Config.Config.MainConfig.IsTilsEnabled)
        {
            tileGroup.Children.Add(tile);
        }
    }
}