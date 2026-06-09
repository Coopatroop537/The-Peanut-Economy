using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    private Button playButton;
    private Button quitButton;

    private void Start()
    {
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        // Find existing EventSystem
        EventSystem existingEventSystem = FindObjectOfType<EventSystem>();
        if (existingEventSystem == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<EventSystem>();
        }

        // Find Play button in hierarchy
        playButton = GameObject.Find("Canvas/PlayButton").GetComponent<Button>();
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayGame);
            Debug.Log("Play button found and listener added!");
        }
        else
        {
            Debug.LogWarning("Play button not found! Make sure there's a button named 'PlayButton' as a child of Canvas.");
        }

        // Find Quit button in hierarchy
        quitButton = GameObject.Find("Canvas/QuitButton").GetComponent<Button>();
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
            Debug.Log("Quit button found and listener added!");
        }
        else
        {
            Debug.LogWarning("Quit button not found! Make sure there's a button named 'QuitButton' as a child of Canvas.");
        }
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
