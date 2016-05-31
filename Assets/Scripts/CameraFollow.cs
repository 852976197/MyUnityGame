using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

   public Vector4 camLimit;
   public float smoothTimeX, smoothTimeY;
   public GameObject background;

   private float minX, minY,maxX,maxY;
   private Transform target;
   private Vector2 velocity;

   void Awake() {
      background.SetActive(true);
   }

   void Start() {
      var camHeight = Camera.main.orthographicSize;
      var camWidth = Camera.main.orthographicSize * Camera.main.aspect;
      minX = camLimit.x + camWidth;
      minY = camLimit.y + camHeight;
      maxX = camLimit.z - camWidth;
      maxY = camLimit.w - camWidth;
   }

   void FixedUpdate() {
      if (GameObject.Find("Player(Clone)")) 
         target = GameObject.Find("Player(Clone)").GetComponent<Transform>();
      else
         target = GameObject.Find("PlayerDeath(Clone)").GetComponent<Transform>();

      var posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTimeX);
      var posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTimeY);

      if (!GameManager.Instance.winLevel) {
         posX = Mathf.Clamp(posX, minX, maxX);
         posY = Mathf.Clamp(posY, minY, maxY);
         transform.position = new Vector3(posX, posY, -100);
      }
   }
}
