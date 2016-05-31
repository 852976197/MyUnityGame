using UnityEngine;
using System.Collections;

public class SimpleRoutine : MonoBehaviour {

   public Transform startPos;
   public Transform endPos;
   public float moveSpeed;
   public bool monster;

   private Vector2 tempPos;
   [SerializeField]
   private bool swap = false;
   void Update () {

      if (transform.position != endPos.position && !swap) {
         if (monster)
            transform.localScale = new Vector3(1, 1, 1);

         tempPos = endPos.position;
      }
      else {
         swap = true;
         tempPos = startPos.position;
      }

      if (transform.position !=startPos.position && swap) {
         if(monster)
            transform.localScale = new Vector3(-1, 1, 1);

         tempPos = startPos.position;
      }else {
         swap = false;
         tempPos = endPos.position;
      }

     if (!GameManager.Instance.winLevel)
     transform.position = Vector2.MoveTowards(transform.position, tempPos, moveSpeed * Time.deltaTime);
   }
}
