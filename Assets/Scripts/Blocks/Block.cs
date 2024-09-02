using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class Block : MonoBehaviour, ICollidable
{
    [SerializeField] private Vector2 lengthLimits;
    [SerializeField] private Vector2 angleLimits;
    [SerializeField] private float maxAngleDiference;
    [SerializeField] private float minObstacleDistance;
    [SerializeField] private Transform ground;
    [SerializeField] private List<LaneController> lanes;
    [SerializeField] private Dictionary<int, LaneController> lanesDict;
    public Dictionary<int, LaneController> Lanes {
        get {return lanesDict;}
        private set {}
    }
    [SerializeField] private List<GameObject> interactables;
    public Transform start;
    public Transform finish;
    public Vector3 direction;
    public float length {get; private set;}
    public float angle {get; private set;}
    private BlockSpawner blockSpawner;

    private void Awake() 
    {
        if (!transform.parent.TryGetComponent<BlockSpawner>(out blockSpawner))
        {
            Debug.LogError("["+ gameObject.name +"] Failed to get Block Spawner Parent!");
            return;
        }

        lanesDict = new Dictionary<int, LaneController>();
        lanes.ForEach(lane => lanesDict.Add(lane.index, lane));

        length = Mathf.Floor(Random.Range(lengthLimits.x, lengthLimits.y));

        CalculateAngle();

        ground.localScale = new Vector3(ground.localScale.x, ground.localScale.y, length);
        transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
        ground.localPosition = new Vector3(0, 0, length / 2);
        finish.localPosition = new Vector3(0, 0, length);

        direction = (finish.position - start.position).normalized;
        
        
        blockSpawner.ChangeNextSpawnPositon(finish.position);

        SpawnInteractables();
    }

    public void OnCollideWithPlayer(PlayerManager player)
    {
        player.EnteredNewBlock(this);
        blockSpawner.SpawnNewBlock();
    }

    private void SpawnInteractables()
    {
        if (interactables == null || interactables.Count == 0) return;
        InstantiateInteractable(0);
    }

    private void InstantiateInteractable(float positionInBlock)
    {
        if (interactables == null || interactables.Count == 0) return;

        Interactable prefabToSpawn = interactables[Random.Range(0, interactables.Count)].GetComponent<Interactable>();

        int[] possibleLanes = prefabToSpawn.Lanes;
        int laneToSpawn = possibleLanes[Random.Range(0, possibleLanes.Length)];

        float positionX = lanesDict[laneToSpawn].transform.position.x;
        float positionY = transform.position.y + 0.5f;
        float positionZ = transform.position.z + positionInBlock;
        if (angle != 0.0f)
        {
            positionY -= Mathf.Tan(Mathf.Deg2Rad * angle) * positionInBlock;
        }

        Vector3 spawnPosition = new Vector3(positionX, positionY, positionZ);
        Instantiate(prefabToSpawn, spawnPosition, transform.rotation, transform);

        if (positionInBlock + prefabToSpawn.Length + (2*minObstacleDistance) < length)
        {
            InstantiateInteractable(positionInBlock + prefabToSpawn.Length + minObstacleDistance);
        }
    }

    private void CalculateAngle()
    {
        if (angleLimits.magnitude == 0.0f)
        {
            angle = angleLimits.x;
            return;
        }
        angle = Mathf.Floor(Random.Range(angleLimits.x, angleLimits.y));
        if (angle % 2 != 0) angle = 0.0f;
        if (blockSpawner.blocks.Count <= 0) return;
        Block lastBlock = blockSpawner.blocks.Last(block => block != this);
        if (lastBlock != null)
        {
            float angleDiference = angle - lastBlock.angle;
            if (angleDiference > maxAngleDiference)
            {
                angle = lastBlock.angle + maxAngleDiference;
            }
            else if (angleDiference < -1f * maxAngleDiference)
            {
                angle = lastBlock.angle - maxAngleDiference;
            }
        }
    }
}
