using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

   public GameObject playerPrefab;   
   public GameObject[] checkPoints;
   public int maxCoinNum, curCoinNum;
   public GameObject[] DeadEffect;

   [HideInInspector]
   public bool gameOver, changeView, winLevel=false;

   [SerializeField]
   private GameObject deadBody,player;
   private GameObject[] coinCount;
   private TimeManager time;
   private Transform spawnPoint;
   private bool blink;
   private float blinkTime,timer;
   private Transform s;
   private Text respawnText,levelText,coinText,winText,deathText;
   private int deathNum = 0;
   private string cpName,cpDisable;
   private AudioSource bgMusic;

   void Awake () {
      if (Instance == null) {
         this.transform.parent = null;

         Instance = this;
      }
      else {
         Destroy(this.gameObject);
         return;
      }

      #region GetComponent
      time = GetComponent<TimeManager>();
      coinCount = GameObject.FindGameObjectsWithTag("Coin");
      spawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
      respawnText = GameObject.Find("respawnText").GetComponent<Text>();
      levelText = GameObject.Find("LevelText").GetComponent<Text>();
      checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
      coinText = GameObject.Find("coinText").GetComponent<Text>();
      winText = GameObject.Find("WinText").GetComponent<Text>();
      deathText = GameObject.Find("DeathText").GetComponent<Text>();
      bgMusic = gameObject.GetComponent<AudioSource>();
      #endregion
   }

   public static GameManager Instance { get; private set; }

   void Start() {
      Time.timeScale = 1;
      winText.canvasRenderer.SetAlpha(0);
      deathText.canvasRenderer.SetAlpha(0);
      CoinCounter();
      Respawn();
   }
	
	void Update () {
      coinText.text = curCoinNum + "/" + maxCoinNum;

      if (gameOver) {
         if (Time.timeScale < 0.3f) {
            blinkTime++;
            if (blinkTime % 40 == 0) {
               blink = !blink;
            }
            respawnText.canvasRenderer.SetAlpha(blink ? 0 : 1);
            bgMusic.Stop();
         }

         if (Input.anyKeyDown && Time.timeScale == 0) {
            time.ManipulateTime(1, 1f);
            blinkTime = 0;
            deathNum++;

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
      }
      else if (Input.GetKeyDown(KeyCode.C))
            changeView = !changeView;

      if (Input.GetKey(KeyCode.Escape))
         SceneManager.LoadScene(0);

      Win();
    }

   void OnPlayerKilled() {

      var playerDestroyScript = playerPrefab.GetComponent<CharController>();
      playerDestroyScript.DestroyCallBack -= OnPlayerKilled;

      time.ManipulateTime(0, 15f);

      gameOver = true;

      respawnText.text = "PRESS ANY BUTTON TO RESPAWN";

      foreach(GameObject temp in DeadEffect) {
         temp.SetActive(true);
      }

      timer = 0f;
   }

   void Respawn() {
      bgMusic.Play();
      changeView = false;
      gameOver = false;
      respawnText.canvasRenderer.SetAlpha(0);

      foreach (GameObject temp in DeadEffect) {
         temp.SetActive(false);
      }

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

   public void Win() {
      if (winLevel) {
         bgMusic.Stop();
         player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
         player.GetComponent<Rigidbody2D>().gravityScale = 0;
         player.GetComponent<Animator>().Stop();
         winText.enabled = true;
         time.ManipulateTime(0, 5f);
         blinkTime++;
         if (blinkTime % 40 == 0) {
            blink = !blink;
         }
         winText.canvasRenderer.SetAlpha(blink ? 0 : 1);
         deathText.canvasRenderer.SetAlpha(1);
         var addS = "";
         addS = (deathNum > 1) ? "S" : "";

         deathText.text = "DEATH TIME" + addS + ": " + deathNum + "\n\n" + "PRESS 'ENTER' TO PLAY NEXT LEVEL";

         if (Input.GetKeyDown(KeyCode.Return)) {
            var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
         }
      }
   }

   void CoinCounter() {
      foreach (GameObject c in coinCount) {
         maxCoinNum++;
      }

      maxCoinNum += 5;
   }

   public void getDeadBody(GameObject dead) {
      deadBody = dead;
   }

   public Text getLevelText() {
      return levelText;
   }
}
