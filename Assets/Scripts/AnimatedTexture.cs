using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour {

   public Vector2 speed;

   private Vector2 offset = Vector2.zero;
   private Material material;

	void Start () {
      material = this.GetComponent<Renderer>().material;

      offset = material.GetTextureOffset("_MainTex");
	}
	
	void Update () {
      offset += speed * Time.deltaTime;

      material.SetTextureOffset("_MainTex", offset);
	}
}
