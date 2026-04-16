using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int minBet = 10;
    [SerializeField] private int maxBet = 100;

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

    public int GetMinBet()
    {
        return minBet;
    }

    public int GetMaxBet()
    {
        return maxBet;
    }

    public bool IsValidBet(int amount)
    {
        return amount >= minBet && amount <= maxBet && amount <= PeanutManager.Instance.GetPeanuts();
    }
}