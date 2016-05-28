using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

   private Animator myAni;

   void Awake () {
      myAni = this.GetComponent<Animator>();
	}
	
   public void UpdateSpeed (float currentSpeed) {
      myAni.SetFloat("Running", currentSpeed);
	}

   public void UpdateIsGrounded(bool isGrounded) {
      myAni.SetBool("isGrounded", isGrounded);
   }

   public void UpdateIsClimbing(bool isClimbing,float onLadder) {
      myAni.SetBool("isClimbing", isClimbing);
      myAni.SetFloat("onLadder", onLadder);
   }

   public void UpdateIsOnWall(bool isOnWall) {
      myAni.SetBool("isOnWall", isOnWall);
   }


}
