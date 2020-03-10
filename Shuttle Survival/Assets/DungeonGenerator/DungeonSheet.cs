using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Space/Dungeon Sheet")]
public class DungeonSheet : ScriptableObject
{
    [Header("General Dungeon Settings")]
    public GameObject startingRoom;
    public RoomDoors[] roomDoors;
    public int[] roomDoorsNum;

    [Header("Guaranteed Rooms Configuration")]
    public RoomSetupConfig[] guaranteedRoomsConfig;
    [Range(0.00f, 1.00f)][Tooltip("Une position de 0 signifie la premiere room instantiated, et 1 signifie la derniere, il faut que les guaranteeed room soient suffisament espacees pour ne pas avoir de conflits")]
    public float[] guaranteedPositions;

    [Header("Random Rooms Configuration")]
    public RoomSetupConfig[] randomRoomsConfig;
    [Range(0.00f, 1.00f)]
    public float[] chancesToBeDrawn;
    public float[] variationPerRoom;
    public int[] maximumAmountOfRoomType;
}
