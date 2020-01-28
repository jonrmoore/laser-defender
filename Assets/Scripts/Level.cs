using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayLoadTime = 3f;
   public void LoadGameScene(string name)
   {
       SceneManager.LoadScene(name);
   }

   public void LoadStartMenu()
   {
       SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);
   }

   public void LoadGameOver()
   {
       StartCoroutine(DelayLoad(delayLoadTime));
   }

   public void QuitGame()
   {
       Application.Quit();
   }

   private IEnumerator DelayLoad(float secs)
   {
       yield return new WaitForSeconds(secs);
       SceneManager.LoadScene("Game Over");
   }
}
