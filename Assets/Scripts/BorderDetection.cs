using UnityEngine;
using System.Collections;

public class BorderDetection : MonoBehaviour {

   private Camera cam;
   private float camWidth;

   void Start() {
      cam = Camera.main;
      camWidth = cam.orthographicSize * 2 * cam.aspect;
      transform.localScale = new Vector3(camWidth, 10, 1);
   }

   void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject != null)
         Destroy(other.gameObject);
   }
}
