using System.Collections;
using System.Collections.Generic;
using Coords;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class CreatePlanetEditor : EditorWindow {

    public static CreatePlanetEditor instance;

    public static void ShowPallette() {
        instance = (CreatePlanetEditor) EditorWindow.GetWindow(typeof(CreatePlanetEditor));

        instance.titleContent = new GUIContent("Planet Editor");
    }

    private string nazev = "";
    private GameObject planet;
    private GameObject player;
    private GameObject camera;
    private GameObject gameState;
    private GameObject branoSystem;

    private Sprite planetBackground;

    private void OnGUI() {
        EditorGUILayout.LabelField("Scene name:");
        nazev = GUILayout.TextField(nazev);
        if (nazev == "")
        {
            nazev = "test";
        }
        EditorGUILayout.LabelField("Planet Image:");
        planetBackground = (Sprite)EditorGUILayout.ObjectField(planetBackground, typeof(Sprite), true);

        EditorGUILayout.LabelField("Planet Prefab:");
        planet = (GameObject) EditorGUILayout.ObjectField(planet, typeof(GameObject), true);

        EditorGUILayout.LabelField("Player Prefab:");
        player = (GameObject)EditorGUILayout.ObjectField(player, typeof(GameObject), true);

        EditorGUILayout.LabelField("Camera Prefab:");
        camera = (GameObject)EditorGUILayout.ObjectField(camera, typeof(GameObject), true);

        EditorGUILayout.LabelField("gameState Prefab:");
        gameState = (GameObject)EditorGUILayout.ObjectField(gameState, typeof(GameObject), true);

        EditorGUILayout.LabelField("branoSystem Prefab:");
        branoSystem = (GameObject)EditorGUILayout.ObjectField(branoSystem, typeof(GameObject), true);


        if (GUILayout.Button("Create!")) {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
            EditorSceneManager.SaveScene(scene, string.Format("Assets/{0}.unity", nazev));

            var planetObject = GameObject.Instantiate(planet);
            planetObject.GetComponent<SpriteRenderer>().sprite = planetBackground;

            var playerObject = GameObject.Instantiate(player);
            var cameraObject = GameObject.Instantiate(camera);
            var gameStateObject = GameObject.Instantiate(gameState);
            var branoSystemObject = GameObject.Instantiate(branoSystem);

            playerObject.GetComponent<PlayerController>().cameraTransform = cameraObject.transform;
            playerObject.GetComponent<PlayerController>().branoTransform = branoSystemObject.transform;

            playerObject.transform.parent = planetObject.transform;
            gameStateObject.transform.parent = planetObject.transform;
            cameraObject.transform.parent = planetObject.transform;
            branoSystemObject.transform.parent = planetObject.transform;



        }
    }

    [MenuItem("Tools/Level Creator/Show Planet Editor")]
    private static void ShowPlanetEditor() {
        CreatePlanetEditor.ShowPallette();
    }
}
