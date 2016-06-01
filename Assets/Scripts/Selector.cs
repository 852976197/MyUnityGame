using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Selector : MonoBehaviour {

   public void setBeg() {
      LevelManager.Instance.setBeg();
      SceneManager.LoadScene(1);
   }

   public void setExp() {
      LevelManager.Instance.setExp();
      SceneManager.LoadScene(1);
   }
}
