using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

   public bool beg, exp;

   void Awake() {
      if (Instance == null) {
         Instance = this;
      }
      else {
         Destroy(this.gameObject);
         return;
      }

      DontDestroyOnLoad(this);
   }

   public void setBeg() {
      exp = false;
      beg = true;
   }

   public void setExp() {
      beg = false;
      exp = true;
   }


   public static LevelManager Instance { get; private set; }
}
