using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Plant))]
public class PlantEditor : Editor
{
    //SerializedProperty typeProp;

    //SerializedProperty ageProp;
    SerializedProperty yieldProp;
    SerializedProperty valueProp;

    //SerializedProperty yieldPerDayProp;
    //SerializedProperty maxYieldProp;

    SerializedProperty plantStateSeedProp;
    SerializedProperty plantStateGermProp;
    SerializedProperty plantStateSmallProp;
    SerializedProperty plantStateBigProp;
    SerializedProperty plantStateFlowerProp;
    SerializedProperty plantStateSmallFruitProp;
    SerializedProperty plantStateFruitProp;
    SerializedProperty plantStateDeadProp;
    SerializedProperty plantStateRemovedProp;
    SerializedProperty plantStateWinterProp;

    SerializedProperty droppedFruitProp;
    SerializedProperty droppedSeedProp;

    void OnEnable()
    {
        // Setup the SerializedProperties
        //typeProp = serializedObject.FindProperty("type");

        //ageProp = serializedObject.FindProperty("data.age");
        yieldProp = serializedObject.FindProperty("data.yield");
        valueProp = serializedObject.FindProperty("data.value");

        //yieldPerDayProp = serializedObject.FindProperty("yieldPerDay");
        //maxYieldProp = serializedObject.FindProperty("maxYield");

        plantStateSeedProp = serializedObject.FindProperty("seed");
        plantStateGermProp = serializedObject.FindProperty("germ");
        plantStateSmallProp = serializedObject.FindProperty("small");
        plantStateBigProp = serializedObject.FindProperty("big");
        plantStateFlowerProp = serializedObject.FindProperty("flower");
        plantStateSmallFruitProp = serializedObject.FindProperty("smallFruit");
        plantStateFruitProp = serializedObject.FindProperty("fruit");
        plantStateDeadProp = serializedObject.FindProperty("dead");
        plantStateRemovedProp = serializedObject.FindProperty("removed");
        plantStateWinterProp = serializedObject.FindProperty("winter");

        droppedFruitProp = serializedObject.FindProperty("droppedFruit");
        droppedSeedProp = serializedObject.FindProperty("droppedSeed");
    }

    public PlantType plantType;

