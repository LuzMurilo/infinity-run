using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, ICollidable
{
    [SerializeField] private Vector2 lengthLimits;
    [SerializeField] private Vector2 angleLimits;
    [SerializeField] private Transform ground;
    [SerializeField] private List<LaneController> lanes;
    public List<LaneController> Lanes {
        get {return lanes;}
        private set {}
    }
    public Transform start;
    public Transform finish;
    public Vector3 direction;
    public float length {get; private set;}
    public float angle {get; private set;}
    private BlockSpawner blockSpawner;

    private void Awake() 
    {
        length = Mathf.Floor(Random.Range(lengthLimits.x, lengthLimits.y));
        angle = Mathf.Floor(Random.Range(angleLimits.x, angleLimits.y));
        if (angle % 2 != 0) angle = 0.0f;
        ground.localScale = new Vector3(ground.localScale.x, ground.localScale.y, length);
        transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
        ground.localPosition = new Vector3(0, 0, length / 2);
        finish.localPosition = new Vector3(0, 0, length);

        direction = (finish.position - start.position).normalized;
        
        if (transform.parent.TryGetComponent<BlockSpawner>(out blockSpawner))
        {
            blockSpawner.ChangeNextSpawnPositon(finish.position);
        }
    }

    public void OnCollideWithPlayer(PlayerManager player)
    {
        player.EnteredNewBlock(this);
        blockSpawner.SpawnNewBlock();
    }
}
