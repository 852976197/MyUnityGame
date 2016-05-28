using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

   public enum state {Inactive,Active,Locked};
   public state status;

	void Update () {
	    if(status == state.Active) {
         //animation add later;
      }
	}

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player"))
         GameManager.Instance.UpdateCheckPoints(this.gameObject);
   }
}
