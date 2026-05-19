using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private Transform shopContent; // Parent container for shop cards
    [SerializeField] private GameObject shopCardPrefab; // Prefab for individual shop card UI
    [SerializeField] private Button refreshButton; // Button to refresh shop

    private void Start()
    {
        ShopManager.Instance.OnShopRefreshed += DisplayShopCards;
        
        if (refreshButton != null)
        {
            refreshButton.onClick.AddListener(() => ShopManager.Instance.RefreshShop());
        }
    }

    private void DisplayShopCards(List<Card> cards)
    {
        // Clear existing cards from display
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }

        // Create UI for each card
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            GameObject cardUI = Instantiate(shopCardPrefab, shopContent);
            
            // Setup card display
            TextMeshProUGUI cardNameText = cardUI.transform.Find("CardName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardDescText = cardUI.transform.Find("CardDescription").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardCostText = cardUI.transform.Find("CardCost").GetComponent<TextMeshProUGUI>();
            Button buyButton = cardUI.transform.Find("BuyButton").GetComponent<Button>();

            cardNameText.text = card.cardName;
            cardDescText.text = card.description;
            cardCostText.text = "Cost: " + card.cost + " peanuts";

            int slotIndex = i;
            buyButton.onClick.AddListener(() => OnBuyButtonClicked(slotIndex));
        }
    }

    private void OnBuyButtonClicked(int slotIndex)
    {
        ShopManager.Instance.PurchaseCard(slotIndex);
    }
}
