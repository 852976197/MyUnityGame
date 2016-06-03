using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPrintEffect : MonoBehaviour {

   public Text text;
   public string displayText;
   public float timeInterval;
   public bool destoryAfter;
   public bool blinkText;

   private bool blink;
   private float blinkTime;

   void Start () {
      if(!blinkText)
      StartCoroutine(PrintText());
   }

   void Update() {
      if (blinkText) {
         Blink();
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
