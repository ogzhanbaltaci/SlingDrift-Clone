using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Diagnostics.Tracing;

public class LeftTurn : MonoBehaviour
{
    [SerializeField] GameObject straightRoad;
    [SerializeField] GameObject straightRoadFinish;
    public Vector3 position;
    RoadGenerator roadGenerator;

    void Start()
    {
        roadGenerator = GameManager.instance.roadGenerator;
        BuildRoad(); 
    }
    
    public void BuildLevelUpRoad()
    {
        position = transform.Find(GameConstants.FinishPos).position;
        if(roadGenerator.counter < roadGenerator.builtRoadCounter)
        {
            GameObject road = Instantiate(straightRoadFinish, position, Quaternion.identity);
            roadGenerator.builtRoads.Add(road);
            roadGenerator.counter++;
            roadGenerator.levelCounter++;
        }
    }
    
    public void BuildRoad()
    {
        position = transform.Find(GameConstants.FinishPos).position;
        if(roadGenerator.counter < roadGenerator.builtRoadCounter)
        {
            GameObject road = Instantiate(straightRoad, position, Quaternion.identity);
            roadGenerator.builtRoads.Add(road);
            roadGenerator.counter++;
            roadGenerator.levelCounter++;
        }
            
    }
}
