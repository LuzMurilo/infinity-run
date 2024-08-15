using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton {get; private set;}
    [SerializeField] private TMP_Text coinDisplay;
    [SerializeField] private TMP_Text distanceDisplay;
    [SerializeField] private List<GameObject> hearts;
    [SerializeField] private MainMenuController mainMenu;
    [SerializeField] private EndScreenController endScreen;

    private void Awake() {
        if (UIManager.Singleton != null)
        {
            Destroy(this);
            return;
        }
        UIManager.Singleton = this;

        mainMenu.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    public void SetCoinDisplayNumber(int coins)
    {
        coinDisplay.text = coins.ToString();
    }

    public void SetDistanceDisplayNumber(int distance)
    {
        if (distanceDisplay.text == distance.ToString() + "m") return;
        distanceDisplay.text = distance.ToString() + "m";
    }

    public void SetHeartsDisplayed(int number)
    {
        if (number > hearts.Count)
        {
            Debug.LogError("[UI] not enough heart objects on List!");
            return;
        }
        hearts.ForEach(heart => heart.SetActive(false));
        for (int i = 0; i < number; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public void ShowGameOverScreen(PlayerManager player)
    {
        endScreen.Setup(player.totalCoins);
        StartCoroutine(GameOverScreenDelay());
    }

    private IEnumerator GameOverScreenDelay()
    {
        yield return new WaitForSeconds(2f);
        endScreen.gameObject.SetActive(true);
    }
}
