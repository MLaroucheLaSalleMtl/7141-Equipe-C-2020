using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Tile currentTile;
                int k = 0;
                if (j == 0 || i == 0 || i == 14)
                {
                    if(j == 5 && ((i == 0 && leftDoor) || (i == 14 && rightDoor)))
                    {
                        currentTile = wallTile;
                    }
                    else
                    {
                        currentTile = botWallTile;
                    }
                    k = 1;
                }
                else if(j == 8)
                {
                    currentTile = wallTile;
                }
                else
                {
                    currentTile = floorTile;
                }
                k = 1;
                Vector3Int vector3Int = new Vector3Int(i, j, 0);

                tilemap.SetTile(vector3Int, currentTile);
            }
        }
        if (topDoor)
        {
            tilemap.SetTile(new Vector3Int(7, 8, 0), doorTile);
        }
        if (leftDoor)
        {
            tilemap.SetTile(new Vector3Int(0, 4, 0), doorTile);
        }
        if (bottomDoor)
        {
            tilemap.SetTile(new Vector3Int(7, 0, 0), doorTile);
        }
        if (rightDoor)
        {
            tilemap.SetTile(new Vector3Int(14, 4, 0), doorTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
