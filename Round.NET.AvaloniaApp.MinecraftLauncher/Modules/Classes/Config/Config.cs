﻿using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Avalonia.Controls.Documents;
using Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Java;
using SkiaSharp;
using User = Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Game.User.User;

namespace Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Config;

public class ConfigRoot
{
    public List<GameFolderConfig> GameFolders { get; set; } = new();
    public bool IsUseOrganizationConfig { get; set; } = false;
    public string OrganizationUrl { get; set; } = string.Empty;
    public int SelectedGameFolder { get; set; } = 0;
    public int SelectedJava { get; set; } = 0;
    public int SelectedUser { get; set; } = 0;
    public int BackModlue { get; set; } = 0;
    public int UpdateSourse { get; set; } = 0;
    public string BackImage { get; set; } = string.Empty;
    public double BackOpacity { get; set; } = 50;
    public string StyleFile { get; set; } = string.Empty;
    public bool ShowErrorWindow { get; set; } = true;
    public bool IsAutoUpdate { get; set; } = true;
    public bool IsDebug { get; set; } = false;
    public int WindowWidth { get; set; } = 850;
    public int WindowHeight { get; set; } = 450;
    public bool IsUsePlug { get; set; } = true;
    public int MessageLiveTimeMs { get; set; } = 5000;
    public MainHomeType HomeBody { get; set; } = 0;
    public bool SetTheLanguageOnStartup { get; set; } = true;
    public bool SetTheGammaOnStartup { get; set; } = true;
    public int JavaUseMemory { get; set; } = 4096;
    public bool IsLaunchJavaMemory { get; set; } = true;
    public int GameLogOpenModlue { get; set; } = 0;
    public int DownloadThreads { get; set; } = 256;
    public int WindowX { get; set; } = 0;
    public int WindowY { get; set; } = 0;
    public string RSAccount { get; set; } = string.Empty;
#if true
    public string LoginServer { get; set; } = Config.LoginServerIP;
#else
    [JsonIgnore]
    public string LoginServer = Config.LoginServerIP;
#endif
}

public class GameFolderConfig
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int SelectedGameIndex { get; set; } = 0;
}

public enum MainHomeType
{
    None = 0,
    Card = 1,
    Custom = 2
}
public class Config
{
#if DEBUG
    public static string LoginServerIP = "http://127.0.0.1:32127";
#else
    public static readonly string LoginServerIP = "http://account.roundstduio.top:32127";
#endif
    
    public static bool IsInitialized { get; set; } = false;
    public static ConfigRoot MainConfig = new()
    {
        GameFolders = new()
        {
            new GameFolderConfig
            {
                Name = "当前文件夹",
                Path = Path.GetFullPath("../RMCL/RMCL.Minecraft")
            }
        }
    };
    
    private const string ConfigFileName = "../RMCL/RMCL.Config/Config.json";
    public static void LoadConfig()
    {
        if (!File.Exists(ConfigFileName))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigFileName));
            SaveConfig();
            return;
        }
        
        var json = File.ReadAllText(Path.GetFullPath(ConfigFileName));
        if (string.IsNullOrEmpty(json))
        {
            SaveConfig();
        }
        else
        {
            try
            {
                MainConfig = JsonSerializer.Deserialize<ConfigRoot>(json);
                if (!IsInitialized)
                {
                    IsInitialized = true;
                }
            }
            catch
            {
                SaveConfig();
            }
        }
    }

    public static void SaveConfig()
    {
        string jsresult = Regex.Unescape(JsonSerializer.Serialize(MainConfig, new JsonSerializerOptions() { WriteIndented = true }).Replace("\\","\\\\")); //获取结果并转换成正确的格式
        File.WriteAllText(Path.GetFullPath(ConfigFileName), jsresult);
    }
}