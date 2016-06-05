using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPrintEffect : MonoBehaviour {

   public Text text;
   public string displayText;
   public float timeInterval;
   public bool destoryAfter;
   public bool blinkText,active = true;
   public bool activeOnTrigger;
   public bool Y, X;
   public Transform trigger;

   private bool blink;
   private float blinkTime;
   private GameObject player;
   private CameraFollow cam;

   void Awake() {
      cam = Camera.main.GetComponent<CameraFollow>();
   }

   void Update() {
      if (active && !blinkText && !activeOnTrigger && !cam.activeMap) {
         StartCoroutine(PrintText());
         active = false;
      }

      if (blinkText) {
         Blink();
      }

      if (activeOnTrigger && Y) {
         player = GameObject.Find("Player(Clone)");
         if(player != null&& trigger.position.y < player.transform.position.y) {
            StartCoroutine(PrintText());
            activeOnTrigger = false;
         }
      }else if(activeOnTrigger && X) {
         player = GameObject.Find("Player(Clone)");
         if (player != null && trigger.position.x < player.transform.position.x) {
            StartCoroutine(PrintText());
            activeOnTrigger = false;
         }
      }
   }

   IEnumerator PrintText() {
      var tempString = "";
      for (int i = 0; i < displayText.Length; i++) {
         tempString += displayText[i];
         if(displayText[i] != ' ')
         yield return new WaitForSeconds(timeInterval);
         text.text = tempString;
      }
      if (destoryAfter) {
         yield return new WaitForSeconds(1.5f);
         Destroy(this.gameObject);
      }
   }

   void Blink() {
      blinkTime++;
      if (blinkTime % 40 == 0) {
         blink = !blink;
      }
      text.canvasRenderer.SetAlpha(blink ? 0 : 1);
   }
}
