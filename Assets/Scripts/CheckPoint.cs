using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

   public enum state {Inactive,Active,Locked};
   public state status;
   public Sprite[] activeStatus;
   public AudioClip check;

   private SpriteRenderer sp;

   void Start() {
      sp= GetComponent<SpriteRenderer>();
   }

	void Update () {
	    if(status == state.Active)
         sp.sprite = activeStatus[1];
        else
         sp.sprite = activeStatus[0];
	}

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
         if (status == state.Inactive) {
            AudioSource.PlayClipAtPoint(check, transform.position);
         }
         GameManager.Instance.UpdateCheckPoints(this.gameObject);
      }
   }
}
