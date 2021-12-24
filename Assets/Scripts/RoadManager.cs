using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject myRoad;
    [SerializeField] private GameObject finalRoad;
    [SerializeField] private List<GameObject> roads;
    [SerializeField] private Transform parent;
    //TODO
    [SerializeField]private bool isThereFinal;
    private GameObject final;
    private static RoadManager instance;
    public static RoadManager Instance => instance ?? (instance = instance = FindObjectOfType<RoadManager>());
    // Start is called before the first frame update

    private void Awake()
    {
        isThereFinal = roads.Count == 0 ? false : true;
        instance = instance ??= this;
        Debug.Log(instance);
    }
    [Button("Create")]
    void CreateRoad()
    {
        float roadPosition = myRoad.transform.localScale.z * (roads.Count == 0 ? roads.Count : roads.Count-1);
        float finalPosition = roadPosition + myRoad.transform.localScale.z;
        Debug.Log($"Final {finalPosition} Road {roadPosition} Roads count {roads.Count}");
        moveFinal(new Vector2(0, finalPosition));
        roads.Add(Instantiate(myRoad, new Vector3(0, 0 , roadPosition) , Quaternion.identity, parent));
    }
    [Button("Delete")]
    void DeleteRoad()
    {
        if(roads.Count == 0)
        {
            isThereFinal = false;
            return;
        }
        float finalPosition = myRoad.transform.localScale.z*(roads.Count - 2);
        moveFinal(new Vector2(0, finalPosition));
        DestroyImmediate(roads[roads.Count - 1]);
        roads.RemoveAt(roads.Count - 1);

    }

    [Button("Delete All")]
    void DeleteAll()
    {
        for (int i = 0; i < roads.Count; i++)
        {
            DestroyImmediate(roads[i]);
        }
        roads.Clear();
        isThereFinal = false;
        Debug.Log("Cleared all");
    }
    void moveFinal(Vector2 position)
    {
        if (!isThereFinal)
        {
            roads.Add(Instantiate(finalRoad, Vector3.zero, Quaternion.identity, parent));
        }
        isThereFinal = true;
        roads[0].transform.position = new Vector3(position.x, 0, position.y);
    }

   

}
