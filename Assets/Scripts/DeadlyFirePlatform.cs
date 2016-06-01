using UnityEngine;
using System.Collections;

public class DeadlyFirePlatform : MonoBehaviour {

   public float timeDelay;
   public float activeTime;
   public GameObject[] fires;

   private GameObject defaultfire;
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
            defaultfire.SetActive(true);
            foreach (var fire in fires)
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
      }
   }

   void Reset() {
      active = false;
      start = false;
      timeElapsed = 0f;
      time = 0f;
      defaultfire.SetActive(false);
      foreach (var fire in fires)
         fire.SetActive(false);
   }

   void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.CompareTag("Player") && !start) {
         start = true;
         if (defaultfire != null)
            defaultfire.SetActive(false);
         else {
            defaultfire = Instantiate(fires[0], new Vector3(transform.position.x, transform.position.y + 14f, 0), Quaternion.identity) as GameObject;
            defaultfire.SetActive(false);
         }
      }
   }
}
