using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Space/Room Setup Config")]
public class RoomSetupConfig : ScriptableObject
{
    [SerializeField] string roomSetupName;
    [SerializeField] public RoomPoint[] possiblePointsOfInterests;
}
