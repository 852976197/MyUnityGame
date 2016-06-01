using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ShowButtonText : MonoBehaviour, IPointerEnterHandler {
   //, IPointerExitHandler

   public Text showText;

   private float timeElapsed = 0;

   public void OnPointerEnter(PointerEventData eventData) {
      showText.gameObject.SetActive(true);
   }

   /*public void OnPointerExit(PointerEventData eventData) {
      showText.gameObject.SetActive(false);
   }*/
}
