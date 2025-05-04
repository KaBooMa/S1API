#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppInterop.Runtime.InteropTypes.Arrays;
#endif

using System;
using S1API.Internal.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace S1API.UI
{
    /// <summary>
    /// Utility class for constructing and configuring various UI elements in Unity.
    /// </summary>
    /// <remarks>
    /// Provides a collection of static methods to create and manage UI components, such as panels, buttons,
    /// text fields, layouts, and more. It includes functionality for customizing appearance, alignment,
    /// and interactive behavior, catering to the needs of efficient and dynamic UI design.
    /// </remarks>
    public static class UIFactory
    {
        /// Creates a UI panel with a background color and optional anchoring.
        /// <param name="name">The name of the GameObject representing the panel.</param>
        /// <param name="parent">The transform to which the panel will be parented.</param>
        /// <param name="bgColor">The background color of the panel.</param>
        /// <param name="anchorMin">The minimum anchor point of the RectTransform. Defaults to (0.5, 0.5) if not specified.</param>
        /// <param name="anchorMax">The maximum anchor point of the RectTransform. Defaults to (0.5, 0.5) if not specified.</param>
        /// <param name="fullAnchor">Whether to stretch the panel across the entire parent RectTransform. Overrides anchorMin and anchorMax if true.</param>
        /// <returns>The GameObject representing the created UI panel.</returns>
        public static GameObject Panel(string name, Transform parent, Color bgColor, Vector2? anchorMin = null,
            Vector2? anchorMax = null, bool fullAnchor = false)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();

            if (fullAnchor)
            {
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
            else
            {
                rt.anchorMin = anchorMin ?? new Vector2(0.5f, 0.5f);
                rt.anchorMax = anchorMax ?? new Vector2(0.5f, 0.5f);
            }

            var img = go.AddComponent<Image>();
            img.color = bgColor;
            return go;
        }

        /// Creates a Text UI element with specified properties.
        /// <param name="name">The name of the GameObject to create for the text element.</param>
        /// <param name="content">The content of the text to display.</param>
        /// <param name="parent">The Transform to which the created text GameObject will be assigned.</param>
        /// <param name="fontSize">The font size of the text. Defaults to 14.</param>
        /// <param name="anchor">The alignment of the text within its RectTransform. Defaults to `TextAnchor.UpperLeft`.</param>
        /// <param name="style">The font style of the text. Defaults to `FontStyle.Normal`.</param>
        /// <returns>The created Text component with the specified properties applied.</returns>
        public static Text Text(string name, string content, Transform parent, int fontSize = 14,
            TextAnchor anchor = TextAnchor.UpperLeft, FontStyle style = FontStyle.Normal)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();

            var txt = go.AddComponent<Text>();
            txt.text = content;
            txt.fontSize = fontSize;
            txt.alignment = anchor;
            txt.fontStyle = style;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.color = Color.white;
            txt.horizontalOverflow = HorizontalWrapMode.Wrap;
            txt.verticalOverflow = VerticalWrapMode.Overflow;
            return txt;
        }

        /// Creates a scrollable vertical list UI component with a configured child hierarchy, allowing vertical scrolling of dynamically added items.
        /// <param name="name">The name of the scrollable list GameObject.</param>
        /// <param name="parent">The parent transform where the scrollable list will be added.</param>
        /// <param name="scrollRect">Outputs the ScrollRect component associated with the created scrollable list.</param>
        /// <returns>Returns the RectTransform of the "Content" GameObject, allowing items to be added to the scrollable list.</returns>
        public static RectTransform ScrollableVerticalList(string name, Transform parent, out ScrollRect scrollRect)
        {
            var scrollGO = new GameObject(name);
            scrollGO.transform.SetParent(parent, false);
            var scrollRT = scrollGO.AddComponent<RectTransform>();
            scrollRT.anchorMin = Vector2.zero;
            scrollRT.anchorMax = Vector2.one;
            scrollRT.offsetMin = Vector2.zero;
            scrollRT.offsetMax = Vector2.zero;

            scrollRect = scrollGO.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;

            var viewport = new GameObject("Viewport");
            viewport.transform.SetParent(scrollGO.transform, false);
            var viewportRT = viewport.AddComponent<RectTransform>();
            viewportRT.anchorMin = Vector2.zero;
            viewportRT.anchorMax = Vector2.one;
            viewportRT.offsetMin = Vector2.zero;
            viewportRT.offsetMax = Vector2.zero;
            viewport.AddComponent<Image>().color = new Color(0, 0, 0, 0.05f);
            viewport.AddComponent<Mask>().showMaskGraphic = false;
            scrollRect.viewport = viewportRT;

            var content = new GameObject("Content");
            content.transform.SetParent(viewport.transform, false);
            var contentRT = content.AddComponent<RectTransform>();
            contentRT.anchorMin = new Vector2(0, 1);
            contentRT.anchorMax = new Vector2(1, 1);
            contentRT.pivot = new Vector2(0.5f, 1);

            var layout = content.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlHeight = true;
            layout.childForceExpandHeight = false;

            content.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.content = contentRT;
            return contentRT;
        }

        /// Adjusts the height of the RectTransform content to fit its children's preferred layout size.
        /// Adds a ContentSizeFitter component to the content if one is not present.
        /// <param name="content">The RectTransform whose height will be adjusted to match the preferred size of its children.</param>
        public static void FitContentHeight(RectTransform content)
        {
            var fitter = content.gameObject.GetComponent<ContentSizeFitter>();
            if (fitter == null)
            {
	            fitter = content.gameObject.AddComponent<ContentSizeFitter>();
            }

            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        /// <summary>
        /// A private static field used to store a cached Sprite instance with rounded corners.
        /// This sprite is dynamically generated within the GetRoundedSprite method and used
        /// for UI elements requiring a rounded appearance to prevent redundant sprite creation.
        /// </summary>
        private static Sprite roundedSprite;

        /// Creates a rounded button with a label inside a mask container for rounded corners.
        /// <param name="name">The name of the GameObject representing the button.</param>
        /// <param name="label">The text displayed on the button.</param>
        /// <param name="parent">The transform to which the button will be parented.</param>
        /// <param name="bgColor">The background color of the button.</param>
        /// <param name="width">The width of the button in pixels.</param>
        /// <param name="height">The height of the button in pixels.</param>
        /// <param name="fontSize">The font size of the label text.</param>
        /// <param name="textColor">The color of the label text.</param>
        /// <returns>A tuple containing the mask container GameObject, the Button component, and the Text component.</returns>
        public static (GameObject, Button, Text) RoundedButtonWithLabel(string name, string label, Transform parent,
                                        Color bgColor, float width, float height ,   int fontSize ,Color textColor)
        {
            // Create mask container with rounded corners
            var maskGO = new GameObject(name + "_RoundedMask");
            maskGO.transform.SetParent(parent, false);

            var maskRT = maskGO.AddComponent<RectTransform>();
            maskRT.sizeDelta = new Vector2(width, height);

            // ✅ Add layout element for layout sizing
            var layoutElement = maskGO.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = width;
            layoutElement.preferredHeight = height;

            var maskImage = maskGO.AddComponent<Image>();
            maskImage.sprite = GetRoundedSprite();
            maskImage.type = Image.Type.Sliced;
            maskImage.color = Color.white;

            var mask = maskGO.AddComponent<Mask>();
            mask.showMaskGraphic = true;

            // Create actual button inside mask
            var buttonGO = new GameObject(name);
            buttonGO.transform.SetParent(maskGO.transform, false);

            var rt = buttonGO.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            var img = buttonGO.AddComponent<Image>();
            img.color = bgColor;
            img.sprite = GetRoundedSprite();
            img.type = Image.Type.Sliced;

            var btn = buttonGO.AddComponent<Button>();
            btn.targetGraphic = img;

            // Label
            var textGO = new GameObject("Label");
            textGO.transform.SetParent(buttonGO.transform, false);

            var textRT = textGO.AddComponent<RectTransform>();
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            textRT.offsetMin = Vector2.zero;
            textRT.offsetMax = Vector2.zero;

            var txt = textGO.AddComponent<Text>();
            txt.text = label;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.fontSize = fontSize;
            txt.fontStyle = FontStyle.Bold;
            txt.color = textColor;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            return (maskGO, btn, txt);
        }


        /// Generates and retrieves a reusable Sprite with rounded corners for UI elements.
        /// The sprite is created with a specified border for safe slicing and consistent scaling.
        /// <returns>A Sprite instance with rounded corners, ready for use in UI components.</returns>
        private static Sprite GetRoundedSprite()
        {
            if (roundedSprite != null)
            {
	            return roundedSprite;
            }

            int size = 32;
            var tex = new Texture2D(size, size, TextureFormat.ARGB32, false);
            Color32 transparent = new Color32(0, 0, 0, 0);
            Color32 white = new Color32(255, 255, 255, 255);

            float radius = 6f; // radius for rounded corners

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    bool isCorner =
                        (x < radius && y < radius && Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius)) > radius) || // bottom-left
                        (x > size - radius - 1 && y < radius && Vector2.Distance(new Vector2(x, y), new Vector2(size - radius - 1, radius)) > radius) || // bottom-right
                        (x < radius && y > size - radius - 1 && Vector2.Distance(new Vector2(x, y), new Vector2(radius, size - radius - 1)) > radius) || // top-left
                        (x > size - radius - 1 && y > size - radius - 1 && Vector2.Distance(new Vector2(x, y), new Vector2(size - radius - 1, size - radius - 1)) > radius); // top-right

                    tex.SetPixel(x, y, isCorner ? transparent : white);
                }
            }

            tex.Apply();

            // Border = safe slicing margins (try 8px for all sides)
            var border = new Vector4(8, 8, 8, 8);
            roundedSprite = Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, border);
            return roundedSprite;
        }

        /// Creates a horizontal row of buttons using a HorizontalLayoutGroup with configurable spacing and alignment.
        /// <param name="name">The name of the GameObject representing the button row.</param>
        /// <param name="parent">The transform to which the button row will be parented.</param>
        /// <param name="spacing">The spacing between each button in the row. Defaults to 12.</param>
        /// <param name="alignment">The alignment of the buttons within the row. Defaults to TextAnchor.MiddleCenter.</param>
        /// <returns>The GameObject representing the created button row.</returns>
        public static GameObject ButtonRow(string name, Transform parent, float spacing = 12f, TextAnchor alignment = TextAnchor.MiddleCenter)
        {
            var row = new GameObject(name);
            row.transform.SetParent(parent, false);

            var rt = row.AddComponent<RectTransform>();
            HorizontalLayoutGroup hLayout = row.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing = spacing;
            hLayout.childAlignment = alignment;

            hLayout.childControlWidth = false;
            hLayout.childControlHeight = false;
            hLayout.childForceExpandWidth = false;
            hLayout.childForceExpandHeight = false;

            return row;
        }

        /// Creates a button with a label and specific dimensions, adding it as a child to a parent UI element.
        /// <param name="name">The name of the button GameObject.</param>
        /// <param name="label">The text to display on the button.</param>
        /// <param name="parent">The Transform to which the button GameObject will be parented.</param>
        /// <param name="bgColor">The background color of the button.</param>
        /// <param name="Width">The width of the button.</param>
        /// <param name="Height">The height of the button.</param>
        /// <returns>A tuple containing the button's GameObject, Button component, and Text component.</returns>
        public static (GameObject, Button, Text) ButtonWithLabel(string name, string label, Transform parent,
            Color bgColor, float Width, float Height)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(Height, Width);

            var img = go.AddComponent<Image>();
            img.color = bgColor;
            img.sprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
            img.type = Image.Type.Sliced;

            var btn = go.AddComponent<Button>();
            btn.targetGraphic = img;

            var textGO = new GameObject("Label");
            textGO.transform.SetParent(go.transform, false);
            var textRT = textGO.AddComponent<RectTransform>();
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            textRT.offsetMin = Vector2.zero;
            textRT.offsetMax = Vector2.zero;

            var txt = textGO.AddComponent<Text>();
            txt.text = label;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.fontSize = 16;
            txt.fontStyle = FontStyle.Bold;
            txt.color = Color.white;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            return (go, btn, txt);
        }

        /// <summary>
        /// Sets an icon as a child of the specified parent transform with the given sprite.
        /// </summary>
        /// <param name="sprite">The sprite to be used as the icon.</param>
        /// <param name="parent">The transform that will act as the parent of the icon.</param>
        public static void SetIcon(Sprite sprite, Transform parent)
        {
            var icon = new GameObject("Icon");
            icon.transform.SetParent(parent, false);

            var rt = icon.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            var img = icon.AddComponent<Image>();
            img.sprite = sprite;
            img.preserveAspect = true;
        }

        /// Creates a text block consisting of a title, subtitle, and an optional completed status label.
        /// <param name="parent">The parent transform where the text block will be added.</param>
        /// <param name="title">The title text of the text block, displayed in bold.</param>
        /// <param name="subtitle">The subtitle text of the text block, displayed below the title.</param>
        /// <param name="isCompleted">A boolean indicating whether the text block represents a completed state. If true, an additional label indicating "Already Delivered" will be added.</param>
        public static void CreateTextBlock(Transform parent, string title, string subtitle, bool isCompleted)
        {
            Text(parent.name + "Title", title, parent, 16, TextAnchor.MiddleLeft, FontStyle.Bold);
            Text(parent.name + "Subtitle", subtitle, parent, 14, TextAnchor.UpperLeft);
            if (isCompleted)
            {
	            Text("CompletedLabel", "<color=#888888><i>Already Delivered</i></color>", parent, 12, TextAnchor.UpperLeft);
            }
        }

        /// Adds a button to the specified GameObject, sets its target graphic, and configures its interactivity and click behavior.
        /// <param name="go">The GameObject to which the button component will be added.</param>
        /// <param name="clickHandler">The UnityAction to invoke when the button is clicked.</param>
        /// <param name="enabled">Specifies whether the button will be interactable.</param>
        public static void CreateRowButton(GameObject go, UnityAction clickHandler, bool enabled)
        {
            var btn = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            btn.targetGraphic = img;
            btn.interactable = enabled;

            btn.onClick.AddListener(clickHandler);
        }

        /// Clears all child objects of the specified parent transform.
        /// <param name="parent">The transform whose child objects will be destroyed.</param>
        public static void ClearChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
	            GameObject.Destroy(child.gameObject);
            }
        }

        /// Configures a GameObject to use a VerticalLayoutGroup with specified spacing and padding.
        /// <param name="go">The GameObject to which a VerticalLayoutGroup will be added or configured.</param>
        /// <param name="spacing">The spacing between child objects within the VerticalLayoutGroup. Default is 10.</param>
        /// <param name="padding">The padding around the edges of the VerticalLayoutGroup. If null, a default RectOffset of (10, 10, 10, 10) will be used.</param>
        public static void VerticalLayoutOnGO(GameObject go, int spacing = 10, RectOffset? padding = null)
        {
            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = spacing;
            layout.padding = padding ?? new RectOffset(10, 10, 10, 10);
        }

        /// Creates a quest row GameObject with a specific layout, including an icon panel and a text panel.
        /// <param name="name">The name of the GameObject representing the quest row.</param>
        /// <param name="parent">The parent Transform to which the quest row will be attached.</param>
        /// <param name="iconPanel">An output parameter that will hold the generated icon panel GameObject.</param>
        /// <param name="textPanel">An output parameter that will hold the generated text panel GameObject.</param>
        /// <returns>The GameObject representing the newly created quest row.</returns>
        public static GameObject CreateQuestRow(string name, Transform parent, out GameObject iconPanel,
            out GameObject textPanel)
        {
            // Create the main row object
            var row = new GameObject("Row_" + name);
            row.transform.SetParent(parent, false);
            var rowRT = row.AddComponent<RectTransform>();
            rowRT.sizeDelta = new Vector2(0f, 90f); // Let layout handle width
            row.AddComponent<LayoutElement>().minHeight = 50f;
            row.AddComponent<Outline>().effectColor = new Color(0, 0, 0, 0.2f); // or Image line separator below


            var line = UIFactory.Panel("Separator", row.transform, new Color(1,1,1,0.05f));
            line.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, 1f);

            // Add background + target graphic
            var bg = row.AddComponent<Image>();
            bg.color = new Color(0.12f, 0.12f, 0.12f);

            var button = row.AddComponent<Button>();
            button.targetGraphic = bg;

            // Layout group
            var layout = row.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 20;
            layout.padding = new RectOffset(75, 10, 10, 10);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            var rowLE = row.AddComponent<LayoutElement>();
            rowLE.minHeight = 90f;
            rowLE.flexibleWidth = 1;

            // Icon panel
            iconPanel = Panel("IconPanel", row.transform, new Color(0.12f, 0.12f, 0.12f));
            var iconRT = iconPanel.GetComponent<RectTransform>();
            iconRT.sizeDelta = new Vector2(80f, 80f);
            var iconLE = iconPanel.AddComponent<LayoutElement>();
            iconLE.preferredWidth = 80f;
            iconLE.preferredHeight = 80f;

            // Text panel
            textPanel = Panel("TextPanel", row.transform, Color.clear);
            VerticalLayoutOnGO(textPanel, spacing: 2);
            var textLE = textPanel.AddComponent<LayoutElement>();
            textLE.minWidth = 200f;
            textLE.flexibleWidth = 1;

            return row;
        }

        /// Creates a top bar UI element with customizable title and layout settings.
        /// <param name="name">The name of the GameObject representing the top bar.</param>
        /// <param name="parent">The transform to which the top bar will be parented.</param>
        /// <param name="title">The text content for the title displayed in the top bar.</param>
        /// <param name="topbarSize">The relative size of the top bar on the Y-axis.</param>
        /// <param name="paddingLeft">The padding on the left side of the top bar.</param>
        /// <param name="paddingRight">The padding on the right side of the top bar.</param>
        /// <param name="paddingTop">The padding on the top side of the top bar.</param>
        /// <param name="paddingBottom">The padding on the bottom side of the top bar.</param>
        /// <returns>The created GameObject representing the top bar.</returns>
        public static GameObject TopBar(string name, Transform parent, string title,
            float topbarSize,
            int paddingLeft, int paddingRight, int paddingTop, int paddingBottom)
        {
            var topBar = Panel(name, parent, new Color(0.15f, 0.15f, 0.15f),
                new Vector2(0f, topbarSize), new Vector2(1f, 1f));

            var layout = topBar.AddComponent<HorizontalLayoutGroup>();
            layout.padding = new RectOffset(paddingLeft, paddingRight, paddingTop, paddingBottom);
            layout.spacing = 20;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            // Title
            var titleText = Text("TopBarTitle", title, topBar.transform, 26, TextAnchor.MiddleLeft, FontStyle.Bold);
            var titleLayout = titleText.gameObject.AddComponent<LayoutElement>();
            titleLayout.minWidth = 300;
            titleLayout.flexibleWidth = 1;

            return topBar;
        }

        /// Adds a HorizontalLayoutGroup component to the specified GameObject and configures its settings.
        /// <param name="go">The GameObject to which the HorizontalLayoutGroup will be added.</param>
        /// <param name="spacing">The spacing between child elements within the layout. Defaults to 10.</param>
        /// <param name="padLeft">The left padding of the layout's RectTransform. Defaults to 0.</param>
        /// <param name="padRight">The right padding of the layout's RectTransform. Defaults to 0.</param>
        /// <param name="padTop">The top padding of the layout's RectTransform. Defaults to 0.</param>
        /// <param name="padBottom">The bottom padding of the layout's RectTransform. Defaults to 0.</param>
        /// <param name="alignment">The alignment of the child elements within the layout. Defaults to TextAnchor.MiddleCenter.</param>
        public static void HorizontalLayoutOnGO(GameObject go, int spacing = 10, int padLeft = 0, int padRight = 0, int padTop = 0, int padBottom = 0, TextAnchor alignment = TextAnchor.MiddleCenter)
        {
            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = spacing;
            layout.childAlignment = alignment;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.padding = new RectOffset(padLeft, padRight, padTop, padBottom);
        }

        /// Sets the padding of a given LayoutGroup.
        /// <param name="layoutGroup">The LayoutGroup for which the padding will be set.</param>
        /// <param name="left">The left padding value.</param>
        /// <param name="right">The right padding value.</param>
        /// <param name="top">The top padding value.</param>
        /// <param name="bottom">The bottom padding value.</param>
        public static void SetLayoutGroupPadding(LayoutGroup layoutGroup, int left, int right, int top, int bottom)
        {
            layoutGroup.padding = new RectOffset(left, right, top, bottom);
        }


        /// Binds an action to a button and updates its label text.
        /// <param name="btn">The button to which the action will be bound.</param>
        /// <param name="label">The text label associated with the button.</param>
        /// <param name="text">The text to set as the label of the button.</param>
        /// <param name="callback">The action that will be executed when the button is clicked.</param>
        public static void BindAcceptButton(Button btn, Text label, string text, UnityAction callback)
        {
            label.text = text;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(callback);
        }
    }
}

        /// <summary>
        /// Represents a handler that encapsulates a callback action to be invoked when a click event occurs.
        /// </summary>
        /// <remarks>
        /// This class provides a mechanism to handle and execute logic when a click event is triggered.
        /// It associates an action defined by a UnityAction delegate with the click event.
        /// </remarks>
#pragma warning disable S3903
        public class ClickHandler
#pragma warning restore S3903
        {
            /// <summary>
            /// A private field that stores the UnityAction delegate to be executed when a specific UI interaction or click event occurs.
            /// </summary>
            private readonly UnityAction _callback;

            /// Represents a handler that encapsulates a callback action to be invoked when a click event occurs.
            /// <param name="callback">The UnityAction delegate to be executed when the click event is triggered.</param>
            public ClickHandler(UnityAction callback)
            {
                _callback = callback;
            }

            /// Invokes the callback action associated with a click event.
            /// <remarks>
            /// Executes the UnityAction delegate provided during the creation of the ClickHandler instance.
            /// This method is used to process and handle click events associated with the handler.
            /// </remarks>
            public void OnClick()
            {
                _callback.Invoke();
            }
        }