    public override void OnInspectorGUI()
    {
        Plant plant = (Plant)target;

        plant.type = (PlantType)EditorGUILayout.EnumPopup("Type", plant.type);
        serializedObject.Update();

        switch(plant.type)
        {
            case PlantType.Bush:
                DrawBasicInfo(plant);

                plant.plantStates = new PlantState[10];
                PlantStateField(plant, 0, plantStateSeedProp, plant.seed, "Seed", false);
                PlantStateField(plant, 1, plantStateGermProp, plant.germ, "Germ", true);
                PlantStateField(plant, 2, plantStateSmallProp, plant.small, "Small", true);
                PlantStateField(plant, 3, plantStateBigProp, plant.big, "Big", true);
                PlantStateField(plant, 4, plantStateFlowerProp, plant.flower, "Flower", true);
                PlantStateField(plant, 5, plantStateSmallFruitProp, plant.smallFruit, "SmallFruit", true);
                PlantStateField(plant, 6, plantStateFruitProp, plant.fruit, "Fruit", true);
                PlantStateField(plant, 7, plantStateDeadProp, plant.dead, "Dead", true);
                PlantStateField(plant, 8, plantStateRemovedProp, plant.removed, "Removed", false);
                PlantStateField(plant, 9, plantStateWinterProp, plant.winter, "Winter", false);

                EditorGUILayout.PropertyField(droppedFruitProp, new GUIContent(name));
                break;
            case PlantType.Flower:
                DrawBasicInfo(plant);

                plant.plantStates = new PlantState[7];
                PlantStateField(plant, 0, plantStateSeedProp, plant.seed, "Seed", false);
                PlantStateField(plant, 1, plantStateGermProp, plant.germ, "Germ", true);
                PlantStateField(plant, 2, plantStateSmallProp, plant.small, "Small", true);
                PlantStateField(plant, 3, plantStateFlowerProp, plant.flower, "Flower", true);
                PlantStateField(plant, 4, plantStateFruitProp, plant.fruit, "Fruit", true);
                PlantStateField(plant, 5, plantStateDeadProp, plant.dead, "Dead", true);
                PlantStateField(plant, 6, plantStateRemovedProp, plant.removed, "Removed", false);

                EditorGUILayout.PropertyField(droppedFruitProp, new GUIContent(name));
                EditorGUILayout.PropertyField(droppedSeedProp, new GUIContent(name));
                break;
            case PlantType.Root:
                DrawBasicInfo(plant);

                plant.plantStates = new PlantState[7];
                PlantStateField(plant, 0, plantStateSeedProp, plant.seed, "Seed", false);
                PlantStateField(plant, 1, plantStateGermProp, plant.germ, "Germ", true);
                PlantStateField(plant, 2, plantStateSmallProp, plant.small, "Small", true);
                PlantStateField(plant, 3, plantStateFruitProp, plant.fruit, "Fruit", true);
                PlantStateField(plant, 4, plantStateFlowerProp, plant.flower, "Flower", true);
                PlantStateField(plant, 5, plantStateDeadProp, plant.dead, "Dead", true);
                PlantStateField(plant, 6, plantStateRemovedProp, plant.removed, "Removed", false);

                EditorGUILayout.PropertyField(droppedFruitProp, new GUIContent(name));
                EditorGUILayout.PropertyField(droppedSeedProp, new GUIContent(name));
                break;
            case PlantType.Tree:
                DrawBasicInfo(plant);

                plant.plantStates = new PlantState[10];
                PlantStateField(plant, 0, plantStateSeedProp, plant.seed, "Seed", false);
                PlantStateField(plant, 1, plantStateGermProp, plant.germ, "Germ", true);
                PlantStateField(plant, 2, plantStateSmallProp, plant.small, "Small", true);
                PlantStateField(plant, 3, plantStateBigProp, plant.big, "Big", true);
                PlantStateField(plant, 4, plantStateFlowerProp, plant.flower, "Flower", true);
                PlantStateField(plant, 5, plantStateSmallFruitProp, plant.smallFruit, "SmallFruit", true);
                PlantStateField(plant, 6, plantStateFruitProp, plant.fruit, "Fruit", true);
                PlantStateField(plant, 7, plantStateDeadProp, plant.dead, "Dead", true);
                PlantStateField(plant, 8, plantStateRemovedProp, plant.removed, "Removed", false);
                PlantStateField(plant, 9, plantStateWinterProp, plant.winter, "Winter", false);

                EditorGUILayout.PropertyField(droppedFruitProp, new GUIContent(name));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DrawBasicInfo(Plant plant)
    {
        EditorGUILayout.IntSlider(valueProp, 0, 200, new GUIContent("Value"));
        plant.data.age = EditorGUILayout.IntField("Age", plant.data.age);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.IntSlider(yieldProp, 0, plant.maxYield, new GUIContent("Yield", "Yield, Maximum Yield, YieldPerDay"));
        GUILayout.Label("/", GUILayout.Width(12));
        plant.maxYield = Mathf.Max(1, EditorGUILayout.IntField(plant.maxYield, GUILayout.Width(25)));
        GUILayout.Label("+", GUILayout.Width(12));
        plant.yieldPerDay = Mathf.Max(1, EditorGUILayout.IntField(plant.yieldPerDay, GUILayout.Width(25)));
        EditorGUILayout.EndHorizontal();

        if (plant.maxYield != 0)
            ProgressBar(plant.data.yield / (float)plant.maxYield, "Yield");
    }

    void PlantStateField(Plant plant, int plantStateIndex, SerializedProperty sp, Sprite s, string name, bool requiresAge)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(sp, new GUIContent(name, name + " Sprite, " + name + "required Age"));
        Rect rect = GUILayoutUtility.GetRect(50, 16, "TextField");
        if (requiresAge)
        {
            plant.plantStateRequiredAge[plantStateIndex] = Mathf.Max(plant.plantStateRequiredAge[plantStateIndex-1]+1, EditorGUI.IntField(rect, plant.plantStateRequiredAge[plantStateIndex]));
            //plant.plantStates[plantStateIndex].requiredAge = EditorGUI.IntField(rect, plant.plantStates[plantStateIndex].requiredAge);
        }
        plant.plantStates[plantStateIndex] = new PlantState(s, plant.plantStateRequiredAge[plantStateIndex]);
        //plant.plantStates[plantStateIndex].sprite = plant.seed;
        EditorGUILayout.EndHorizontal();
    }

    void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}

//[CustomPropertyDrawer(typeof(PlantState))]
//class PlantStateDrawer : PropertyDrawer
//{

//    // Draw the property inside the given rect
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        // Using BeginProperty / EndProperty on the parent property means that
//        // prefab override logic works on the entire property.
//        EditorGUI.BeginProperty(position, label, property);

//        // Draw label
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//        // Don't make child fields be indented
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;

//        // Calculate rects
//        Rect amountRect = new Rect(position.x, position.y, 30, position.height);
//        Rect unitRect = new Rect(position.x + 35, position.y, 50, position.height);

//        // Draw fields - passs GUIContent.none to each so they are drawn without labels
//        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("sprite"), GUIContent.none);
//        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("requiredAge"), GUIContent.none);

//        // Set indent back to what it was
//        EditorGUI.indentLevel = indent;

//        EditorGUI.EndProperty();
//    }
//}