using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

   public GameObject playerPrefab;   
   public bool gameOver;
   public bool changeView;
   public GameObject[] checkPoints;

   [SerializeField]
   private GameObject deadBody,player;
   private TimeManager time;
   private bool blink;
   private float blinkTime;
   private Transform spawnPoint;
   private Text respawnText;

   void Awake () {
      if (Instance == null) {
         this.transform.parent = null;

         Instance = this;
      }
      else {
         Destroy(this.gameObject);
         return;
      }

      //DontDestroyOnLoad(gameObject);
      time = GetComponent<TimeManager>();
      checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
      spawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
      respawnText = GameObject.Find("respawnText").GetComponent<Text>();
   }

   public static GameManager Instance { get; private set; }

   void Start() {
      Time.timeScale = 1;
      Respawn();
   }

   void OnLevelWasLoaded(int level) {
      // later
   }
	
	void Update () {
      if (gameOver && Time.timeScale == 0) {
         blinkTime++;
         if (blinkTime % 40 == 0) {
            blink = !blink;
         }
         respawnText.canvasRenderer.SetAlpha(blink ? 0 : 1);
         if ( Input.anyKeyDown) {
            time.ManipulateTime(1, 1f);
            foreach (GameObject cp in checkPoints) {
               if (cp.GetComponent<CheckPoint>().status == CheckPoint.state.Active) {
                  var checkPoint = cp.GetComponent<Transform>();
                  var checkPos = new Vector3(checkPoint.position.x, checkPoint.position.y + 10, 0);
                  if(player == null)
                  player = Instantiate(playerPrefab, checkPos, Quaternion.identity) as GameObject;
               }
            }
            Respawn();
         }

         if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);
      }
      else if (Input.GetKeyDown(KeyCode.C)) {
            changeView = !changeView;
      }
    }

   void OnPlayerKilled() {

      var playerDestroyScript = playerPrefab.GetComponent<CharController>();
      playerDestroyScript.DestroyCallBack -= OnPlayerKilled;

      time.ManipulateTime(0, 15f);

      gameOver = true;

      respawnText.text = "PRESS ANY BUTTON TO RESPAWN";
   }

   void Respawn() {
      gameOver = false;
      respawnText.canvasRenderer.SetAlpha(0);

      if (player == null)
         player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity) as GameObject;

      var playerDestroyScript = player.GetComponent<CharController>();
      playerDestroyScript.DestroyCallBack += OnPlayerKilled;

      if (deadBody != null) {
         Destroy(deadBody);
      }
   }

   public void UpdateCheckPoints(GameObject currentCheck) {
      foreach (GameObject cp in checkPoints) {
         if (cp.GetComponent<CheckPoint>().status == CheckPoint.state.Active)
            cp.GetComponent<CheckPoint>().status = CheckPoint.state.Locked;
      }
      currentCheck.GetComponent<CheckPoint>().status = CheckPoint.state.Active;
   }

   public void getDeadBody(GameObject dead) {
      deadBody = dead;
   }
}
