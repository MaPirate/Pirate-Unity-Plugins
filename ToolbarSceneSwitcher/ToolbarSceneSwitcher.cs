using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

// Title: Toolbar Scene Switcher (Position & Color Fixed)
// Description: Located at the rightmost of the Left-Zone. Grey colored button.

[InitializeOnLoad]
public static class ToolbarSceneSwitcher
{
    private static ScriptableObject _toolbar;
    private static string _currentSceneName;

    static ToolbarSceneSwitcher()
    {
        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        if (_toolbar == null)
        {
            var toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
            var toolbars = Resources.FindObjectsOfTypeAll(toolbarType);
            _toolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
        }

        if (_toolbar != null)
        {
            var rootProperty = _toolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
            var root = rootProperty?.GetValue(_toolbar) as VisualElement;

            if (root != null)
            {
                // تغییر ۱: پیدا کردن ناحیه سمت چپ (جایی که دکمه‌های کلاود و اکانت هستند)
                var toolbarZoneLeft = root.Q("ToolbarZoneLeftAlign");

                if (toolbarZoneLeft != null && toolbarZoneLeft.Q("SceneSwitcherContainer") == null)
                {
                    var container = new IMGUIContainer(OnGUI);
                    container.name = "SceneSwitcherContainer";
                    
                    // تنظیمات کانتینر
                    container.style.width = 140; // عرض دکمه
                    container.style.justifyContent = Justify.Center;
                    container.style.marginTop = 4; // تراز ارتفاع
                    container.style.marginLeft = 10; // فاصله از آیکون‌های قبلی

                    // تغییر ۲: استفاده از Add به جای Insert برای قرار گرفتن در آخرِ لیست سمت چپ
                    toolbarZoneLeft.Add(container);
                }
            }
        }
    }

    private static void OnGUI()
    {
        var scene = EditorSceneManager.GetActiveScene();
        _currentSceneName = string.IsNullOrEmpty(scene.name) ? "Unsaved" : scene.name;

        // تغییر ۳: تغییر رنگ پس‌زمینه دکمه به خاکستری روشن
        var defaultColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.7f, 0.7f, 0.7f, 1f); // رنگ خاکستری (هرچه عدد بیشتر، روشن‌تر)

        // استایل دکمه
        var style = new GUIStyle(EditorStyles.toolbarDropDown);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white; // رنگ متن سفید

        if (GUILayout.Button($"{_currentSceneName}", style))
        {
            OpenScenePopup();
        }

        // برگرداندن رنگ به حالت پیش‌فرض تا روی بقیه ادیتور تاثیر نگذارد
        GUI.backgroundColor = defaultColor;
    }

    private static void OpenScenePopup()
    {
        GenericMenu menu = new GenericMenu();
        var scenes = EditorBuildSettings.scenes;

        if (scenes.Length == 0)
        {
            menu.AddDisabledItem(new GUIContent("No Scenes in Build Settings"));
        }

        foreach (var scene in scenes)
        {
            if (!scene.enabled) continue;

            string name = System.IO.Path.GetFileNameWithoutExtension(scene.path);
            string path = scene.path;
            bool isCurrent = (EditorSceneManager.GetActiveScene().path == path);

            menu.AddItem(new GUIContent(name), isCurrent, () =>
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(path);
                }
            });
        }

        menu.ShowAsContext();
    }
}