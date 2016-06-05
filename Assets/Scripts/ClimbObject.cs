using UnityEngine;
using System.Collections;

public class ClimbObject : MonoBehaviour {

   void OnTriggerStay2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
         other.gameObject.GetComponent<CharController>().isOnLadder = true;
      }
   }
}
