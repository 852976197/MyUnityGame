using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
         SceneManager.LoadScene(1);
      }
   }
}
