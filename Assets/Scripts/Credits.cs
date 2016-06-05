using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour {
   public float speed;
   public Text[] text;
   public float posY;
   public float delay;

   void Update() {
      if(text[4].transform.position.y < posY) {
         foreach (var t in text) {
            t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y + speed * Time.deltaTime, 0);
         }
         StartCoroutine(WaitFor());
      }

      if (Input.GetKey(KeyCode.Escape))
         SceneManager.LoadScene(0);
   }

   IEnumerator WaitFor() {
      yield return new WaitForSeconds(delay);
      StartCoroutine(WaitFor());
   }
   

}