using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

   public Vector4 camLimit;
   public GameObject background;

   private float camWidth;
   private float camHeight;
   private float minX, minY,maxX,maxY;
   private Transform target;

   void Awake() {
      background.SetActive(true);
   }

   void Start() {
      camHeight = Camera.main.orthographicSize;
      camWidth = Camera.main.orthographicSize * Camera.main.aspect;
      minX = camLimit.x + camWidth;
      minY = camLimit.y + camHeight;
      maxX = camLimit.z - camWidth;
      maxY = camLimit.w - camWidth;
   }

   void Update() {
      if (GameObject.Find("Player(Clone)")) 
         target = GameObject.Find("Player(Clone)").GetComponent<Transform>();
      else
         target = GameObject.Find("PlayerDeath(Clone)").GetComponent<Transform>();

      var posX = Mathf.Clamp(target.position.x, minX, maxX);
      var posY = Mathf.Clamp(target.position.y, minY, maxY);
      transform.position = new Vector3(posX, posY, -100);
   }
}
