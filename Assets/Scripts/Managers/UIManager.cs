using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton {get; private set;}
    [SerializeField] private TMP_Text coinDisplay;
    [SerializeField] private List<GameObject> hearts;

    private void Awake() {
        if (UIManager.Singleton != null)
        {
            Destroy(this);
            return;
        }
        UIManager.Singleton = this;

    }

    public void SetCoinDisplayNumber(int coins)
    {
        coinDisplay.text = coins.ToString();
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
}
