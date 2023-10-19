using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] CarMovementController carMovementController;
    public List<GameObject> builtRoads = new List<GameObject>();
    public int counter = 0;
    public int builtRoadCounter = 11;
    public int levelCounter = 0;
    int levelUpCap = 13;
    GameObject lastRoad;


    void Update()
    {
        UpdateBuiltRoadCounter();
        ContinueBuilding();
    }

    private void UpdateBuiltRoadCounter()
    {
        if (builtRoads.Count > 10)
            lastRoad = builtRoads[builtRoadCounter - 1]; //Finds the last roadPart on the list

        if (levelCounter > levelUpCap)
            levelCounter = 0;
    }

    private void ContinueBuilding() //Generates a new roadPart based on the shape of the last roadPart in the list
    {
        if (ShouldContinueBuilding())
        {
            if (lastRoad.tag == GameConstants.LeftTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<LeftTurn>().BuildRoad();
            }
            else if (lastRoad.tag == GameConstants.RightTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<RightTurn>().BuildRoad();
            }
            else if (lastRoad.tag == GameConstants.StraightLeft)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<StraightLeft>().BuildRoad();
            }
            else if (lastRoad.tag == GameConstants.StraightRight)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<StraightRight>().BuildRoad();
            }
            else
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<StraightUp>().BuildRoad();
            }
        }
        else if (levelCounter == levelUpCap)
        {
            if (lastRoad.tag == GameConstants.LeftTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<LeftTurn>().BuildLevelUpRoad();
            }
            else if (lastRoad.tag == GameConstants.RightTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<RightTurn>().BuildLevelUpRoad();
            }
        }
    }
    private bool ShouldContinueBuilding()
    {
        return carMovementController.roadSeen * 2 - 1 >= builtRoadCounter - 8 && levelCounter < levelUpCap;
    }
}
