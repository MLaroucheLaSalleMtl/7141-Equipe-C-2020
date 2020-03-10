using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public static RoomTemplates roomTemplates;

    public GameObject[] bottomRooms;
    public GameObject[] botRoomsNoClosure;
    public GameObject[] botRoomClosure;

    public GameObject[] topRooms;
    public GameObject[] topRoomsNoClosure;
    public GameObject[] topRoomClosure;

    public GameObject[] leftRooms;
    public GameObject[] leftRoomsNoClosure;
    public GameObject[] leftRoomClosure;

    public GameObject[] rightRooms;
    public GameObject[] rightRoomsNoClosure;
    public GameObject[] rightRoomClosure;

    public GameObject closedRoom;
    public GameObject fourWayRoom;
    public List<GameObject> rooms;

    public float waitTime;
    private bool assignedRooms;

    public DungeonSheet currentDungeonSheet;
    public GameObject[] numberSprites;
    public GameObject boss;
    bool guaranteedPosition = false;

    public int numOfRooms = 0;
    

    private void Awake()
    {
        if (RoomTemplates.roomTemplates == null)
        {
            RoomTemplates.roomTemplates = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Instantiate(currentDungeonSheet.startingRoom, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTime <= 0 && assignedRooms == false)
        {
            AssignConfigToRooms();
        }
        else if(assignedRooms == false)
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void AssignConfigToRooms()
    {
        assignedRooms = true;
        if (currentDungeonSheet.guaranteedPositions.Length > 0) guaranteedPosition = true; //on verifie si il existe des room garantie
        int guaranteedIndex = 0;

        int [] currentRoomTypeAmount = new int[currentDungeonSheet.maximumAmountOfRoomType.Length]; //array qui va keep track de combien de chaque random room on a, pour respecter nos maximum. //-
        for (int i = 0; i < rooms.Count; i++)
        {
            if (guaranteedPosition)
            {
                float positionPercentage = i * 1f / (rooms.Count - 1);
                if (positionPercentage >= currentDungeonSheet.guaranteedPositions[guaranteedIndex]) //si on a atteint ou depasser la position reserver a la piece garantie, on la cree
                {
                    rooms[i].GetComponent<AddRoom>().ReceiveRoomConfig(currentDungeonSheet.guaranteedRoomsConfig[guaranteedIndex]); //on assigne la configuration a la room qui va se charger de la respecter
                    guaranteedIndex++; //on increment l'index pour passer a la guaranteed room suivante
                    if (guaranteedIndex >= currentDungeonSheet.guaranteedPositions.Length) guaranteedPosition = false; //si jamais on a atteint la fin des guaranteed room, on arrete de verifier pour les prochaines rooms
                    continue; //keyword qui fait passer a la prochaine iteration, on a spawn une guaranteed room alors notre job est faite pour cette iteration
                }
            }
            //si on est rendu ici dans l'iteration, c'est qu'on doit tirer une room au hasard selon la configuration.
            float randUpperBound = CalculateTotalRoomChances(currentRoomTypeAmount, i);

            float rand = Random.Range(0.00f, randUpperBound);
            for (int j = 0; j < currentDungeonSheet.randomRoomsConfig.Length; j++) //cette loop va assigner au hasard, selon les chances calculees, le role de la room
            {
                if (currentRoomTypeAmount[j] >= currentDungeonSheet.maximumAmountOfRoomType[j]) continue; //si cette room type est deja a max capacity, on passe a la suivante, n'ont pas ete calcule dans les chances anyway
                if (rand <= currentDungeonSheet.chancesToBeDrawn[j] + (currentDungeonSheet.variationPerRoom[j] * i)) //verifie si le rand entre dans le pourcentage de chance de spawn de la premiere room
                {
                    rooms[i].GetComponent<AddRoom>().ReceiveRoomConfig(currentDungeonSheet.randomRoomsConfig[j]);
                    currentRoomTypeAmount[j]++;
                    break; //on a assign une config donc on passe a la room suivante
                }
                else
                {
                    rand -= currentDungeonSheet.chancesToBeDrawn[j]; //si notre rand a depasser les chances de j, on ajouter les chances de j au dice et on regarde le prochain
                }
            }
        }
    }

    private float CalculateTotalRoomChances(int[] amountOfRoomsPerType, int i)
    {
        float randUpperBound = 0;
        for (int j = 0; j < currentDungeonSheet.chancesToBeDrawn.Length; j++) //cette loop sert a calculer les chances de spawn de nos rooms config.
        {
            if (amountOfRoomsPerType[j] >= currentDungeonSheet.maximumAmountOfRoomType[j]) continue; //si notre room config a deja atteitn son maximum, on ne comptabilise pas ses chances.
            randUpperBound += currentDungeonSheet.chancesToBeDrawn[j] + (currentDungeonSheet.variationPerRoom[j] * i); //on calcule egalement la variation par room.
        }

        return randUpperBound;
    }

    public void ReceiveDungeonSheet(DungeonSheet dungeonSheet)
    {
        this.currentDungeonSheet = dungeonSheet;
    }

    public void RegisterRoom(GameObject roomToRegister)
    {
        rooms.Add(roomToRegister);
    }
}
