using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour, ICollidable
{
    [SerializeField] private Vector2 lengthLimits;
    [SerializeField] private Vector2 angleLimits;
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
        lanesDict = new Dictionary<int, LaneController>();
        lanes.ForEach(lane => lanesDict.Add(lane.index, lane));
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
        for (int i = Mathf.FloorToInt(length/minObstacleDistance) - 1; i > 0; i--)
        {
            InstantiateInteractable(i);
        }
    }

    private void InstantiateInteractable(int positionInBlock)
    {
        if (interactables == null || interactables.Count == 0) return;

        GameObject prefabToSpawn = interactables[Random.Range(0, interactables.Count)];

        int[] possibleLanes = prefabToSpawn.GetComponent<Interactable>().Lanes;
        int laneToSpawn = possibleLanes[Random.Range(0, possibleLanes.Length)];

        float positionX = lanesDict[laneToSpawn].transform.position.x;
        float positionY = transform.position.y + 0.5f;
        float positionZ = transform.position.z + (positionInBlock * minObstacleDistance);
        if (angle != 0.0f)
        {
            positionY -= Mathf.Tan(Mathf.Deg2Rad * angle) * positionInBlock * minObstacleDistance;
        }

        Vector3 spawnPosition = new Vector3(positionX, positionY, positionZ);
        Instantiate(prefabToSpawn, spawnPosition, transform.rotation, transform);
    }
}
