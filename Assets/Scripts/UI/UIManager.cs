using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI peanutDisplay;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject spinPanel;

    private void Start()
    {
        PeanutManager.Instance.OnPeanutChanged += UpdatePeanutDisplay;
        SlotMachine.Instance.OnSpinComplete += OnSpinComplete;
        ShopManager.Instance.OnShopRefreshed += OnShopRefreshed;
    }

    private void UpdatePeanutDisplay(int amount)
    {
        if (peanutDisplay != null)
        {
            peanutDisplay.text = "Peanuts: " + amount;
        }
    }

    private void OnSpinComplete(int[] results, int winAmount)
    {
        if (resultText != null)
        {
            if (winAmount > 0)
            {
                resultText.text = "You won " + winAmount + " peanuts!";
            }
            else
            {
                resultText.text = "No match! Better luck next time.";
            }
        }
    }

    private void OnShopRefreshed(System.Collections.Generic.List<Card> cards)
    {
        // This will be expanded to display shop cards dynamically
        Debug.Log("Shop refreshed with " + cards.Count + " new cards");
    }

    public void ToggleShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }
}