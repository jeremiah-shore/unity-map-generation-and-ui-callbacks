using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnPlanner : MonoBehaviour {

    private const float CEILING = 0.5f;

    [System.Serializable]
    private class GeneratedObject {
        [SerializeField] public GameObject prefab;
        [SerializeField] [Range(0, CEILING)] public float density;
    }
    [SerializeField] GeneratedObject[] objectsToGenerate;

    private class MapPosition {
        public int x, y;
    }

    private GameMapController mapController;
    private GameObject[,] spawnPlanGrid;

    /********************************************************************/

    void Start () {
        mapController = GetComponent<GameMapController>();
    }  

    private Dictionary<GameObject, int> calculateSpawnCounts() {
        Dictionary<GameObject, int> spawnCount = new Dictionary<GameObject, int>();
        foreach (GeneratedObject genObj in objectsToGenerate) {
            GameObject prefab = genObj.prefab;
            int count = calcPrefabSpawnAmt(genObj);
            spawnCount.Add(prefab, count);
        }
        return spawnCount;
    }

    private int calcPrefabSpawnAmt(GeneratedObject prefab) {
        int amtToSpawn = 0;
        for (int i = 0; i < mapController.getMapTileCount(); i++)
            if (Random.Range(0, CEILING) < prefab.density)
                amtToSpawn++;
        return amtToSpawn;
    }

    public void fillSpawnPlanGrid(GameObject[,] spawnPlanGrid) {
        this.spawnPlanGrid = spawnPlanGrid;
        Dictionary<GameObject, int> spawnCounts = calculateSpawnCounts();
        foreach (KeyValuePair<GameObject, int> entry in spawnCounts) {
            for(int i = 0; i < entry.Value; i++) {
                MapPosition pos = getEmptySpawnPosition();
                spawnPlanGrid[pos.x, pos.y] = entry.Key;
            }
        }
    }

    private MapPosition getEmptySpawnPosition() {
        MapPosition pos;
        GameObject prefabInGrid;
        do {
            pos = getRandomMapPosition();
            prefabInGrid = spawnPlanGrid[pos.x, pos.y];
        } while (prefabInGrid != null);
        return pos;
    }

    private MapPosition getRandomMapPosition() {
        MapPosition pos = new MapPosition();
        pos.x = Random.Range(0, mapController.getMapWidth());
        pos.y = Random.Range(0, mapController.getMapHeight());
        return pos;
    }

}

