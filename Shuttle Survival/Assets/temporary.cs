using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temporary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ShipScanManager.shipScanManager.TESTING_MANUALLY_SETUP_CHARACTER_UI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
