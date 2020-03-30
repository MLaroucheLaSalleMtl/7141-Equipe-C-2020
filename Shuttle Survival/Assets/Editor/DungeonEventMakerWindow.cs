using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class DungeonEventMakerWindow : EditorWindow
{    
    Texture2D headerSectionTexture;
    Rect headerSection;
    Rect buttonSection;
    Texture2D eventSectionTexture;

    Texture2D event2SectionTexture;
    Rect event2Section;
    Rect scrollView;
    Rect totalScrollableView;
    Vector2 scrollPosition = Vector2.zero;
    
    Rect[] eventSections = new Rect[20];
    string subfolder;
    GUISkin skin;

    int[] numberOfEventOptions = new int[20];
    bool[] unlockedColumns = new bool[20];
    string[] columnEventNames = new string[20];
    int[] columnEventIndex = new int[20];
    public DungeonEvent[] dungeonEvents = new DungeonEvent[20];
    public DungeonOption[,] dungeonEventsOptions = new DungeonOption[20,5]; //related to dungeonevents, array of 5 options for this event.
    List<bool> rollAffectedByModifiers = new List<bool>();


    static DungeonEventMakerWindow window;

    [MenuItem("Dungeon Tools/Dungeon Event Maker")]
    static void OpenWindow()
    {       
        window = (DungeonEventMakerWindow)GetWindow(typeof(DungeonEventMakerWindow));
        window.minSize = new Vector2(1500, 900);
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
        InitData();
        skin = Resources.Load<GUISkin>("guiStyles/DungeonEventMakerWindowSkin");
        EditorStyles.textArea.wordWrap = true;
    }

    private void InitData()
    {
        unlockedColumns[0] = true;
        columnEventNames[0] = "Base Dungeon Event";
        columnEventIndex[0] = 0;
        for(int i = 0; i < dungeonEvents.Length; i++)
        {
            dungeonEvents[i] = (DungeonEvent)ScriptableObject.CreateInstance(typeof(DungeonEvent));
        }
        for (int i = 0; i < dungeonEventsOptions.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonEventsOptions.GetLength(1); j++)
            {
                dungeonEventsOptions[i,j] = (DungeonOption)ScriptableObject.CreateInstance(typeof(DungeonOption));
            }
        }       
    }

    private void InitTextures()
    {
        headerSectionTexture = Resources.Load<Texture2D>("Textures/Titanium");
        eventSectionTexture = Resources.Load<Texture2D>("Textures/PiggyPink");
        event2SectionTexture = Resources.Load<Texture2D>("Textures/Parklife");
    }

    private void OnGUI()
    {
        DrawHeaderAndButtonLayout();
        DrawHeader();
        DrawCreateButton();

        scrollPosition = GUI.BeginScrollView(scrollView, scrollPosition, totalScrollableView, false, false);
        DrawEventLayouts();
        for (int i = 0; i < unlockedColumns.Length; i++)
        {
            if (unlockedColumns[i])
            {
                DrawEvent1Column(i);
            }
        }
        GUI.EndScrollView();
    }


    private void DrawHeaderAndButtonLayout()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 60;
        GUI.DrawTexture(headerSection, headerSectionTexture);

        buttonSection.x = Screen.width - 200;
        buttonSection.y = 5;
        buttonSection.width = 200;
        buttonSection.height = 60;

    }

    private void DrawEventLayouts()
    {
        scrollView.x = 0;
        scrollView.y = 60;
        scrollView.width = Screen.width;
        scrollView.height = Screen.height - 80;

        totalScrollableView.x = 0;
        totalScrollableView.y = 60;
        totalScrollableView.width = (Screen.width / 6f) * eventSections.Length;
        totalScrollableView.height = 4000;

        for (int i = 0; i < eventSections.Length; i++)
        {
            eventSections[i].x = (i * (Screen.width / 6f));
            eventSections[i].y = 60;
            eventSections[i].width = Screen.width / 6f;
            eventSections[i].height = 4000;
            if (i % 2 == 0)
            {
                GUI.DrawTexture(eventSections[i], eventSectionTexture);
            }
            else
            {
                GUI.DrawTexture(eventSections[i], event2SectionTexture);
            }
        }
    }

    private void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        GUILayout.Label("Dungeon Event Maker Tool", skin.GetStyle("Header1"));
        GUILayout.EndArea();
    }

    private void DrawCreateButton()
    {
        GUILayout.BeginArea(buttonSection);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Subfolder:", skin.GetStyle("Event1"));
        subfolder = GUILayout.TextField(subfolder);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("CREATE", GUILayout.Height(25)))
        {
            WarningCreationWindow.OpenWindow();
        }
        GUILayout.EndArea();
    }

    private void DrawEvent1Column(int columnIndex)
    {        
        GUILayout.BeginArea(eventSections[columnIndex]);
        GUILayout.Label(columnEventNames[columnIndex], skin.GetStyle("Event1"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Event Name", skin.GetStyle("Event1"));
        dungeonEvents[columnIndex].eventName = EditorGUILayout.TextField(dungeonEvents[columnIndex].eventName);        
        EditorGUILayout.EndHorizontal();

        
        GUILayout.Label("Event Message", skin.GetStyle("Event1"));
        dungeonEvents[columnIndex].eventMessage = EditorGUILayout.TextArea(dungeonEvents[columnIndex].eventMessage, EditorStyles.textArea, GUILayout.Height(120), GUILayout.Width(Screen.width / 6f - 20));

        GUILayout.Label("Event Effects", skin.GetStyle("Event1"));

        for (int i = 0; i < dungeonEvents[columnIndex].eventEffects.Count; i++)
        {
            DrawDungeonEventEffects(dungeonEvents[columnIndex], i);
        }

        if (GUILayout.Button("Add Event Effect ! ", GUILayout.Height(20)))
        {
            CreateDungeonEffect(dungeonEvents[columnIndex]);
        }

        GUILayout.Label("Event Options", skin.GetStyle("Event1"));

        for (int i = 0; i < dungeonEvents[columnIndex].dungeonOptions.Count; i++)
        {
            DrawDungeonEventOptions(dungeonEvents[columnIndex], i, columnIndex);
        }

        if (GUILayout.Button("Create Option ! ", GUILayout.Height(40)))
        {
            CreateDungeonOption(dungeonEvents[columnIndex], columnIndex);
        }

        
        GUILayout.EndArea();
    }

    

    private void CreateDungeonEffect(DungeonEvent dungeonEvent)
    {
        DungeonEffect newDungeonEffect = new DungeonEffect();
        dungeonEvent.eventEffects.Add(newDungeonEffect);


    }

    private void DrawDungeonEventEffects(DungeonEvent dungeonEvent, int effectIndex)
    {
        DungeonEffect currentEffect = dungeonEvent.eventEffects[effectIndex];       
        GUILayout.Label("Effect " + (effectIndex + 1), skin.GetStyle("Event1"));
        currentEffect.dungeonEffectType = (DungeonEffectType)EditorGUILayout.EnumPopup(currentEffect.dungeonEffectType);
        if(currentEffect.dungeonEffectType != DungeonEffectType.SpecificItemsLoot)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Effect intensity in unit", skin.GetStyle("Event1"));
            currentEffect.dungeonEffectIntensity = EditorGUILayout.FloatField(currentEffect.dungeonEffectIntensity);
            EditorGUILayout.EndHorizontal();
        }        
        else
        {
            GUILayout.Label("What item to loot", skin.GetStyle("Event1"));
            var serializedObject = new SerializedObject(dungeonEvent);
            var serializedProperty = serializedObject.FindProperty("eventEffects");
            EditorGUILayout.PropertyField(serializedProperty.GetArrayElementAtIndex(effectIndex).FindPropertyRelative("specificItemsToReceive"));
            serializedObject.ApplyModifiedProperties();
        }


    }

    private void CreateDungeonOption(DungeonEvent dungeonEvent, int columnIndex)
    {
        dungeonEvent.dungeonOptions.Add(dungeonEventsOptions[columnIndex, numberOfEventOptions[columnIndex]]);
        numberOfEventOptions[columnIndex]++;        
    }

    private void DrawDungeonEventOptions(DungeonEvent currentDungeonEvent, int optionIndex, int columnIndex)
    {
        DungeonOption currentOption = dungeonEventsOptions[columnIndex, optionIndex];
        GUILayout.BeginHorizontal();
        GUILayout.Label("Option " + (optionIndex + 1) + " name", skin.GetStyle("Event1"));
        currentOption.optionName = GUILayout.TextField(currentOption.optionName);
        GUILayout.EndHorizontal();
        GUILayout.Label("Option Text", skin.GetStyle("Event1"));
        currentOption.optionText = EditorGUILayout.TextArea(currentOption.optionText, EditorStyles.textArea, GUILayout.Height(30), GUILayout.Width(Screen.width / 6f - 20));
        GUILayout.Label("Requirements for the option \nto be active", skin.GetStyle("Event1"));
        for (int i = 0; i < currentOption.requirementsToBeActive.Count; i++)
        {
            GUILayout.Label("Requirement " + (i + 1), skin.GetStyle("Event1"));
            DungeonOptionRequirement currentRequirement = currentOption.requirementsToBeActive[i];
            currentRequirement.statsToCheck = (DungeonStatsToCheck)EditorGUILayout.EnumPopup(currentRequirement.statsToCheck);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Quantity Required", skin.GetStyle("Event1"));
            currentRequirement.quantityRequired = EditorGUILayout.FloatField(currentRequirement.quantityRequired);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add requirement! ", GUILayout.Height(40)))
        {
            AddOptionRequirement(currentOption);
        }

        DungeonRoll currentRoll = currentOption.relatedDungeonRoll;
        DrawDungeonRoll(currentRoll, currentOption, currentDungeonEvent);
    }

    private void DrawDungeonRoll(DungeonRoll currentRoll, DungeonOption currentOption, DungeonEvent currentDungeonEvent)
    {
        GUILayout.Label("Related Roll Test", skin.GetStyle("Event1"));
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Base Success (%)", skin.GetStyle("Event1"));
        currentRoll.baseChanceOfSucess = EditorGUILayout.Slider(currentRoll.baseChanceOfSucess * 100f, 0, 100) / 100f;
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < currentRoll.rollChanceModifiers.Count; i++)
        {
            GUILayout.Label("Roll modifier " + (i + 1), skin.GetStyle("Event1"));
            DungeonRollModifier currentRollModifier = currentRoll.rollChanceModifiers[i];
            currentRollModifier.statsToCheck = (DungeonStatsToCheck)EditorGUILayout.EnumPopup(currentRollModifier.statsToCheck);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Percentage change per unit", skin.GetStyle("Event1"));
            currentRollModifier.percentIncreasePerRelatedStatsPoint = EditorGUILayout.FloatField(currentRollModifier.percentIncreasePerRelatedStatsPoint * 100f) / 100f;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Decreaser", skin.GetStyle("Event1"));
            currentRollModifier.decreaser = EditorGUILayout.Toggle(currentRollModifier.decreaser);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Party wide", skin.GetStyle("Event1"));
            currentRollModifier.partyWide = EditorGUILayout.Toggle(currentRollModifier.partyWide);
            EditorGUILayout.EndHorizontal();

        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("% modifiers", skin.GetStyle("Event1"));

        if (GUILayout.Button("Add roll modifier! ", GUILayout.Height(25)))
        {
            AddModifierToRoll(currentRoll);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Event if success", skin.GetStyle("Event1"));
        if (currentRoll.successEvent == null && GUILayout.Button("Add success event! ", GUILayout.Height(25)))
        {
            currentRoll.successEvent = FindEmptyEventInArray();
            CreateNewEventArea(currentDungeonEvent.eventName + "-" + currentOption.optionName + "-Success Event", currentRoll.successEvent);
        }
        GUILayout.Label("Event if fail", skin.GetStyle("Event1"));
        if (currentRoll.failureEvent == null && GUILayout.Button("Add failure event! ", GUILayout.Height(25)))
        {
            currentRoll.failureEvent = FindEmptyEventInArray();
            CreateNewEventArea(currentDungeonEvent.eventName + "-" + currentOption.optionName + "-Failure Event", currentRoll.failureEvent);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("Critical Failure Possible?");
        currentRoll.critFailPossible = EditorGUILayout.Toggle(currentRoll.critFailPossible);
        GUILayout.EndHorizontal();
        if (currentRoll.critFailPossible)
        {
            if (currentRoll.criticalFailureEvent == null && GUILayout.Button("Add critical failure event! ", GUILayout.Height(25)))
            {
                currentRoll.criticalFailureEvent = FindEmptyEventInArray();
                CreateNewEventArea(currentDungeonEvent.eventName + "-" + currentOption.optionName + "-CritFail Event", currentRoll.criticalFailureEvent);
            }
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("Critical Success Possible?");
        currentRoll.critSucessPossible = EditorGUILayout.Toggle(currentRoll.critSucessPossible);
        GUILayout.EndHorizontal();
        if (currentRoll.critSucessPossible)
        {
            if (currentRoll.criticalSuccessEvent == null && GUILayout.Button("Add critical success event! ", GUILayout.Height(25)))
            {
                currentRoll.criticalSuccessEvent = FindEmptyEventInArray();
                CreateNewEventArea(currentDungeonEvent.eventName + "-" + currentOption.optionName + "-CritSuccess Event", currentRoll.criticalSuccessEvent);
            }
        }
    }

    private void AddOptionRequirement(DungeonOption currentOption)
    {
        DungeonOptionRequirement newRequirement = new DungeonOptionRequirement();
        currentOption.requirementsToBeActive.Add(newRequirement);
    }

    private void AddModifierToRoll(DungeonRoll relatedDungeonRoll)
    {
        DungeonRollModifier newRollModifier = new DungeonRollModifier();
        relatedDungeonRoll.rollChanceModifiers.Add(newRollModifier);
    }

    private void CreateNewEventArea(string eventAreaTitle, DungeonEvent currentEvent)
    {
        for (int i = 0; i < unlockedColumns.Length; i++)
        {
            if (!unlockedColumns[i])
            {
                unlockedColumns[i] = true;
                columnEventNames[i] = eventAreaTitle;
                columnEventIndex[i] = Array.IndexOf(dungeonEvents, currentEvent);
                break;
            }
        }
    }

    private DungeonEvent FindEmptyEventInArray()
    {
        for (int i = 0; i < unlockedColumns.Length; i++)
        {
            if(unlockedColumns[i] == false)
            {
                return dungeonEvents[i];
            }
        }
        return null;
    }

    public void CreateEverything()
    {       
        string dataPath = "Assets/Resources/DungeonEvents/";

        if (!AssetDatabase.IsValidFolder(dataPath + subfolder))
        {
            System.IO.Directory.CreateDirectory(dataPath + subfolder);
        }

        dataPath = Path.Combine(dataPath, subfolder);
        dataPath = Path.Combine(dataPath, dungeonEvents[0].eventName);

        if (!AssetDatabase.IsValidFolder(dataPath))
        {
            System.IO.Directory.CreateDirectory(dataPath);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        DungeonEvent[] dungeonEventsReferences = new DungeonEvent[20];
        for (int i = 0; i < unlockedColumns.Length; i++)
        {
            if (unlockedColumns[i])
            {
                AssetDatabase.CreateAsset(dungeonEvents[i], dataPath + "/" + dungeonEvents[i].eventName + ".asset");
                dungeonEventsReferences[i] = (DungeonEvent)AssetDatabase.LoadMainAssetAtPath(dataPath + "/" + dungeonEvents[i].eventName + ".asset");
            }
        }
        
        dataPath = Path.Combine(dataPath, "Options");
        if (!AssetDatabase.IsValidFolder(dataPath))
        {
            System.IO.Directory.CreateDirectory(dataPath);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        DungeonOption[,] dungeonEventsOptionsReferences = new DungeonOption[20, 5];
        for (int i = 0; i < dungeonEventsOptions.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonEventsOptions.GetLength(1); j++)
            {
                if(dungeonEventsOptions[i,j].optionName != null)
                {
                    AssetDatabase.CreateAsset(dungeonEventsOptions[i, j], dataPath + "/" + dungeonEventsOptions[i, j].optionName + ".asset");
                    dungeonEventsOptionsReferences[i, j] = (DungeonOption)AssetDatabase.LoadMainAssetAtPath(dataPath + "/" + dungeonEventsOptions[i, j].optionName + ".asset");                   
                }                
            }
        }

        for (int i = 0; i < unlockedColumns.Length; i++)
        {
            if (unlockedColumns[i])
            {
                EditorUtility.SetDirty(dungeonEventsReferences[i]);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        window.Close();
    }
}
