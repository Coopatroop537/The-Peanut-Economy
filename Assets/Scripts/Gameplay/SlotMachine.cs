using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour
{
    // =========================
    // Singleton
    // =========================
    private static SlotMachine _instance;

    public static SlotMachine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindAnyObjectByType<SlotMachine>();
            }
            return _instance;
        }
    }

    // =========================
    // Serialized Fields
    // =========================
    [SerializeField] private Transform[] reels = new Transform[3];
    [SerializeField] private Image[] reelImages = new Image[3];
    [SerializeField] private Sprite[] symbols = new Sprite[4];
    [SerializeField] private float spinDuration = 2f;
    [SerializeField] private int spinSpeed = 10;

    private bool isSpinning = false;
    private int[] currentReelValues = new int[3];
    private List<Card> activeCards = new List<Card>();

    public delegate void SpinCompleteDelegate(int[] results, int winAmount);
    public event SpinCompleteDelegate OnSpinComplete;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeReels();
    }

    private void InitializeReels()
    {
        for (int i = 0; i < currentReelValues.Length; i++)
        {
            currentReelValues[i] = Random.Range(0, 4);
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

        float elapsedTime = 0f;
        while (elapsedTime < spinDuration)
        {
            for (int i = 0; i < reels.Length; i++)
            {
                currentReelValues[i] = (currentReelValues[i] + spinSpeed) % 4;
                UpdateReelDisplay(i, currentReelValues[i]);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < currentReelValues.Length; i++)
        {
            currentReelValues[i] = Random.Range(0, 4);
            UpdateReelDisplay(i, currentReelValues[i]);
        }

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
        if (currentReelValues[0] == currentReelValues[1] &&
            currentReelValues[1] == currentReelValues[2])
        {
            return betAmount * 3;
        }

        if (currentReelValues[0] == currentReelValues[1] ||
            currentReelValues[1] == currentReelValues[2])
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
        if (reelImages[reelIndex] != null && value >= 0 && value < symbols.Length)
        {
            reelImages[reelIndex].sprite = symbols[value];
        }
    }

    public bool IsSpinning => isSpinning;
    public int[] CurrentReelValues => currentReelValues;
}
