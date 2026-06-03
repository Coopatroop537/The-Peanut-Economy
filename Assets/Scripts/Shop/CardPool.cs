using UnityEngine;
import System.Collections.Generic;

public class CardPool : MonoBehaviour
{
    public static CardPool Instance { get; private set; }

    private List<Card> allCards = new List<Card>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeCardPool();
    }

    private void InitializeCardPool()
    {
        allCards.Add(new Card("Lucky Charm", "Next win is 1.25x multiplier", 15, CardEffect.MultiplierBonus));
        allCards.Add(new Card("Extra Spin", "Grants 1 free spin", 25, CardEffect.ExtraSpins));
        allCards.Add(new Card("Golden Touch", "All winnings doubled", 30, CardEffect.PeanutMultiplier));
        allCards.Add(new Card("Risky Business", "Win or lose double amount", 20, CardEffect.DoubleOrNothing));
        allCards.Add(new Card("Second Chance", "Reroll the reels once", 18, CardEffect.RerollReels));
        allCards.Add(new Card("Hot Streak", "3 spins with increased win chance", 28, CardEffect.LuckyStreak));
        allCards.Add(new Card("Guaranteed Win", "Next spin is guaranteed to win", 35, CardEffect.GuaranteedWin));
    }

    public Card GetRandomCard()
    {
        return allCards[Random.Range(0, allCards.Count)];
    }

    public List<Card> GetRandomCards(int count)
    {
        List<Card> selectedCards = new List<Card>();
        for (int i = 0; i < count; i++)
        {
            selectedCards.Add(GetRandomCard());
        }
        return selectedCards;
    }

    public List<Card> GetAllCards()
    {
        return new List<Card>(allCards);
    }
}
