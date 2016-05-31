using UnityEngine;
using System.Collections;

public class SimpleRoutine : MonoBehaviour {

   public Transform startPos;
   public Transform endPos;
   public float moveSpeed;

   private Vector2 tempPos;
   [SerializeField]
   private bool swap = false;
   void Update () {

      if (transform.position != endPos.position && !swap)
         tempPos = endPos.position;
      else {
         swap = true;
         tempPos = startPos.position;
      }

      if (transform.position !=startPos.position && swap) {
         tempPos = startPos.position;
      }else {
         swap = false;
         tempPos = endPos.position;
      }

     if (!GameManager.Instance.winLevel)
     transform.position = Vector2.MoveTowards(transform.position, tempPos, moveSpeed * Time.deltaTime);
   }
}
