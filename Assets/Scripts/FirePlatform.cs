using UnityEngine;
using System.Collections;

public class FirePlatform : MonoBehaviour {

   public float timeDelay;
   public float activeTime;

   private float timeElapsed = 0f, time = 0f;
   private BoxCollider2D col;
   private SpriteRenderer sp;
   private bool active = false,start = false;
	void Awake () {
      col = GetComponent<BoxCollider2D>();
      sp = GetComponent<SpriteRenderer>();
	}

   void Start() {
      col.enabled = false;
      sp.enabled = false;
      tag = "Untagged";
   }
	
	// Update is called once per frame
	void Update () {
      if (start) {
         TimeElapsed();
      }

      if (active) {
         time += Time.deltaTime;
         if (time < activeTime) {
            col.enabled = true;
            sp.enabled = true;
            tag = "Deadly";
         }
         else
            Reset();
      }
	}

   void TimeElapsed() {
      timeElapsed += Time.deltaTime;
      if(timeElapsed> timeDelay) {
         active = true;
         start = false;
      }
   }

   void Reset() {
      active = false;
      timeElapsed = 0f;
      time = 0f;
      col.enabled = false;
      sp.enabled = false;
      tag = "Untagged";
   }

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) {
         start = true;
      }
   }
}
