using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

   public float moveSpeed, jumpSpeed, climbSpeed, slideSpeed, slideMulti;
   public Vector2 wallJump;
   public LayerMask JumpDetectlayers;
   public LayerMask Walldetectlayers;
   public Transform bodyTrans, leftFoot, rightFoot, wallDetect,roofDetect;
   public GameObject playerDeadEffect, dustEffect, slideEffect;
   public BoxCollider2D[] boxCols;
   public CircleCollider2D[] circleCols;

   [SerializeField]//[HideInInspector]
   public bool jumpDown, isOnLadder, isClimbing, isOnWall,cantJump,isJumping;

   public delegate void onDestroy();
   public event onDestroy DestroyCallBack;

   [SerializeField]
   private bool isGrounded, airJump, wallJumping,moveActive;

   private float inputX, inputY, wallJumpDir = 0, timeElapsed = 0f, airJumpTime,wallJumpTime,difference;
   private Rigidbody2D body2d;
   private Animator animator;
   private AnimationController myAni;
   private Transform enter;

   void Awake() {
      body2d = this.GetComponent<Rigidbody2D>();
      animator = this.GetComponent<Animator>();
      myAni = this.GetComponent<AnimationController>();
   }

   void FixedUpdate() {
      if (this.gameObject != null && !GameManager.Instance.winLevel && !wallJumping) {
         MoveX(Input.GetAxisRaw("Horizontal"));
         MoveY(Input.GetAxisRaw("Vertical"));
         CheckIsGrounded();
         CheckIsOnWall();
         CheckCantJump();
      }

      if (inputX == 0 && moveActive) {
         if(!isJumping)
         transform.position = new Vector2(enter.position.x + difference, transform.position.y);
      }
      else {
         moveActive = false;
      }
   }

   void Update() {
      if (Input.GetButton("Jump") && !cantJump && !GameManager.Instance.winLevel) {
         Jump();       
      }

      if (wallJumping) {
         wallJumpTime += Time.deltaTime;
         if(wallJumpTime > 0.1f) {
            wallJumping = false;
            wallJumpTime = 0f;
         }
      }
   }

   void CheckCantJump() {
      cantJump = Physics2D.Linecast(transform.position, roofDetect.position, Walldetectlayers)? true: false;
   }

   void CheckIsOnWall() {
      var nearWall = Physics2D.Linecast(transform.position, wallDetect.position, Walldetectlayers);

      isOnWall = (nearWall && !isGrounded) ? true : false;

      if (isOnWall) {
         isJumping = false;
         boxCols[0].enabled = false;
         boxCols[1].enabled = true;
         circleCols[0].enabled = false;
         circleCols[1].enabled = true;

         float velY = slideSpeed;
         if (inputY < 0)
            velY *= slideMulti;

         body2d.velocity = new Vector2(body2d.velocity.x, velY);

         timeElapsed += Time.deltaTime;
         if (timeElapsed > 0.15) {
            var pos = transform.position.y;
            var slide = Instantiate(slideEffect, transform.position, Quaternion.identity) as GameObject;
            slide.transform.localScale = transform.localScale;
            timeElapsed = 0;
         }

         if (wallJumpDir == 0)
            wallJumpDir = -transform.localScale.x;
      }
      else {
         boxCols[0].enabled = true;
         boxCols[1].enabled = false;
         circleCols[0].enabled = true;
         circleCols[1].enabled = false;
         wallJumpDir = 0;
      }

      myAni.UpdateIsOnWall(isOnWall);
   }

   void CheckIsGrounded() {
      isGrounded = (Physics2D.Linecast(bodyTrans.position, leftFoot.position, JumpDetectlayers)
      || Physics2D.Linecast(bodyTrans.position, rightFoot.position, JumpDetectlayers)) ? true : false;

      if (isGrounded) {
         isJumping = false;
         airJumpTime = 0;
      }
      else {
         airJumpTime += Time.deltaTime;
         airJump = (airJumpTime < 0.2f) ? true : false;
      }

      myAni.UpdateIsGrounded(isGrounded);
   }

   public void MoveX(float horizontalInput) {
      Vector2 moveVel = body2d.velocity;
      inputX = horizontalInput;
      myAni.UpdateSpeed(horizontalInput);

      if (inputX != wallJumpDir && !isOnWall) {
         body2d.velocity = new Vector2(inputX * moveSpeed * Time.deltaTime, moveVel.y);
         gameObject.transform.localScale = new Vector3(horizontalInput, 1, 1);
      }
   }

   public void MoveY(float verti) {
      inputY = verti;

      if (isOnLadder && inputY != 0) {
         isClimbing = true;
         body2d.gravityScale = 0;
         body2d.velocity = Vector2.zero;
         transform.Translate(0, inputY * climbSpeed * Time.deltaTime, 0);
      }
      else
         isClimbing = false;

      myAni.UpdateIsClimbing(isClimbing, body2d.gravityScale);
   }

   public void Jump() {
      if ((isGrounded || airJump) && body2d.velocity.y <= 0) {
         isJumping = true;
         body2d.velocity = new Vector2(body2d.velocity.x, jumpSpeed);
         Instantiate(dustEffect, rightFoot.position, Quaternion.identity);
      }
      else if (isOnWall && inputX == wallJumpDir) {
         if (!wallJumping) {
            isJumping = true;
            body2d.velocity = new Vector2(wallJumpDir * wallJump.x, wallJump.y);
            wallJumpDir = 0;
            Instantiate(dustEffect, wallDetect.position, Quaternion.identity);
            wallJumping = true;
         }
      }
   }

   //move with moving platform not perfect yet.
   void OnCollisionStay2D(Collision2D other) {
      if (other.gameObject.CompareTag("MovingPlatform")) {
         if (!moveActive) {
            enter = other.transform;
            difference = this.gameObject.transform.position.x - enter.position.x;
            moveActive = true;
         }
      }
   }

   void OnTriggerExit2D(Collider2D other) {
      if (other.gameObject.CompareTag("Stair")) {
         isOnLadder = false;
         body2d.gravityScale = 1;
      }
   }

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Deadly")) {
         var dead = Instantiate(playerDeadEffect, new Vector3(transform.position.x, transform.position.y + 5, 0), Quaternion.identity) as GameObject;
         dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
         GameManager.Instance.getDeadBody(dead);
         Destroy(this.gameObject);

         if(DestroyCallBack != null) {
            DestroyCallBack();
         }
      }
   }
}
