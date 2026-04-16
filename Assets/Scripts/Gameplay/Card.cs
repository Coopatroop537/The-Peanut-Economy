using UnityEngine;

[System.Serializable]
public class Card
{
    public string cardName;
    public string description;
    public int cost;
    public CardEffect effect;

    public Card(string name, string desc, int cardCost, CardEffect cardEffect)
    {
        cardName = name;
        description = desc;
        cost = cardCost;
        effect = cardEffect;
    }
}

public enum CardEffect
{
    MultiplierBonus,      // Increases payout multiplier
    ExtraSpins,           // Grants additional free spins
    GuaranteedWin,        // Next spin is guaranteed to win
    DoubleOrNothing,      // Win or lose double amount
    PeanutMultiplier,     // Multiply all earned peanuts
    RerollReels,          // Reroll the current spin result
    LuckyStreak,          // Increased win chance for next 3 spins
}