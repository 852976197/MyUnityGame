using UnityEngine;
using System.Collections;

public class JumpPlatform : MonoBehaviour {

   private Transform playerTrans;
   private EdgeCollider2D collider2d;
   private CharController controller;
   private bool disactive = false;

   void Update() {
      if (GameObject.Find("Player(Clone)") != null) {
         playerTrans = GameObject.Find("Player(Clone)").GetComponent<Transform>();
         controller = GameObject.Find("Player(Clone)").GetComponent<CharController>();
         collider2d = this.GetComponent<EdgeCollider2D>();
      }

      if (playerTrans != null) {
         if (this.transform.position.y + 6 < playerTrans.position.y - 14 && !disactive)
            collider2d.isTrigger = false;
         else
            collider2d.isTrigger = true;
      }
      else
         collider2d.isTrigger = false;
   }

   void OnCollisionStay2D(Collision2D other) {
      if (other.gameObject != null) {
         if (other.gameObject.CompareTag("Player")) {
            if (other.gameObject.GetComponent<CharController>().isClimbing)
               disactive = true;
            else
               disactive = false;
         }
      }
   }

   void OnTriggerExit2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
            disactive = false;
      }
   }


}
