using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapController : MonoBehaviour {

    public const float POSITION_OFFSET = 0.5f;

    [SerializeField] private int mapHeight;
    [SerializeField] private int mapWidth;
    [SerializeField] Camera mainCamera;

    private GameObject[,] spawnPlanGrid;
    private List<GameObject> spawnedGameObjectList;

    private void moveCamera() {
        float centerX = (mapWidth / 2) + POSITION_OFFSET;
        float centerY = (mapHeight / 2) + POSITION_OFFSET;
        Vector3 newCameraPos = new Vector3(centerX, centerY, mainCamera.transform.position.z);
        mainCamera.transform.position = newCameraPos;
    }

    public void generateObjects() {
        moveCamera();
        clearGameObjectsInEditor();
        spawnedGameObjectList = new List<GameObject>();
        spawnPlanGrid = new GameObject[mapWidth, mapHeight];

        MapSpawnPlanner planner =  GetComponentsInChildren<MapSpawnPlanner>()[0];
        planner.fillSpawnPlanGrid(spawnPlanGrid);

        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                GameObject prefab = spawnPlanGrid[x, y];
                if (prefab != null) {
                    Vector2 position = new Vector2(x + POSITION_OFFSET, y + POSITION_OFFSET);
                    GameObject instance = Instantiate(prefab, position, Quaternion.identity);
                    instance.GetComponent<SpriteRenderer>().sortingOrder = mapHeight - y;
                    spawnedGameObjectList.Add(instance);
                }
            }
        }
    }

    public void clearGameObjectsInEditor() {
        if (spawnedGameObjectList == null) return;
        int count = 0;
        foreach (GameObject gobj in spawnedGameObjectList) {
            DestroyImmediate(gobj);
            count++;
        }
        Debug.Log(string.Format("destroyed {0} instances in {1}", count, spawnedGameObjectList.GetType()));
        spawnedGameObjectList = null;
    }

    public int getMapHeight() {
        return mapHeight;
    }

    public int getMapWidth() {
        return mapWidth;
    }

    public int getMapTileCount() {
        return mapHeight * mapWidth;
    }

}
