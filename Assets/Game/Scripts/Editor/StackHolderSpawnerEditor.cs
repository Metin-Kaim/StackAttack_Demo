using UnityEditor;
using UnityEngine;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Datas; // ColorTypes burada tanımlı olduğunu varsayıyoruz

[CustomEditor(typeof(StackHolderSpawner))]
public class StackHolderSpawnerEditor : Editor
{
    // Rastgele config kontrolü için geçici alanlar
    private bool randomizeConfig = false;
    private Vector2 boundsOfSize;
    private Vector2 boundsOfMultiplier;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Random Config Options", EditorStyles.boldLabel);

        // Checkbox
        randomizeConfig = EditorGUILayout.Toggle("Randomize Config", randomizeConfig);

        // Eğer işaretliyse alt ayarları göster
        if (randomizeConfig)
        {
            EditorGUI.indentLevel++;
            boundsOfSize = EditorGUILayout.Vector2Field("Bounds Of Size", boundsOfSize);
            boundsOfMultiplier = EditorGUILayout.Vector2Field("Bounds Of Multiplier", boundsOfMultiplier);
            EditorGUI.indentLevel--;
        }
    }

    private void OnEnable() => SceneView.duringSceneGui += OnSceneGUI;
    private void OnDisable() => SceneView.duringSceneGui -= OnSceneGUI;

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        var spawner = (StackHolderSpawner)target;
        Camera cam = SceneView.currentDrawingSceneView.camera;

        // Unity’nin tıklamayla obje seçmesini engelle
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        // Mouse pozisyonunu world space'e çevir
        Vector3 mousePos = e.mousePosition;
        mousePos.y = cam.pixelHeight - mousePos.y;
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -cam.transform.position.z));
        worldPos.z = 0;

        // Kare önizleme
        Handles.color = new Color(0, 1, 0, 0.4f);
        float size = 1f;
        Vector3[] square = new Vector3[]
        {
            worldPos + new Vector3(-size / 2, -size / 2, 0),
            worldPos + new Vector3(-size / 2,  size / 2, 0),
            worldPos + new Vector3( size / 2,  size / 2, 0),
            worldPos + new Vector3( size / 2, -size / 2, 0)
        };
        Handles.DrawSolidRectangleWithOutline(square, new Color(0, 1, 0, 0.4f), Color.black);

        // Sol tıklama ile spawn
        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            if (spawner.stackHolderHandler != null)
            {
                var obj = PrefabUtility.InstantiatePrefab(spawner.stackHolderHandler) as StackHolderHandler;
                obj.transform.position = worldPos;
                obj.transform.SetParent(spawner.stackHolderContainer);

                // Config atama
                var config = spawner.Config;

                if (randomizeConfig)
                {
                    // Min-Max aralığında rastgele count
                    config.StackSize = (byte)Random.Range(boundsOfSize.x, boundsOfSize.y + 1);
                    config.SizeMultiplier = (byte)Random.Range(boundsOfMultiplier.x, boundsOfMultiplier.y + 1);

                    // Rastgele bir ColorType seç
                    var colors = (ColorTypes[])System.Enum.GetValues(typeof(ColorTypes));
                    config.ColorType = colors[Random.Range(0, colors.Length)];
                }

                obj.Config = config;

                Undo.RegisterCreatedObjectUndo(obj.gameObject, "Spawn StackHolder");
                Debug.Log($"✅ Spawned with Color: {config.ColorType}, Count: {config.StackSize}");
            }
            else
            {
                Debug.LogError("❌ stackHolderHandler prefab atanmadı!");
            }

            e.Use();
        }

        SceneView.RepaintAll();
    }
}
