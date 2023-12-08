using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObject/Obstacle")]
public class BlockSO : ScriptableObject
{
    public Vector3 direction;
    public float start;
    public float end;
    public List<BlockSO> nextPossible;
}
