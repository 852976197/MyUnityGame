using UnityEngine;
using System.Collections;

public class ActivePlatform : MonoBehaviour {

   public MonoBehaviour script;
   private Vector3 defaultPos;
   private bool active;
   void Awake () {
      defaultPos = this.gameObject.transform.position;
	}

   void Update() {
      if (GameManager.Instance.gameOver) {
         Debug.Log("reset");
         script.enabled = false;
         transform.position = defaultPos;
      }
   }
	
   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
         Debug.Log("ok");
         script.enabled = true;
      }
   }
}
