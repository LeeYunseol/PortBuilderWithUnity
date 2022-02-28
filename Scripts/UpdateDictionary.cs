using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpdateDictionary : MonoBehaviour
{
    public PlacementManager placementManager;
    public RoadManager roadManager;
    public RoadFixer roadFixer;
    public GameObject gameObject;

    public GameObject save;
    // ★ save.transform.GetChild(i).position 이게 각 자식의 위치값을 불려오는 것 
    // ★ save.transform.GetChild(i).GetChild(0).GetChild(0) 이건 무슨 도로인지 가져오는 것 
    // ★★ save.transform.GetChild(i).GetComponent<StructureModel>() structuremodel 클래스의 인스턴스 가져오기

    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void updateDictionary()
    {
        /* 좌표 확인하는 코드 */
        // for(int i=0 ; i<save.transform.childCount ; i++)
        // {
        //     Debug.Log(save.transform.GetChild(i).position.x);
        //     Debug.Log(save.transform.GetChild(i).position.z);
        //     var newPosition = new Vector3Int(Mathf.RoundToInt(save.transform.GetChild(i).position.x) , 0, 
        //             Mathf.RoundToInt(save.transform.GetChild(i).position.z) );
        //     Debug.Log(newPosition);
        // }
        Debug.Log("기존에 설치됐던 도로 개수 : " + save.transform.childCount);
        for(int i=0 ; i<save.transform.childCount ; i++)
        {
            var newPosition = new Vector3Int(Mathf.RoundToInt(save.transform.GetChild(i).position.x) , 0, 
                    Mathf.RoundToInt(save.transform.GetChild(i).position.z));
            roadManager.roadPositionsToRecheck.Add(newPosition);
            placementManager.PlaceTemporaryStructure(newPosition, roadFixer.roadStraight , CellType.Road);
            
        }
        foreach (var positionsToFix in roadManager.roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, positionsToFix);
        }
        placementManager.AddtemporaryStructuresToStructureDictionary();
       
        DeleteChildren(save);

        
    }

    public void DeleteChildren(GameObject save) 
    {
        Transform[] childList = save.GetComponentsInChildren<Transform>(true);
        if(childList != null)
        {
            for(int i=1;i<childList.Length;i++)
            {
                if(childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    public void SaveButton()
    {
        //placementManger의 모든 자식을 다시 save의 자식으로 이동

        // while 로 하면 될 것 같긴하거든요
        
        /*
        for(int i=0 ; i<gameObject.transform.childCount ; i++)
        {
            gameObject.transform.GetChild(i).transform.SetParent(save.transform);
        }
        */
        Debug.Log(gameObject.transform.childCount);

        while(gameObject.transform.childCount != 0)
        {
           gameObject.transform.GetChild(0).transform.SetParent(save.transform);
        }

        Debug.Log("새로 설치한 도로의 개수는 : " + save.transform.childCount);
        PrefabUtility.SaveAsPrefabAsset(save, "Assets/Prefabs/Road/SaveModel.prefab");
    }
}
