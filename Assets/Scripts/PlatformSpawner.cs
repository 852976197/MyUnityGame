using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {
   public GameObject[] myPrefab;
   public float dropDelay;
   public Vector2 spawnXRange, spawnYRange, spawnRate;

   private bool active = true;
   private float camWidth, camHeight, camPosY, lastPosY;

   void Start() {
      camWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
      camHeight = Camera.main.orthographicSize * 2f;
      camPosY = Camera.main.transform.position.y;
      transform.position = new Vector3(0, camPosY + camHeight * 1.5f + 10f, 0);
      lastPosY = transform.position.y + 50;
   }

   void Update() {
      camPosY = Camera.main.transform.position.y;
      if (camPosY > camHeight / 2) {
      }
   }

   void Generator() {
      if (active) {
         float beginPosY = transform.position.y;
         float randPosX = 0;


         while (beginPosY > lastPosY) {
            int randomNum = (int)Random.Range(spawnRate.x, spawnRate.y + 1);
            for (int col = 0; col < randomNum; col++) {
               var randomSelect = Random.Range(0, myPrefab.Length);
               //add gap between platform 
               if (col == 0)
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
         }
         lastPosY = transform.position.y;
      }
   }
}
