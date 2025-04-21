using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides static utility methods to create common UI components in Unity applications.
/// </summary>
public static class UIFactory
{
    /// <summary>
    /// Creates a background panel with optional anchors.
    /// </summary>
    public static GameObject Panel(string name, Transform parent, Color bgColor, Vector2? anchorMin = null, Vector2? anchorMax = null, bool fullAnchor = false)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        RectTransform rt = go.AddComponent<RectTransform>();

        if (fullAnchor)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }
        else
        {
            rt.anchorMin = anchorMin ?? Vector2.zero;
            rt.anchorMax = anchorMax ?? Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        Image bg = go.AddComponent<Image>();
        bg.color = bgColor;

        return go;
    }

    /// <summary>
    /// Creates a UI text element.
    /// </summary>
    public static Text Text(string name, string content, Transform parent, int fontSize = 16, TextAnchor anchor = TextAnchor.UpperLeft, FontStyle style = FontStyle.Normal)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);

        RectTransform rt = go.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0f, 30f);

        Text txt = go.AddComponent<Text>();
        txt.text = content;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = fontSize;
        txt.alignment = anchor;
        txt.fontStyle = style;
        txt.color = Color.white;

        return txt;
    }

    /// <summary>
    /// Creates a button with label and background color.
    /// </summary>
    public static GameObject Button(string name, string label, Transform parent, Color color)
    {
        GameObject buttonGO = new GameObject(name);
        buttonGO.transform.SetParent(parent, false);

        RectTransform rt = buttonGO.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 40);

        Image img = buttonGO.AddComponent<Image>();
        img.color = color;

        Button btn = buttonGO.AddComponent<Button>();
        btn.targetGraphic = img;

        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(buttonGO.transform, false);

        Text txt = textGO.AddComponent<Text>();
        txt.text = label;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = 18;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.color = Color.white;

        RectTransform textRT = textGO.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;

        return buttonGO;
    }

    /// <summary>
    /// Creates a vertical scrollable list container.
    /// </summary>
    public static RectTransform ScrollableVerticalList(string name, Transform parent, out VerticalLayoutGroup layoutGroup)
    {
        GameObject scrollGO = new GameObject(name);
        scrollGO.transform.SetParent(parent, false);

        RectTransform scrollRT = scrollGO.AddComponent<RectTransform>();
        scrollRT.anchorMin = Vector2.zero;
        scrollRT.anchorMax = Vector2.one;
        scrollRT.offsetMin = Vector2.zero;
        scrollRT.offsetMax = Vector2.zero;

        ScrollRect scroll = scrollGO.AddComponent<ScrollRect>();
        scroll.horizontal = false;

        GameObject viewport = Panel("Viewport", scrollGO.transform, new Color(0, 0, 0, 0.05f));
        Mask mask = viewport.AddComponent<Mask>();
        mask.showMaskGraphic = false;

        scroll.viewport = viewport.GetComponent<RectTransform>();

        GameObject content = new GameObject("Content");
        content.transform.SetParent(viewport.transform, false);

        RectTransform contentRT = content.AddComponent<RectTransform>();
        contentRT.anchorMin = new Vector2(0, 1);
        contentRT.anchorMax = new Vector2(1, 1);
        contentRT.pivot = new Vector2(0.5f, 1);
        contentRT.anchoredPosition = Vector2.zero;

        layoutGroup = content.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childAlignment = TextAnchor.UpperCenter;
        layoutGroup.childControlHeight = true;
        layoutGroup.childForceExpandHeight = false;

        ContentSizeFitter fitter = content.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scroll.content = contentRT;

        return scrollRT;
    }
}
