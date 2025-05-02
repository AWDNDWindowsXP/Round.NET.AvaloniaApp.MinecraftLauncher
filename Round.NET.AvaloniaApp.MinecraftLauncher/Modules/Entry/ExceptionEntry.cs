﻿using System;
using System.Collections.Generic;
using MinecraftLaunch.Base.Models.Game;
using Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Config;
using Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Enum;
using Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Game.User;
using Round.NET.VersionServerMange.Library.Entry;

namespace Round.NET.AvaloniaApp.MinecraftLauncher.Modules.Entry;

public class ExceptionEntry
{
    public ConfigRoot MainConfig { get; set; } = Config.Config.MainConfig;
    public List<JavaEntry> JavaRoot = Java.FindJava.JavasList;
    public List<User.UserConfig> UserConfig { get; set; } = User.Users;
    public ExceptionEnum ExceptionType { get; set; }
    public DateTime RecordTime { get; set; } = DateTime.Now;
    public string Exception { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
    public string SystemVersion { get; set; } = string.Empty;
    public string SystemLanguage { get; set; } = string.Empty;
    public string SystemTimeZone { get; set; } = string.Empty;
    public string ExceptionSource { get; set; } = string.Empty;
}