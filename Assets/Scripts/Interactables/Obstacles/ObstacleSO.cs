using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObject/Obstacle")]
public class ObstacleSO : ScriptableObject
{
    public string obstacleName = "Null Obstacle";
    public bool killPlayer = false;
}
