using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

//Renommer PathFinding
public class PathFinding : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    //distance pour se coller à l'objectif avant de passer au suivant
          //probl utiliser ca si on veut enventuellement assigner une liste de tâche et non faire 1 à 1
    [SerializeField] private float nextWaypointDistance = 3f;
    
    //current path
    Path path;
    int currentWayPoint = 0;
    bool reachEndOfPath;

    //seeker creates path
    Seeker seeker;
    Rigidbody2D rb;

    //flip dat boi
    [SerializeField] Transform PersonnageVisuel;   

    shipNPCmanager NPC;

    // Start is called before the first frame update
    void Start(){
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        NPC = shipNPCmanager.NPCmanagInstance;

        //Pas de target au début, donc pas nécessaire, mais je garde les lignes de codes au cas ou on change d'idée

        //Pour update là ou se situe l'objectif pratique si on veut que le personnage suive le target
        //InvokeRepeating("UpdatePath", 0f, 0.5f);
        //generate path (start point, end of path, funtion to call when done)
        //on laisse la ligne ici si on veut pas que ça update
        //seeker.StartPath(rb.position, target.position, OnPathComplete);

    }

    void UpdatePath()
    {
        if(target == null)
        {
            CancelInvoke();
            return;
        }
        if (seeker.IsDone()) 
            //generate path (start point, end of path, funtion to call when done)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p){
        if (!p.error){
            path = p;
            currentWayPoint = 0;           
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null ){
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            //Debug.Log("path end" + reachEndOfPath);
            reachEndOfPath = true;
            path = null;
            return;
        }
        else {
            reachEndOfPath = false;
        }

        //direction to the next waypoint 
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        //move bitch get out da way get out da way
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        //distance next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        //permet de flip le visuel du personnage dépandant de la direction, ou il se dirige
        if (force.x >= 0.1)
        {
            //checking moving to the right
            PersonnageVisuel.localScale = new Vector3(-1, 1, 1);
        }
        else if (force.x <= -0.1)
        {
            //checking moving to the left
            PersonnageVisuel.localScale = new Vector3(1, 1, 1);

        }
    }

    //appeler cette fonction pour que perso bouge son cul
    public void BobDoSomething(Transform Target)
    {
        target = Target;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

}
