using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraFollow : MonoBehaviour {

   public Vector4 camLimit;
   public float smoothTimeX, smoothTimeY;
   public GameObject background, startText;
   public GameObject[] camPos;
   public float moveSpeed = 60;
   public bool activeMap = true;

   private float minX, minY, maxX, maxY;
   private Transform target;
   private Vector2 velocity;
   private int index = 0;

   void Awake() {
      if(background != null)
      background.SetActive(true);

      camPos = GameObject.FindGameObjectsWithTag("camPos").OrderBy(x => x.name).ToArray();
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
      if (!activeMap) {
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
      else {
         if (index < camPos.Length && activeMap) {
            target = camPos[index].transform;
            var posX = Mathf.Clamp(target.position.x, minX, maxX);
            var posY = Mathf.Clamp(target.position.y, minY, maxY);
            target.position = new Vector3(posX, posY, -100);
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            startText.SetActive(true);
            if (transform.position == target.position)
               index++;
         }
      }
   }

   void Update() {
      if (activeMap && Input.GetKeyDown(KeyCode.Return)){
         transform.position = GameObject.Find("Player(Clone)").GetComponent<Transform>().position;
         startText.SetActive(false);
         activeMap = false;
      }
   }
}

