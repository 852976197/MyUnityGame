using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {

   public Transform target;
   public float xOffset, yOffset;
	
	void Update () {
      target = GameObject.Find("Player(Clone)").GetComponent<Transform>();
      if(target !=null)
      transform.position = new Vector2(target.transform.position.x+xOffset, target.transform.position.y+yOffset);
	}
}
