using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestManager : MonoBehaviour
{
    public static PointOfInterestManager pointOfInterestManager;
    [Header("POI Pool")]
    [SerializeField] PointOfInterestObject chair;
    [SerializeField] PointOfInterestObject table;
    [SerializeField] PointOfInterestObject tv;
    
    private void Awake()
    {
        if(pointOfInterestManager == null)
        {
            pointOfInterestManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPOI(Vector2 poiPosition, Transform zeroCoordParent, PointOfInterestObject poiObject)
    {
        PointOfInterestObject newPOI = null;     
        
        newPOI = Instantiate(poiObject, zeroCoordParent);
        newPOI.transform.localPosition = poiPosition;
    }


}
