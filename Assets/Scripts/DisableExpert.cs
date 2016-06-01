using UnityEngine;
using System.Collections;

public class DisableExpert : MonoBehaviour {

	void Awake () {
      if(LevelManager.Instance.exp)
      this.gameObject.SetActive(false);
	}
}
