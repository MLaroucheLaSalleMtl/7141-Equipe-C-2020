using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom opening
    //2 --> need top opening
    // 3 --> need left opening
    // 4  --> need right opening
    public float timeSpawned;
    private RoomTemplates templates;
    public float waitTime = 4f;
    private bool spawned = false;
    int numOfRooms = 0;

    private void Awake()
    {
        if (transform.position == Vector3.zero) // delete rooms at starting point.
            Destroy(this.gameObject);
        templates = RoomTemplates.roomTemplates;
    }
    private void Start()
    {
        timeSpawned = Time.time;
        Invoke("Spawn", Random.Range(0.04f, 0.25f));
    }
    private void Spawn()
    {
        if(spawned == false)
        {
            Destroy(gameObject, waitTime);
            spawned = true;
            numOfRooms = templates.numOfRooms;
            RoomDoors roomDoors = GetRoomOpennessFromDungeonSheet();
            InstantiateRoomAccordingToRoomDoorsAndOpenDirection(roomDoors);
        }
    }

    private void InstantiateRoomAccordingToRoomDoorsAndOpenDirection(RoomDoors roomDoors)
    {
        if (openingDirection == 1)
        {
            //need to spawn a room with B
            switch (roomDoors)
            {
                case RoomDoors.All:
                    Instantiate(templates.bottomRooms[Random.Range(0, templates.bottomRooms.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.FullyOpen:
                    Instantiate(templates.fourWayRoom, transform.position, Quaternion.identity);
                    break;
                case RoomDoors.Close:
                    Instantiate(templates.botRoomClosure[Random.Range(0, templates.botRoomClosure.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.NoClosure:
                    Instantiate(templates.botRoomsNoClosure[Random.Range(0, templates.botRoomsNoClosure.Length)], transform.position, Quaternion.identity);
                    break;
            }
        }
        else if (openingDirection == 2)
        {
            //need to spawn a room with T
            switch (roomDoors)
            {
                case RoomDoors.All:
                    Instantiate(templates.topRooms[Random.Range(0, templates.bottomRooms.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.FullyOpen:
                    Instantiate(templates.fourWayRoom, transform.position, Quaternion.identity);
                    break;
                case RoomDoors.Close:
                    Instantiate(templates.topRoomClosure[Random.Range(0, templates.botRoomClosure.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.NoClosure:
                    Instantiate(templates.topRoomsNoClosure[Random.Range(0, templates.botRoomsNoClosure.Length)], transform.position, Quaternion.identity);
                    break;
            }
        }
        else if (openingDirection == 3)
        {
            //need to spawn a room with L
            switch (roomDoors)
            {
                case RoomDoors.All:
                    Instantiate(templates.leftRooms[Random.Range(0, templates.bottomRooms.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.FullyOpen:
                    Instantiate(templates.fourWayRoom, transform.position, Quaternion.identity);
                    break;
                case RoomDoors.Close:
                    Instantiate(templates.leftRoomClosure[Random.Range(0, templates.botRoomClosure.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.NoClosure:
                    Instantiate(templates.leftRoomsNoClosure[Random.Range(0, templates.botRoomsNoClosure.Length)], transform.position, Quaternion.identity);
                    break;
            }
        }
        else if (openingDirection == 4)
        {
            //need to spawn a room with R
            switch (roomDoors)
            {
                case RoomDoors.All:
                    Instantiate(templates.rightRooms[Random.Range(0, templates.bottomRooms.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.FullyOpen:
                    Instantiate(templates.fourWayRoom, transform.position, Quaternion.identity);
                    break;
                case RoomDoors.Close:
                    Instantiate(templates.rightRoomClosure[Random.Range(0, templates.botRoomClosure.Length)], transform.position, Quaternion.identity);
                    break;
                case RoomDoors.NoClosure:
                    Instantiate(templates.rightRoomsNoClosure[Random.Range(0, templates.botRoomsNoClosure.Length)], transform.position, Quaternion.identity);
                    break;
            }
        }
    }

    private RoomDoors GetRoomOpennessFromDungeonSheet()
    {
        RoomDoors roomDoors = RoomDoors.All;
        for (int i = 0; i < templates.currentDungeonSheet.roomDoors.Length; i++)
        {
            if (numOfRooms >= templates.currentDungeonSheet.roomDoorsNum[i] && i != templates.currentDungeonSheet.roomDoors.Length - 1)
            {
                numOfRooms -= templates.currentDungeonSheet.roomDoorsNum[i];
            }
            else
            {
                roomDoors = templates.currentDungeonSheet.roomDoors[i];
                templates.numOfRooms++;
                break;
            }
        }
        return roomDoors;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if(collision.GetComponent<RoomSpawner>())
            {
                //Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                if(timeSpawned > collision.GetComponent<RoomSpawner>().timeSpawned)
                {
                    Destroy(gameObject);
                }
            }
            //spawned = true;
        }
    }
}
