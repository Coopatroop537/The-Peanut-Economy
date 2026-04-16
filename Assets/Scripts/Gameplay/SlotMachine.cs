using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Transform[] reels = new Transform[3];
    [SerializeField] private float spinDuration = 2f;
    [SerializeField] private int spinSpeed = 10;

    private bool isSpinning = false;
    private int[] currentReelValues = new int[3];
    private List<Card> activeCards = new List<Card>();

    public delegate void SpinCompleteDelegate(int[] results, int winAmount);
    public event SpinCompleteDelegate OnSpinComplete;

    private void Start()
    {
        InitializeReels();
    }

    private void InitializeReels()
    {
        for (int i = 0; i < currentReelValues.Length; i++)
        {
            currentReelValues[i] = Random.Range(0, 10);
        }
    }

    public void Spin(int betAmount)
    {
        if (isSpinning) return;

        if (!PeanutManager.Instance.SpendPeanuts(betAmount))
        {
            Debug.Log("Not enough peanuts!");
            return;
        }

        StartCoroutine(SpinRoutine(betAmount));
    }

    private IEnumerator SpinRoutine(int betAmount)
    {
        isSpinning = true;

        // Spin animation
        float elapsedTime = 0f;
        while (elapsedTime < spinDuration)
        {
            for (int i = 0; i < reels.Length; i++)
            {
                currentReelValues[i] = (currentReelValues[i] + spinSpeed) % 10;
                UpdateReelDisplay(i, currentReelValues[i]);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Final values
        for (int i = 0; i < currentReelValues.Length; i++)
        {
            currentReelValues[i] = Random.Range(0, 10);
        }

        // Calculate winnings
        int winAmount = CalculateWinnings(betAmount);
        ApplyCardEffects(ref winAmount);

        if (winAmount > 0)
        {
            PeanutManager.Instance.AddPeanuts(winAmount);
        }

        isSpinning = false;
        OnSpinComplete?.Invoke(currentReelValues, winAmount);
    }

    private int CalculateWinnings(int betAmount)
    {
        // Three of a kind = 3x bet
        if (currentReelValues[0] == currentReelValues[1] && currentReelValues[1] == currentReelValues[2])
        {
            return betAmount * 3;
        }

        // Two of a kind = 1.5x bet
        if (currentReelValues[0] == currentReelValues[1] || currentReelValues[1] == currentReelValues[2])
        {
            return (int)(betAmount * 1.5f);
        }

        return 0;
    }

    private void ApplyCardEffects(ref int winAmount)
    {
        foreach (Card card in activeCards)
        {
            switch (card.effect)
            {
                case CardEffect.MultiplierBonus:
                    winAmount = (int)(winAmount * 1.25f);
                    break;
                case CardEffect.PeanutMultiplier:
                    winAmount *= 2;
                    break;
                case CardEffect.DoubleOrNothing:
                    winAmount *= 2;
                    break;
            }
        }
    }

    public void AddActiveCard(Card card)
    {
        activeCards.Add(card);
    }

    public void RemoveActiveCard(Card card)
    {
        activeCards.Remove(card);
    }

    private void UpdateReelDisplay(int reelIndex, int value)
    {
        // Update UI to show current reel value
        // This will be connected to UI elements
    }

    public bool IsSpinning => isSpinning;
    public int[] CurrentReelValues => currentReelValues;
}