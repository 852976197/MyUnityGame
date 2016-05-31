using UnityEngine;
using System.Collections;

public class DeadlyFirePlatform : MonoBehaviour {

   public float timeDelay;
   public float activeTime;
   public GameObject[] fires;

   private float timeElapsed = 0f, time = 0f;
   private bool active = false, start = false;

   void Start() {
      foreach (var fire in fires)
         fire.SetActive(false);
   }

   void Update() {
      if (start)
         TimeElapsed();

      if (active) {
         time += Time.deltaTime;
         if (time < activeTime) {
            foreach(var fire in fires)
               fire.SetActive(true);
         }
         else
            Reset();
      }
   }

   void TimeElapsed() {
      timeElapsed += Time.deltaTime;
      if (timeElapsed > timeDelay) {
         active = true;
         start = false;
      }
   }

   void Reset() {
      active = false;
      timeElapsed = 0f;
      time = 0f;
      foreach (var fire in fires)
         fire.SetActive(false);
   }

   void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.CompareTag("Player"))
         start = true;
   }
}
