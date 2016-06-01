using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Selector : MonoBehaviour {

   public void setBeg() {
      LevelManager.Instance.setBeg();
   }

   public void setExp() {
      LevelManager.Instance.setExp();
   }

   public void LoadLeve() {
      SceneManager.LoadScene(1);
   }
}
