using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

   public GameObject[] myPrefab;
   public float dropDelay;
   public Vector2 spawnXRange, spawnYRange, spawnRate;

   private bool active = true;
   private Camera cam;
   private float camWidth;

   void Start() {
      cam = Camera.main;
      camWidth = cam.orthographicSize * 2f * cam.aspect;
      StartCoroutine(ObjGenerator());
   }

   IEnumerator ObjGenerator() {
      yield return new WaitForSeconds(dropDelay);
      if (active) {
         float randPosX = 0;
         int randomNum = (int)Random.Range(spawnRate.x, spawnRate.y+1);

         for (int i = 0; i < randomNum; i++) {
            var randomSelect = Random.Range(0, myPrefab.Length);
            //add gap between platform 
            if (i == 0)
               randPosX += Random.Range(-camWidth / 2 * 0.85f, camWidth / 2 * 0.85f);
            else {
               var range = Random.Range(spawnXRange.x, spawnXRange.y);
               if (randPosX > 0)
                  randPosX -= range;
               else
                  randPosX += range;
            }
            var spawnObject = Instantiate(myPrefab[randomSelect], new Vector3(randPosX, transform.position.y, 1), Quaternion.identity) as GameObject;
            spawnObject.transform.SetParent(GameObject.Find("Platforms").transform);     
         }
         StartCoroutine(ObjGenerator());
      }
   }
}