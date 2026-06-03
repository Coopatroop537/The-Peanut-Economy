using UnityEngine;

public class PeanutManager : MonoBehaviour
{
    public static PeanutManager Instance { get; private set; }

    [SerializeField] private int startingPeanuts = 100;
    private int currentPeanuts;
    private int debt = 0;

    public delegate void PeanutChangedDelegate(int newAmount);
    public event PeanutChangedDelegate OnPeanutChanged;

    public delegate void DebtChangedDelegate(int debtAmount);
    public event DebtChangedDelegate OnDebtChanged;

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
        debt = 0;
    }

    public int GetPeanuts()
    {
        return currentPeanuts;
    }

    public int GetDebt()
    {
        return debt;
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
        
        // Pay off debt first
        if (debt > 0)
        {
            int paymentAmount = Mathf.Min(currentPeanuts, debt);
            debt -= paymentAmount;
            currentPeanuts -= paymentAmount;
            OnDebtChanged?.Invoke(debt);
        }
        
        OnPeanutChanged?.Invoke(currentPeanuts);
    }

    public bool BorrowPeanuts(int amount)
    {
        // Can only borrow if peanuts <= 5
        if (currentPeanuts <= 5)
        {
            currentPeanuts += amount;
            debt += amount;
            OnPeanutChanged?.Invoke(currentPeanuts);
            OnDebtChanged?.Invoke(debt);
            Debug.Log($"Borrowed {amount} peanuts. Total debt: {debt}");
            return true;
        }
        return false;
    }

    public void SetPeanuts(int amount)
    {
        currentPeanuts = amount;
        OnPeanutChanged?.Invoke(currentPeanuts);
    }
}
