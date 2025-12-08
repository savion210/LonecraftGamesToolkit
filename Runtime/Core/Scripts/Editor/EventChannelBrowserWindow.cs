#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using LonecraftGames.Toolkit.Core.Events;
using UnityEditor;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Editor
{
    /// <summary>
    /// An Editor Window that scans the project for all ScriptableObjects inheriting
    /// from EventChannel<T> (including VoidEventChannel) and lists them.
    /// This provides developers with an overview of all event-based communication
    /// hubs in the game.
    /// </summary>
    public class EventChannelBrowserWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private List<ScriptableObject> _allEvents;
        private string _searchString = "";

        [MenuItem("LonecraftGames/Events/Event Channel Browser")]
        public static void ShowWindow()
        {
            // Get existing open window or create a new one
            EventChannelBrowserWindow window = GetWindow<EventChannelBrowserWindow>("Event Browser");
            window.minSize = new Vector2(300, 200);
        }

        private void OnEnable()
        {
            RefreshEventList();
        }

        /// <summary>
        /// Finds all assets that inherit from ScriptableObject and can be cast to EventChannel<T>.
        /// </summary>
        private void RefreshEventList()
        {
            // Find all unique GUIDs for assets that are ScriptableObjects
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
            _allEvents = new List<ScriptableObject>();

            // The EventChannel<T> base class is defined using reflection since we cannot query
            // for the generic type directly via AssetDatabase.
            System.Type eventChannelType = typeof(EventChannel<>);

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                if (so != null)
                {
                    System.Type assetType = so.GetType();

                    // Traverse the inheritance chain to see if it inherits from EventChannel<T>
                    while (assetType != null && assetType != typeof(object))
                    {
                        if (assetType.IsGenericType && assetType.GetGenericTypeDefinition() == eventChannelType)
                        {
                            _allEvents.Add(so);
                            break;
                        }

                        // Also specifically check for non-generic implementations like VoidEventChannel
                        if (assetType == typeof(VoidEventChannel))
                        {
                            _allEvents.Add(so);
                            break;
                        }

                        assetType = assetType.BaseType;
                    }
                }
            }

            Debug.Log($"Found {_allEvents.Count} unique Event Channel assets.");
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical(EditorStyles.inspectorDefaultMargins);

            DrawHeaderAndControls();

            if (_allEvents == null || _allEvents.Count == 0)
            {
                EditorGUILayout.HelpBox(
                    "No Event Channel assets found in the project. Create one using: Create/LonecraftGames/Events/...",
                    MessageType.Info);
                GUILayout.EndVertical();
                return;
            }

            DrawEventList();

            GUILayout.EndVertical();
        }

        private void DrawHeaderAndControls()
        {
            EditorGUILayout.LabelField("Lonecraft Event Hub Status", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            // Search field
            _searchString = EditorGUILayout.TextField("Search:", _searchString);

            // Refresh button
            if (GUILayout.Button("Refresh", GUILayout.Width(80)))
            {
                RefreshEventList();
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space(5);
        }

        private void DrawEventList()
        {
            // Filter list based on search string
            List<ScriptableObject> filteredEvents;
            if (!string.IsNullOrEmpty(_searchString))
            {
                filteredEvents = _allEvents.Where(e =>
                    e.name.IndexOf(_searchString, System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                    e.GetType().Name.IndexOf(_searchString, System.StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }
            else
            {
                filteredEvents = _allEvents;
            }

            // Display current status
            EditorGUILayout.LabelField($"Showing {filteredEvents.Count} of {_allEvents.Count} registered events.",
                EditorStyles.miniLabel);

            // Scrollable area for the list
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            foreach (ScriptableObject eventSO in filteredEvents)
            {
                if (eventSO == null) continue;

                GUILayout.BeginHorizontal(GUI.skin.box);

                // 1. Ping Button/Label (Clicking the name selects and pings)
                if (GUILayout.Button(new GUIContent(eventSO.name, "Click to locate and inspect."), EditorStyles.label,
                        GUILayout.Width(200)))
                {
                    SelectAndFocusInspector(eventSO);
                }

                // 2. Type Info
                string eventTypeName = eventSO.GetType().Name;
                if (eventSO is EventChannel<Void>)
                {
                    eventTypeName = "VoidEventChannel";
                }
                else if (eventSO.GetType().IsGenericType)
                {
                    // Nicely format generic type name, e.g., EventChannel<GameState>
                    string genericArg = eventSO.GetType().GetGenericArguments()[0].Name;
                    eventTypeName = $"EventChannel<{genericArg}>";
                }

                EditorGUILayout.LabelField(eventTypeName, GUILayout.Width(180));

                // --- NEW ASSET CREATION BUTTON ---
                if (GUILayout.Button(
                        new GUIContent("New", "Create a new ScriptableObject asset instance of this type."),
                        GUILayout.Width(40)))
                {
                    CreateAssetInstance(eventSO.GetType());
                }
                // --- END NEW ASSET CREATION BUTTON ---

                // 3. Inspect Button (Explicitly call the selection function)
                if (GUILayout.Button("Inspect", GUILayout.Width(60)))
                {
                    SelectAndFocusInspector(eventSO);
                }

                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// Selects the given ScriptableObject and attempts to focus the Inspector window.
        /// </summary>
        private void SelectAndFocusInspector(ScriptableObject target)
        {
            // 1. Ping the object visually in the Project window
            EditorGUIUtility.PingObject(target);

            // 2. Set the active object (opens it in the Inspector)
            Selection.activeObject = target;

            // 3. Force the Inspector window to gain focus and be visible 
            EditorWindow inspector = GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.InspectorWindow"));
            if (inspector != null)
            {
                inspector.Focus();
            }
        }

        /// <summary>
        /// Prompts the user to save a new ScriptableObject asset of the specified type.
        /// </summary>
        private void CreateAssetInstance(System.Type type)
        {
            // Create a temporary in-memory instance of the ScriptableObject
            ScriptableObject instance = CreateInstance(type);

            // Generate a default file name
            string defaultName = $"{type.Name}.asset";

            // Use Unity's utility to display the save file dialog and create the asset
            // This is the correct way to handle asset creation in the Editor.
            ProjectWindowUtil.CreateAsset(instance, defaultName);
        }
    }
}
#endif