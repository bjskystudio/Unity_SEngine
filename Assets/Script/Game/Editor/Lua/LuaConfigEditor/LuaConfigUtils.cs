﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class LuaConfigUtils
{
    public static readonly string InputConfigPath = Application.dataPath + "/Config/TxtConfig";
    //public static readonly string InputConfigAssetPath = "Assets/TempRes/TxtConfig";
    public static readonly string OutConfigPath = Application.dataPath + "Res/LuaScript/LuaConfig/Auto";
    public static readonly string OutEditorConfigPath = Application.dataPath + "Res/LuaScript/Editor/ConfigTips";

    public static bool DebugUtil = false;

    /// <summary>
    /// 把Txt配置转换为Lua配置
    /// </summary>
    public static void ImportTxtToLuaConfig()
    {
        Directory.CreateDirectory(OutConfigPath);
        Directory.CreateDirectory(OutEditorConfigPath);
        string[] dirs = Directory.GetDirectories(InputConfigPath, "*", SearchOption.AllDirectories);

        for (int i = 0; i < dirs.Length; i++)
        {
            string dir = dirs[i];
            string luaPath = dir.Replace(InputConfigPath, OutConfigPath);
            if (!Directory.Exists(luaPath))
                Directory.CreateDirectory(luaPath);
        }
        string[] files = Directory.GetFiles(InputConfigPath, "*.csv", SearchOption.AllDirectories);
        List<string> fileList = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            //fileList.Add(File.FormatToUnityPath(files[i]));

            fileList.Add(FormatToUnityPath(files[i]));
        }
        files = fileList.ToArray();
        if (files.Length > 0)
        {
            LuaEnv luaEnv = new LuaEnv();
            luaEnv.AddLoader(CustomLoader);
            luaEnv.DoString(string.Format("require('{0}')", "Editor.Config.OPStaticData"));

            if (DebugUtil)
            {
                string debugDll = UnityEngine.Application.dataPath + "/../EmmyLuaDebugger/windows/x64/?.dll";
                byte[] byteArray = System.Text.Encoding.Default.GetBytes($"package.cpath = package.cpath .. ';{debugDll}'\nlocal dbg = require('emmy_core')\ndbg.tcpConnect('localhost', 9966)");
                luaEnv.DoString(Encoding.UTF8.GetString(byteArray), "trunk");
            }

            luaEnv.Global.GetInPath<Action<string[], string[]>>("OPStaticData.Start")?.Invoke(files, files);
            // MapEditorUtils.ClearLua();
        }
    }

    private static string FormatToUnityPath(string path)
    {
        path = path.Replace("\\", "/");
        return path;
    }

    private static byte[] CustomLoader(ref string filepath)
    {
        string scriptPath = string.Empty;
        filepath = filepath.Replace(".", "/");
        string filepath2 = filepath + ".lua";
        scriptPath = Path.Combine(Application.dataPath, "Res/LuaScript");
        scriptPath = Path.Combine(scriptPath, filepath2);
        var luaNames = filepath.Split('/');
        var luaName = luaNames[luaNames.Length - 1];
        if (luaName == "emmy_core")
        {
            return null;
        }
        else
        {
            return File.ReadAllBytes(scriptPath);
        }
    }

    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="path">文件绝对路径</param>
    public static string ReadFile(string path)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            };
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace + "\n" + e.Message);
            return null;
        }
    }

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="path">文件绝对路径</param>
    /// <param name="content">写入内容</param>
    public static bool WriteFile(string path, string content)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
            };
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace + "\n" + e.Message);
            return false;
        }
    }
}
