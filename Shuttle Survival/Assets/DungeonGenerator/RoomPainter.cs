using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum DoorOpening { Top, Left, Bottom, Right };
public class RoomPainter : MonoBehaviour
{
    [SerializeField] Tile floorTile;
    [SerializeField] Tile wallTile;
    [SerializeField] Tile botWallTile;
    [SerializeField] Tile doorTile;

    int x = 15;
    int y = 9;
    Tilemap tilemap;
    [SerializeField] bool topDoor = false;
    [SerializeField] bool leftDoor = false;
    [SerializeField] bool bottomDoor = false;
    [SerializeField] bool rightDoor = false;

    [SerializeField] GameObject topDoorPrefab;
    [SerializeField] GameObject leftDoorPrefab;
    [SerializeField] GameObject bottomDoorPrefab;
    [SerializeField] GameObject rightDoorPrefab;

    GameObject linkedRoom;

   
    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PaintRoom()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Tile currentTile;
                if (j == 0 || i == 0 || i == 14)
                {
                    if (j == 5 && ((i == 0 && leftDoor) || (i == 14 && rightDoor)))
                    {
                        currentTile = wallTile;
                    }
                    else
                    {
                        currentTile = botWallTile;
                    }
                }
                else if (j == 8)
                {
                    currentTile = wallTile;
                }
                else
                {
                    currentTile = floorTile;
                }
                Vector3Int vector3Int = new Vector3Int(i, j, 0);

                tilemap.SetTile(vector3Int, currentTile);
            }
        }
        Transform zeroCoord = GetComponentInChildren<AddRoom>().GetZeroCoord();
       
        if (topDoor)
        {
            Vector3Int position = new Vector3Int(7, 8, 0);
            Tile tileToPaint;
            if (NeighborRoomHaveSameOpening(DoorOpening.Top))
            {
                tileToPaint = floorTile;
                GameObject door = Instantiate(topDoorPrefab, zeroCoord);
                door.transform.localPosition = position - new Vector3(1, 1, 0);
                door.GetComponent<DungeonDoor>().SetLinkedRoomPositionAndOpening(linkedRoom, DoorOpening.Top);
                GetComponent<AddRoom>().ReceiveDungeonDoor(DoorOpening.Top, door.GetComponent<DungeonDoor>());
            }
            else
            {
                tileToPaint = wallTile;
            }
            tilemap.SetTile(position, tileToPaint);

        }
        if (leftDoor)
        {
            Tile tileToPaint;
            Vector3Int position = new Vector3Int(0, 4, 0);
            if (NeighborRoomHaveSameOpening(DoorOpening.Left))
            {
                tileToPaint = floorTile;
                GameObject door = Instantiate(leftDoorPrefab, zeroCoord);
                door.transform.localPosition = position - new Vector3(1, 1, 0);
                door.GetComponent<DungeonDoor>().SetLinkedRoomPositionAndOpening(linkedRoom, DoorOpening.Left);
                GetComponent<AddRoom>().ReceiveDungeonDoor(DoorOpening.Left, door.GetComponent<DungeonDoor>());

            }
            else
            {
                tileToPaint = wallTile;
            }
            tilemap.SetTile(position, tileToPaint);

        }
        if (bottomDoor)
        {
            Tile tileToPaint;
            Vector3Int position = new Vector3Int(7, 0, 0);
            if (NeighborRoomHaveSameOpening(DoorOpening.Bottom))
            {
                tileToPaint = floorTile;
                GameObject door = Instantiate(bottomDoorPrefab, zeroCoord);
                door.transform.localPosition = position - new Vector3(1, 1, 0);
                door.GetComponent<DungeonDoor>().SetLinkedRoomPositionAndOpening(linkedRoom, DoorOpening.Bottom);
                GetComponent<AddRoom>().ReceiveDungeonDoor(DoorOpening.Bottom, door.GetComponent<DungeonDoor>());
            }
            else
            {
                tileToPaint = wallTile;
            }
            tilemap.SetTile(position, tileToPaint);
        }
        if (rightDoor)
        {
            Tile tileToPaint;
            Vector3Int position = new Vector3Int(14, 4, 0);
            if (NeighborRoomHaveSameOpening(DoorOpening.Right))
            {
                tileToPaint = floorTile;
                GameObject door = Instantiate(bottomDoorPrefab, zeroCoord);
                door.transform.localPosition = position - new Vector3(1, 1, 0);
                door.GetComponent<DungeonDoor>().SetLinkedRoomPositionAndOpening(linkedRoom, DoorOpening.Right);
                GetComponent<AddRoom>().ReceiveDungeonDoor(DoorOpening.Right, door.GetComponent<DungeonDoor>());
            }
            else
            {
                tileToPaint = wallTile;
            }
            tilemap.SetTile(position, tileToPaint);
            
        }
    }
    private bool NeighborRoomHaveSameOpening(DoorOpening currentDoorOpening)
    {
        Vector3 relativePosToRoomOfInterest = Vector3.zero;
        switch (currentDoorOpening)
        {
            case DoorOpening.Top:
                relativePosToRoomOfInterest = new Vector3(0, 9, 0);
                break;
            case DoorOpening.Left:
                relativePosToRoomOfInterest = new Vector3(-15, 0, 0);
                break;
            case DoorOpening.Bottom:
                relativePosToRoomOfInterest = new Vector3(0, -9, 0);
                break;
            case DoorOpening.Right:
                relativePosToRoomOfInterest = new Vector3(15, 0, 0);
                break;
        }       
        List<GameObject> rooms = RoomTemplates.roomTemplates.rooms;
        for (int i = 0; i < rooms.Count; i++)
        {
            if(rooms[i].transform.position - transform.position == relativePosToRoomOfInterest)
            {
                switch (currentDoorOpening)
                {
                    case DoorOpening.Top:
                        if (rooms[i].GetComponent<RoomPainter>().bottomDoor)
                        {
                            linkedRoom = rooms[i];
                            return true;
                        }
                        break;
                    case DoorOpening.Left:
                        if (rooms[i].GetComponent<RoomPainter>().rightDoor)
                        {
                            linkedRoom = rooms[i];
                            return true;
                        }
                        break;
                    case DoorOpening.Bottom:
                        if (rooms[i].GetComponent<RoomPainter>().topDoor)
                        {
                            linkedRoom = rooms[i];
                            return true;
                        }
                        break;
                    case DoorOpening.Right:
                        if (rooms[i].GetComponent<RoomPainter>().leftDoor)
                        {
                            linkedRoom = rooms[i];
                        return true;
                        }
                        break;
                }
                return false;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
