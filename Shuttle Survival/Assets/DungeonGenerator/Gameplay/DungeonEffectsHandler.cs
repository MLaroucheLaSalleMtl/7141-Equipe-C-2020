using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEffectsHandler : MonoBehaviour
{
    public static DungeonEffectsHandler dungeonEffectsHandler;

    private void Awake()
    {
        if(dungeonEffectsHandler == null)
        {
            dungeonEffectsHandler = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDungeonEffects(List<DungeonEffect> dungeonEffects)
    {
        CharacterSystem choosenCharacter = DungeonEventPanelHandler.dungeonEventPanelHandler.GetChoosenCharacter();

        for (int i = 0; i < dungeonEffects.Count; i++)
        {
            switch (dungeonEffects[i].dungeonEffectType)
            {
                case DungeonEffectType.AffectHealthSingle:
                    choosenCharacter.Hurt(Mathf.RoundToInt(dungeonEffects[i].dungeonEffectIntensity));
                    break;
                case DungeonEffectType.AffectHealthParty:
                    
                    break;
                case DungeonEffectType.UnlockDoor:
                    DungeonDoorUnlocker.UnlockCurrentDungeonDoor();
                    break;
                
            }
        }
    }
}