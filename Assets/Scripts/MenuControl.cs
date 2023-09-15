using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
   public void LoadMap()
   {
    // Play button pressed in mainmenu
    SceneManager.LoadScene("Map");
    

   }
   public void Save()
   {
      //This will run from menu when you press save 
      GameManager.manager.Save();
   }
   public void Load()
   {
      //Main menu Load button
      GameManager.manager.Load();
   }
}
