using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
         //add effect later;
         Debug.Log("Ok");
         Destroy(gameObject);
      }
   }
}
