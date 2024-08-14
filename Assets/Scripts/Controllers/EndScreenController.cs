using UnityEngine;
using TMPro;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    public void Setup(int coins)
    {
        if (coinsText == null) return;

        coinsText.text = coins.ToString() + " COINS";
    }

    public void RestartButtonClick()
    {
        GameManager.Singleton.RestartGame();
    }
}
