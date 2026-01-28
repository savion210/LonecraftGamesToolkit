using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LonecraftGames.Toolkit.Core
{
    public static class SceneLoader
    {
        public enum Scene
        {
            GameUI,
            GameScene,
            LoaderScene,
            StartupScene,
            Level0
        }

        public enum LevelScene
        {
            Level0,
            LevelOne,
            LevelTwo,
            LevelThree,
        }

        private static Scene _targetScene;

        public static void Load(Scene targetScene)
        {
            _targetScene = targetScene;

            SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
        }
        

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(_targetScene.ToString());
        }

        public static void LoadSceneAdditive(LevelScene targetScene)
        {
            SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Additive);
        }


        public static void MakeActiveLevelScene(LevelScene targetScene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetScene.ToString()));
        }

        public static string GetCurrentActiveScene()
        {
            return SceneManager.GetActiveScene().name;
        }


        public static IEnumerator LoadSceneAdditiveAndSetActive(LevelScene targetScene)
        {
            // Load the scene additively
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene.ToString(), LoadSceneMode.Additive);

            // Wait until the scene has finished loading
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Set the loaded scene as the active scene
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetScene.ToString()));
        }
        
    }
}