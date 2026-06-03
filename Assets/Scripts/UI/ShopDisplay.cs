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
        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.OnShopRefreshed += DisplayShopCards;
            ShopManager.Instance.OnCardPurchased += OnCardPurchased;
        }
        
        if (refreshButton != null)
        {
            refreshButton.onClick.AddListener(() => ShopManager.Instance.RefreshShop());
        }
    }

    private void DisplayShopCards(List<Card> cards)
    {
        // Null check for shopContent
        if (shopContent == null)
        {
            Debug.LogError("Shop Content is not assigned!");
            return;
        }

        if (shopCardPrefab == null)
        {
            Debug.LogError("Shop Card Prefab is not assigned!");
            return;
        }

        // Clear existing cards from display - store children first to avoid iteration issues
        List<Transform> childrenToDestroy = new List<Transform>();
        foreach (Transform child in shopContent)
        {
            childrenToDestroy.Add(child);
        }
        foreach (Transform child in childrenToDestroy)
        {
            Destroy(child.gameObject);
        }

        // Create UI for each card
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            GameObject cardUI = Instantiate(shopCardPrefab, shopContent);
            
            // Setup card display
            Transform cardNameObj = cardUI.transform.Find("CardName");
            Transform cardDescObj = cardUI.transform.Find("CardDescription");
            Transform cardCostObj = cardUI.transform.Find("CardCost");
            Transform buyButtonObj = cardUI.transform.Find("BuyButton");

            if (cardNameObj != null)
            {
                TextMeshProUGUI cardNameText = cardNameObj.GetComponent<TextMeshProUGUI>();
                if (cardNameText != null)
                {
                    cardNameText.text = card.cardName;
                    cardNameText.fontSize = 20;
                    cardNameText.alignment = TextAlignmentOptions.Center;
                }
                
                LayoutElement nameLayout = cardNameObj.GetComponent<LayoutElement>();
                if (nameLayout == null) nameLayout = cardNameObj.gameObject.AddComponent<LayoutElement>();
                nameLayout.preferredHeight = 35;
            }

            if (cardDescObj != null)
            {
                TextMeshProUGUI cardDescText = cardDescObj.GetComponent<TextMeshProUGUI>();
                if (cardDescText != null)
                {
                    cardDescText.text = card.description;
                    cardDescText.fontSize = 12;
                    cardDescText.alignment = TextAlignmentOptions.Center;
                    cardDescText.wordWrappingRatios = 0.5f;
                }
                
                LayoutElement descLayout = cardDescObj.GetComponent<LayoutElement>();
                if (descLayout == null) descLayout = cardDescObj.gameObject.AddComponent<LayoutElement>();
                descLayout.preferredHeight = 50;
            }

            if (cardCostObj != null)
            {
                TextMeshProUGUI cardCostText = cardCostObj.GetComponent<TextMeshProUGUI>();
                if (cardCostText != null)
                {
                    cardCostText.text = "Cost: " + card.cost + " peanuts";
                    cardCostText.fontSize = 14;
                    cardCostText.alignment = TextAlignmentOptions.Center;
                }
                
                LayoutElement costLayout = cardCostObj.GetComponent<LayoutElement>();
                if (costLayout == null) costLayout = cardCostObj.gameObject.AddComponent<LayoutElement>();
                costLayout.preferredHeight = 25;
            }

            if (buyButtonObj != null)
            {
                Button buyButton = buyButtonObj.GetComponent<Button>();
                if (buyButton != null)
                {
                    int slotIndex = i;
                    buyButton.onClick.AddListener(() => OnBuyButtonClicked(slotIndex));
                }

                // Set button text
                TextMeshProUGUI buttonText = buyButtonObj.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = "Buy";
                    buttonText.fontSize = 16;
                }
                
                LayoutElement buttonLayout = buyButtonObj.GetComponent<LayoutElement>();
                if (buttonLayout == null) buttonLayout = buyButtonObj.gameObject.AddComponent<LayoutElement>();
                buttonLayout.preferredHeight = 40;
            }
        }
    }

    private void OnBuyButtonClicked(int slotIndex)
    {
        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.PurchaseCard(slotIndex);
        }
    }

    private void OnCardPurchased(Card card)
    {
        // Add the card to the slot machine's active cards
        SlotMachine slotMachine = FindObjectOfType<SlotMachine>();
        if (slotMachine != null)
        {
            slotMachine.AddActiveCard(card);
            Debug.Log($"Card purchased: {card.cardName}");
        }
        else
        {
            Debug.LogError("SlotMachine not found in scene!");
        }
    }
}
