using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        // Set canvas size
        RectTransform canvasRect = canvasObj.GetComponent<RectTransform>();
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;

        // Create Background Image
        GameObject bgObj = new GameObject("Background");
        bgObj.transform.SetParent(canvasObj.transform);
        Image bgImage = bgObj.AddComponent<Image>();
        bgImage.color = new Color(0.1f, 0.1f, 0.15f, 1f);
        
        RectTransform bgRect = bgObj.GetComponent<RectTransform>();
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
        titleRect.anchoredPosition = new Vector2(0, 300);
        titleRect.sizeDelta = new Vector2(1000, 200);

        // Create Play Button
        playButton = CreateButton("Play", canvasObj.transform, new Vector2(0, 50), new Vector2(300, 100));
        playButton.onClick.AddListener(PlayGame);

        // Create Quit Button
        quitButton = CreateButton("Quit", canvasObj.transform, new Vector2(0, -100), new Vector2(300, 100));
        quitButton.onClick.AddListener(QuitGame);
    }

    private Button CreateButton(string buttonText, Transform parent, Vector2 position, Vector2 size)
    {
        // Create Button Container
        GameObject buttonObj = new GameObject($"{buttonText}Button");
        buttonObj.transform.SetParent(parent);
        
        Button button = buttonObj.AddComponent<Button>();
        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.6f, 0.8f, 1f);

        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = position;
        buttonRect.sizeDelta = size;

        // Create Text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);
        
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = buttonText;
        text.fontSize = 40;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        // Add Button Transitions
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.2f, 0.6f, 0.8f, 1f);
        colors.highlightedColor = new Color(0.3f, 0.7f, 0.9f, 1f);
        colors.pressedColor = new Color(0.1f, 0.5f, 0.7f, 1f);
        button.colors = colors;

        return button;
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
