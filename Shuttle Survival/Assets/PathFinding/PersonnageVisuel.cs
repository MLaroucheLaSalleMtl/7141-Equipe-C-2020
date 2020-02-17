using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PersonnageVisuel : MonoBehaviour
{

    [SerializeField]private AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
        //permet de flip le visuel du personnage dépandant de la direction, ou il se dirige
        if (aiPath.desiredVelocity.x >= 0.1)
        {
            //checking moving to the right
            transform.localScale = new Vector3(-1, 1, 1);
        }else if (aiPath.desiredVelocity.x <= -0.1)
        {
            //checking moving to the left
            transform.localScale = new Vector3(1, 1, 1);

        }
    }
}
