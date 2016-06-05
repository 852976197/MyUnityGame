using UnityEngine;
using System.Collections;

public class GodMode : MonoBehaviour {

   public bool activeGod;
   public float moveSpeed;
   public MonoBehaviour controller;
   public BoxCollider2D[] cols;
   public CircleCollider2D[] ccols;
   public GameObject view;

   private Rigidbody2D body2d;
   private float moveX;
   private float moveY;
   private CameraFollow cam;

   void Awake() {
      body2d = gameObject.GetComponent<Rigidbody2D>();
      cam = Camera.main.GetComponent<CameraFollow>();
   }

   void Start() {
      view = Instantiate(view, transform.position, Quaternion.identity) as GameObject;
      view.transform.SetParent(GameObject.Find("WordCanvas").GetComponent<Transform>());
   }

   void FixedUpdate() {
      if (activeGod) {
         moveX = Input.GetAxisRaw("Horizontal");
         moveY = Input.GetAxisRaw("Vertical");
         Move();
      }
      else {
         controller.enabled = true;
         body2d.isKinematic = false;
         for (int i = 0; i < cols.Length; i++) {
            cols[i].enabled = true;
            ccols[i].enabled = true;
            view.SetActive(false);
         }
      }
   }

   void Update() {
      if (Input.GetKeyDown(KeyCode.G) && !cam.activeMap)
         activeGod = !activeGod;
   }

   void Move() {
      controller.enabled = false;
      body2d.isKinematic = true;
      Vector2 moveVel = body2d.velocity;
      body2d.velocity = new Vector2(moveVel.x + moveX * moveSpeed * Time.deltaTime, moveVel.y + moveY * moveSpeed * Time.deltaTime);
      view.SetActive(true);
      if (moveX != 0)
         gameObject.transform.localScale = new Vector3(moveX, 1, 1);

      for (int i = 0; i < cols.Length; i++) {
         cols[i].enabled = false;
         ccols[i].enabled = false;
      }
   }
}
