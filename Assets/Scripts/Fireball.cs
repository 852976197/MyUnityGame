using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

   public Vector2 randomForce;
   public float delayTime;

   private Rigidbody2D body2d;
   private bool turn = false, fired = false;
   private float timeElapsed = 0;

   void Awake() {
      body2d = this.GetComponent<Rigidbody2D>();
   }

   void Start() {
      body2d.gravityScale = 0;
   }

   void Update() {
      timeElapsed += Time.deltaTime;
      if(timeElapsed > delayTime) {
         body2d.gravityScale = 1;
         Fire();
      }

      if (body2d.velocity.y < 0 && !turn) {
         transform.Rotate(new Vector3(0, 0, 180));
         turn = true;
      }
   }

   void Fire() {
      if (!fired) {
         float forceY = Random.Range(randomForce.x, randomForce.y);
         body2d.AddForce(new Vector2(0, forceY));
         fired = true;
      }
      if(gameObject!=null && timeElapsed > 7f) {
         Destroy(gameObject);
      }
   }

   void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.CompareTag("Border")){
         Destroy(gameObject);
      }
   }
}
