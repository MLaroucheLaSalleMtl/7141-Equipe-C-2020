﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointOfInterest { empty, chair, tv, table};
public enum RoomDoors { FullyOpen, All, Close, NoClosure};

[System.Serializable]
public class Point
{
    [SerializeField]
    [Range(0, 12)]
    public int x;
    [SerializeField]
    [Range(0, 6)]
    public int y;
    [SerializeField]
    public PointOfInterest[] pointsOfInterest;
    [SerializeField]
    [Range(0,1f)]
    public float[] chancesToSpawn;
    public Point(int x, int y, PointOfInterest[] pointsOfInterest, float[] chancesToSpawn)
    {
        this.x = x;
        this.y = y;
        this.pointsOfInterest = pointsOfInterest;
        this.chancesToSpawn = chancesToSpawn;
    }
}

public class AddRoom : MonoBehaviour
{
    RoomTemplates templates;
    [SerializeField] GameObject zeroCoord;
    [SerializeField] RoomSetupConfig roomConfig;
    PointOfInterest[,] coord = new PointOfInterest[12, 6];

    [Header("Points of interests Pool")]
    [SerializeField] GameObject chair;
    [SerializeField] GameObject tv;
    [SerializeField] GameObject table;


    // Start is called before the first frame update
    void Start()
    {
        SetupRoomCoordsToEmpty();
        templates = RoomTemplates.roomTemplates;
        templates.RegisterRoom(gameObject);
    }

    private void SetupRoomCoordsToEmpty()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                coord[i, j] = PointOfInterest.empty;
            }
        }
    }

    public void ReceiveRoomConfig(RoomSetupConfig roomConfig)
    {
        this.roomConfig = roomConfig;
        RollPointsOfInterestsForThisRoom();
    }

    private void RollPointsOfInterestsForThisRoom()
    {
        for (int i = 0; i < roomConfig.possiblePointsOfInterests.Length; i++) //on roll tous les points of interests
        {
            float randUpperBound = 0f;
            for (int j = 0; j < roomConfig.possiblePointsOfInterests[i].pointsOfInterest.Length; j++) //cette loop sert a calculer les chances de spawn les objects du point of interest
            {
                //AU BESOIN, on peut ajouter un max au different points of interest, soit globalement (pas plus de X truc dans la map, ou per room)
                randUpperBound += roomConfig.possiblePointsOfInterests[i].chancesToSpawn[j];
            }
            float rand = Random.Range(0.00f, randUpperBound);
            for (int j = 0; j < roomConfig.possiblePointsOfInterests[i].pointsOfInterest.Length; j++)
            {
                if (rand <= roomConfig.possiblePointsOfInterests[i].chancesToSpawn[j]) //verifie si le rand entre dans le pourcentage de chance de spawn de le premier point of interest
                {
                    SpawnPointOfInterest(roomConfig.possiblePointsOfInterests[i].pointsOfInterest[j], new Vector2(roomConfig.possiblePointsOfInterests[i].x, roomConfig.possiblePointsOfInterests[i].y));
                    break; //on a spawner notre points of interest donc on passe au suivant
                }
                else if (rand > roomConfig.possiblePointsOfInterests[i].chancesToSpawn[j])
                {
                    rand -= roomConfig.possiblePointsOfInterests[i].chancesToSpawn[j]; //si notre rand a depasser les chances de j, on ajouter les chances de j au dice et on regarde le prochain
                }
            }
        }
    }

    public void SpawnPointOfInterest(PointOfInterest roomObject, Vector2 pointOfInterestPosition)
    {
        GameObject POI;
        switch (roomObject)
        {
            case PointOfInterest.chair:
                POI = Instantiate(chair, zeroCoord.transform);
                POI.transform.localPosition = pointOfInterestPosition;
                break;
            case PointOfInterest.tv:
                POI = Instantiate(tv, zeroCoord.transform);
                POI.transform.localPosition = pointOfInterestPosition;
                break;
            case PointOfInterest.table:
                POI = Instantiate(table, zeroCoord.transform);
                POI.transform.localPosition = pointOfInterestPosition;
                break;
        }
    }
}