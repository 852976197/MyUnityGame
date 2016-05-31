using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {

   public Transform target;
	
	void Update () {
      transform.position = new Vector2(target.transform.position.x, transform.position.y);
	}
}
