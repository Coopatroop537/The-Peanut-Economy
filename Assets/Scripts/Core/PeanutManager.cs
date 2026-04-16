using UnityEngine;

public class PeanutManager : MonoBehaviour
{
    public static PeanutManager Instance { get; private set; }

    [SerializeField] private int startingPeanuts = 100;
    private int currentPeanuts;

    public delegate void PeanutChangedDelegate(int newAmount);
    public event PeanutChangedDelegate OnPeanutChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        currentPeanuts = startingPeanuts;
    }

    public int GetPeanuts()
    {
        return currentPeanuts;
    }

    public bool SpendPeanuts(int amount)
    {
        if (currentPeanuts >= amount)
        {
            currentPeanuts -= amount;
            OnPeanutChanged?.Invoke(currentPeanuts);
            return true;
        }
        return false;
    }

    public void AddPeanuts(int amount)
    {
        currentPeanuts += amount;
        OnPeanutChanged?.Invoke(currentPeanuts);
    }

    public void SetPeanuts(int amount)
    {
        currentPeanuts = amount;
        OnPeanutChanged?.Invoke(currentPeanuts);
    }
}