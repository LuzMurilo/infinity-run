using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pick-Up", menuName = "ScriptableObject/Pick-Up")]
public class PickUpSO : ScriptableObject
{
    public string pickUpName = "Null Pick-Up";
    public int value = 0;
}