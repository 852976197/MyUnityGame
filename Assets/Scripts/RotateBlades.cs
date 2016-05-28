using UnityEngine;
using System.Collections;

public class RotateBlades : MonoBehaviour {

   public float rotateSpeed = 0;

	void Update () {
      transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
