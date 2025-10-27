using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class SceneUtils
    {
        public static void ReloadActiveScene()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }

        public static IEnumerator LoadSceneAsync(string name, Action<float> onProgress = null)
        {
            var op = SceneManager.LoadSceneAsync(name);
            while (!op.isDone)
            {
                onProgress?.Invoke(op.progress);
                yield return null;
            }
        }

        public static string GetActiveSceneName() => SceneManager.GetActiveScene().name;
    }
}