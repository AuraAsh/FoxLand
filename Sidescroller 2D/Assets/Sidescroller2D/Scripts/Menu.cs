using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
   public void LoadScene (string Nature_assets)
   {
        SceneManager.LoadSceneAsync(Nature_assets);
   }
   public void Exit()
   {
        Application.Quit();
        Debug.Log("You Left");
   }
}
