using UnityEngine;
using System.Collections;

public class ChangeView : MonoBehaviour {

	public enum state {view1,view2};

    public state status;
    private Transform trans;

	void Awake () {
       trans = transform.GetComponent<Transform>();
	}

   void Update() {
      if (status == state.view1) {
         if (GameManager.Instance.changeView)
            trans.localScale = Vector3.zero;
         else
            trans.localScale = Vector3.one;
      }
      if (status == state.view2) {
         if (!GameManager.Instance.changeView)
            trans.localScale = Vector3.zero;
         else
            trans.localScale = Vector3.one;
      }
   }
}
