using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {

   public int addAmount;

   private bool active = true;

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player") && active) {
         active = false;
         StartCoroutine(AddOneByOne());
      }
   }

  IEnumerator AddOneByOne() {
      for(int i = 0; i<addAmount; i++) {
         GameManager.Instance.curCoinNum ++;
         yield return new WaitForSeconds(0.1f);
      }
      Destroy(gameObject);
   }
}
