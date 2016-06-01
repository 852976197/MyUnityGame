using UnityEngine;
using System.Collections;

public class DisableExpert : MonoBehaviour {

   public enum adjust { disableInExp, disableInBeg }
   public adjust select;
   public MonoBehaviour[] scripts;
   public bool disableSp;


   void Start() {
      if (LevelManager.Instance.exp) {
         if (select == adjust.disableInExp)
            this.gameObject.SetActive(false);
      }
      else {
         if (select == adjust.disableInBeg) {
            if (disableSp) {
               foreach (var sp in scripts) {
                  sp.enabled = false;
               }
            }
            else {
               this.gameObject.SetActive(false);
            }
         }
      }
   }
}
