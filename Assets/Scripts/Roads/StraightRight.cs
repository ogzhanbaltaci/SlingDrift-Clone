using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRight : MonoBehaviour
{
    [SerializeField] List<GameObject> roads = new List<GameObject>();
    
    public Vector3 position;
    RoadGenerator roadGenerator;

    void Start()
    {
        roadGenerator = GameManager.instance.roadGenerator;
        BuildRoad();
    }

    public void BuildRoad()
    {
        int count = roads.Count;
        int index = Random.Range(0, count);
        GameObject pickedRoad = roads[index];
        position = transform.Find(GameConstants.FinishPos).position;
        if(roadGenerator.counter < roadGenerator.builtRoadCounter)
        {
            GameObject road = Instantiate(pickedRoad, position, Quaternion.identity);
            roadGenerator.builtRoads.Add(road);
            roadGenerator.counter++;
            roadGenerator.levelCounter++;
        }
            
    }

}
