using UnityEngine;
using System.Collections;

public class FireBallSpawner : MonoBehaviour {

   public GameObject fireball;
   public Transform startPoint;
   public Transform endPoint;
   public Vector2 spawnRate;
   public Vector2 spawnDelay;

	void Start () {
      StartCoroutine(Spawner());
	}

   IEnumerator Spawner() {
      var Delay = Random.Range(spawnDelay.x, spawnDelay.y);
      var rate = Random.Range(spawnRate.x, spawnRate.y);
      for (int i = 0; i<(int)rate; i++) {        
         var rangeX = Random.Range(startPoint.position.x, endPoint.position.x);
         if (i > 0 && Random.Range(0f, 100f) < 50f)
            break;
         else {
            var fb = Instantiate(fireball, new Vector3(rangeX, transform.position.y, 0), Quaternion.identity) as GameObject;
            fb.transform.Rotate(new Vector3(0, 0, -90));
         }
      }
      yield return new WaitForSeconds(Delay);
      StartCoroutine(Spawner());
   }

}
