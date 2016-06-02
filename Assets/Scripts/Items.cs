using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {

   public int addAmount;
   public AudioClip[] coinSound;

   private bool active = true;

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player") && active) {
         active = false;
         StartCoroutine(AddOneByOne());
      }
   }

  IEnumerator AddOneByOne() {
      for(int i = 0; i<addAmount; i++) {
         var random = Random.Range(0, coinSound.Length);
         GameManager.Instance.curCoinNum ++;
         AudioSource.PlayClipAtPoint(coinSound[random], transform.position,0.2f);
         yield return new WaitForSeconds(0.1f);
      }
      Destroy(gameObject);
   }
}
