using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    private Canvas canvas;
    private Button playButton;
    private Button quitButton;
    private TextMeshProUGUI titleText;

    private void Start()
    {
        CreateMainMenu();
    }

    private void CreateMainMenu()
    {
        // Create EventSystem (REQUIRED for UI interaction)
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<StandaloneInputModule>();
        }

        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        // Set canvas size
        RectTransform canvasRect = canvasObj.GetComponent<RectTransform>();
        canvasRect.anchorMin = Vector2.zero;
        canvasRect.anchorMax = Vector2.one;
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;

        // Create Background Image
        GameObject bgObj = new GameObject("Background");
        bgObj.transform.SetParent(canvasObj.transform);
        Image bgImage = bgObj.AddComponent<Image>();
        bgImage.color = new Color(0.1f, 0.1f, 0.15f, 1f);
        
        RectTransform bgRect = bgObj.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        // Create Title
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(canvasObj.transform);
        titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = "The Peanut Economy";
        titleText.fontSize = 80;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = Color.white;

        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.5f, 0.5f);
        titleRect.anchorMax = new Vector2(0.5f, 0.5f);
        titleRect.anchoredPosition = new Vector2(0, 300);
        titleRect.sizeDelta = new Vector2(1000, 200);

        // Create Play Button
        playButton = CreateButton("Play", canvasObj.transform, new Vector2(-200, 80));
        playButton.onClick.AddListener(PlayGame);

        // Create Quit Button
        quitButton = CreateButton("Quit", canvasObj.transform, new Vector2(200, 80));
        quitButton.onClick.AddListener(QuitGame);

        Debug.Log("Main menu created successfully!");
        Debug.Log($"Play button listener count: {playButton.onClick.GetPersistentEventCount()}");
        Debug.Log($"Quit button listener count: {quitButton.onClick.GetPersistentEventCount()}");
    }

    private Button CreateButton(string buttonText, Transform parent, Vector2 position)
    {
        // Create Button Container
        GameObject buttonObj = new GameObject($"{buttonText}Button");
        buttonObj.transform.SetParent(parent);
        
        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.6f, 0.8f, 1f);

        Button button = buttonObj.AddComponent<Button>();
        button.targetGraphic = buttonImage;
        
        // Setup proper navigation
        Navigation nav = button.navigation;
        nav.mode = Navigation.Mode.None;
        button.navigation = nav;

        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.anchoredPosition = position;
        buttonRect.sizeDelta = new Vector2(250, 80);

        // Create Text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);
        
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = buttonText;
        text.fontSize = 50;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        // Add Button Transitions
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.2f, 0.6f, 0.8f, 1f);
        colors.highlightedColor = new Color(0.3f, 0.7f, 0.9f, 1f);
        colors.pressedColor = new Color(0.1f, 0.5f, 0.7f, 1f);
        colors.selectedColor = new Color(0.3f, 0.7f, 0.9f, 1f);
        button.colors = colors;

        Debug.Log($"{buttonText} button created at {position}");
        return button;
    }

    private void PlayGame()
    {
        Debug.Log("Play button clicked!");
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
