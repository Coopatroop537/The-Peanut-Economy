using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [SerializeField] private int shopSlots = 3;
    private List<Card> currentShopCards = new List<Card>();

    public delegate void ShopRefreshedDelegate(List<Card> cards);
    public event ShopRefreshedDelegate OnShopRefreshed;

    public delegate void CardPurchasedDelegate(Card card);
    public event CardPurchasedDelegate OnCardPurchased;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RefreshShop();
    }

    public void RefreshShop()
    {
        currentShopCards.Clear();
        currentShopCards.AddRange(CardPool.Instance.GetRandomCards(shopSlots));
        OnShopRefreshed?.Invoke(currentShopCards);
    }

    public bool PurchaseCard(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= currentShopCards.Count)
        {
            return false;
        }

        Card card = currentShopCards[slotIndex];

        if (PeanutManager.Instance.SpendPeanuts(card.cost))
        {
            OnCardPurchased?.Invoke(card);
            currentShopCards.RemoveAt(slotIndex);
            
            // Add a new random card to replace the purchased one
            currentShopCards.Insert(slotIndex, CardPool.Instance.GetRandomCard());
            OnShopRefreshed?.Invoke(currentShopCards);
            
            return true;
        }

        return false;
    }

    public List<Card> GetCurrentShopCards()
    {
        return new List<Card>(currentShopCards);
    }
}