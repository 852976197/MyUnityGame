using UnityEngine;
using System.Collections;

public class TransparentPlatform : MonoBehaviour {

   public float timeDelay;
   public float activeTime;

   private float timeElapsed = 0f, time = 0f;
   private bool active = false, start = false;
   private SpriteRenderer sr;
   private EdgeCollider2D col;
   private Color tempCol;

   void Awake() {
      sr = GetComponent<SpriteRenderer>();
      col = GetComponent<EdgeCollider2D>();
   }
   void Start() {
      Color tempCol = sr.color;
      tempCol.a = 1f;
      sr.color = tempCol;
   }

   void Update() {
      if (start)
         TimeElapsed();

      if (active) {
         time += Time.deltaTime;
         if (time < activeTime) {
            tempCol.a = 0;
            sr.color = tempCol;
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
      tempCol.a = 1f;
      sr.color = tempCol;
   }

   void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.CompareTag("Player"))
         start = true;
   }
}
