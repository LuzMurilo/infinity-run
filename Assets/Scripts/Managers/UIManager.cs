using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton {get; private set;}
    [SerializeField] private TMP_Text coinDisplay;

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
}
