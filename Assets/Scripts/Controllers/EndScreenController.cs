using UnityEngine;
using TMPro;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text totalCoinsText;

    public void Setup(int coins, int totalCoins)
    {
        if (coinsText == null) return;

        coinsText.text = "+ " + coins.ToString() + " COINS";
        totalCoinsText.text = "TOTAL: " + totalCoins.ToString() + " COINS";
    }

    public void RestartButtonClick()
    {
        GameManager.Singleton.RestartGame();
    }
}
