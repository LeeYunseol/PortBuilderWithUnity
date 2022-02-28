using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DeleteManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public RoadFixer roadFixer;
    public RoadManager roadManager;
    
    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void DeleteRoad(Vector3Int position)
    {
        for(int i=0 ; i<placementManager.transform.childCount ; i++)
        {
            var newPosition = new Vector3Int(Mathf.RoundToInt(placementManager.transform.GetChild(i).position.x), 0, 
                    Mathf.RoundToInt(placementManager.transform.GetChild(i).position.z));
            if((position.x == newPosition.x) && (position.z == newPosition.z))
            {
                Destroy(placementManager.transform.GetChild(i).gameObject);
                placementManager.structureDictionary.Remove(position);
                placementManager.placementGrid[position.x, position.z] = CellType.Empty;
                break;
            }  
        }
        var newPosition1 = position + new Vector3Int(1,0,0);
        var newPosition2 = position + new Vector3Int(-1,0,0);
        var newPosition3 = position + new Vector3Int(0,0,1);
        var newPosition4 = position + new Vector3Int(0,0,-1);

        roadManager.roadPositionsToRecheck.Clear();
        if(roadManager.roadPositionsToRecheck.Contains(newPosition1)) {
			} else {
				roadManager.roadPositionsToRecheck.Add(newPosition1);
			}
        if(roadManager.roadPositionsToRecheck.Contains(newPosition2)) {
			} else {
				roadManager.roadPositionsToRecheck.Add(newPosition2);
			}
        if(roadManager.roadPositionsToRecheck.Contains(newPosition3)) {
			} else {
				roadManager.roadPositionsToRecheck.Add(newPosition3);
			}
        if(roadManager.roadPositionsToRecheck.Contains(newPosition4)) {
			} else {
				roadManager.roadPositionsToRecheck.Add(newPosition4);
			}
    }
    
    public void FixRoadAfterDelete()
    {
        foreach (var positionsToFix in roadManager.roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, positionsToFix);
        }
        placementManager.AddtemporaryStructuresToStructureDictionary();
        roadManager.roadPositionsToRecheck.Clear();
    }
}
