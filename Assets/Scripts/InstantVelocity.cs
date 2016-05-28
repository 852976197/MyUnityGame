using UnityEngine;
using System.Collections;

public class InstantVelocity : MonoBehaviour {

   public Vector2 randomSpeed;
   public float shiftSpeed;

   private Rigidbody2D body2d;

	void Awake () {
      body2d = this.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
      var speed = Random.Range(randomSpeed.x, randomSpeed.y);
      body2d.velocity = (new Vector2(shiftSpeed * Time.deltaTime, -speed *Time.deltaTime));
	}
}
