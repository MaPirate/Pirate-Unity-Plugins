using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class GitAutomation : EditorWindow
{
    // اضافه کردن گزینه به منوی بالای یونیتی
    [MenuItem("Git Tools/Backup and Push %g")]
    public static void QuickPush()
    {
        UnityEngine.Debug.Log("🚀 Starting Git Backup...");

        // اضافه کردن همه فایل‌ها حتی اگر جدید باشند
        RunGitCommand("add .");
        
        // کامیت کردن تغییرات
        RunGitCommand("commit -m \"Auto-update: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\"");
        
        // تغییر مهم: استفاده از origin HEAD برای ارسال زورکی به همان شاخه‌ای که الان رویش هستی
        RunGitCommand("push origin HEAD");
        
        UnityEngine.Debug.Log("✅ Git Process Finished. Check Console for details.");
    }

    static void RunGitCommand(string gitArguments)
    {
        Process process = new Process();
        ProcessStartInfo processInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = gitArguments,
            CreateNoWindow = true, // پنجره سیاه سی‌ام‌دی باز نشود
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = Application.dataPath.Replace("/Assets", "") // مسیر اصلی پروژه
        };

        process.StartInfo = processInfo;
        process.Start();

        // خواندن خروجی‌ها برای نمایش در کنسول یونیتی
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        // نمایش لاگ‌ها در کنسول یونیتی (فقط اگر خروجی مهمی بود)
        if (!string.IsNullOrEmpty(output))
            UnityEngine.Debug.Log("Git: " + output);
        
        if (!string.IsNullOrEmpty(error) && !error.Contains("nothing to commit"))
            UnityEngine.Debug.LogWarning("Git Warning/Error: " + error);
    }
}