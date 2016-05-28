using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour {

   public int texutreSize = 32;

	void Start () {
      var newWidth = Mathf.Ceil(Screen.width / texutreSize);
      var newHeight = Mathf.Ceil(Screen.height / texutreSize);

      transform.localScale = new Vector3(texutreSize * newWidth, texutreSize * newHeight, 100);

      this.GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 100);
   }
}
