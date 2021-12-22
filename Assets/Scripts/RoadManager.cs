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
    //[SerializeField] private bool isThereFinal = false;
    private static RoadManager instance;
    public static RoadManager Instance => instance ?? (instance = instance = FindObjectOfType<RoadManager>());
    // Start is called before the first frame update

    private void Awake()
    {
        instance = instance ??= this;
        Debug.Log(instance);
    }
    [Button]
    void CreateRoad()
    {
        float roadPosition = myRoad.transform.localScale.z * roads.Count;
        float finalPosition = roadPosition + myRoad.transform.localPosition.z;
        roads.Add(Instantiate(myRoad, new Vector3(0, 0 , roadPosition) , Quaternion.identity, parent));
    }
    [Button]
    void DeleteRoad()
    {
        float finalPosition = myRoad.transform.localScale.z*(roads.Count - 1);
        DestroyImmediate(roads[roads.Count - 1]);
        roads.RemoveAt(roads.Count - 1);
    }

    void moveFinal(Vector2 position)
    {
        finalRoad.transform.position = new Vector3(position.x, 0, position.y);
    }


}
