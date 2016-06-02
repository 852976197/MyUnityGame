using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

   public AudioClip winSound;

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
         AudioSource.PlayClipAtPoint(winSound, transform.position);
         GameManager.Instance.winLevel = true;
      }
   }
}
