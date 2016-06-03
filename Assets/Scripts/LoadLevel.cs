using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
     public void Loadlevel(int Levelindex) {
      SceneManager.LoadScene(Levelindex);
   }
}
