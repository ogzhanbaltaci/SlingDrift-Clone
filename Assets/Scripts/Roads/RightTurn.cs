using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurn : MonoBehaviour
{
    [SerializeField] GameObject straightRoad;
    [SerializeField] GameObject straightRoadFinish;
    public Vector3 position;


    GameManager gameManager;
    RoadGenerator roadGenerator;
    void Start()
    {
        roadGenerator = FindObjectOfType<RoadGenerator>();
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
