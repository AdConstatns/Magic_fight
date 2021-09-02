namespace AmazingTeam.MagicFight {
    
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using Apex.LoadBalancing;
    public class GameOverMenu : SimpleMenu<GameOverMenu> {
        public Text GameOverStatus;
        public Player _player;
        public PlayerKilled _playerkilled;
        public override void OnBackPressed() {
            PlayerPrefs.SetInt("GameOver", 0);          
            Hide();
            Destroy(this.gameObject); // This menu does not automatically destroy itself
            _playerkilled.CurrentDeath = 0;
            MainMenu.Show();
        }

        public void OnRestartPressed() {
            PlayerPrefs.SetInt("GameOver", 0);
            _playerkilled.CurrentDeath = 0;
            // To Disable the Spanning of Enemy and Powerups.
            GameObject.FindGameObjectWithTag("EntityManager").SetActive(false);
            // To Disable the player 
            _player.gameObject.SetActive(false);
            //SceneManager.LoadSceneAsync("MagicFight_2(Arena)", LoadSceneMode.Single);
            LoadBalancer.defaultBalancer.ExecuteOnce(RestartLevel, 1f);           
        }

        protected override void Awake() {
            base.Awake();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<Player>();
        }

        // Start is called once 
        void Start() {
            //Time.timeScale = 0;
            // To Disable the Touch Control (Left Stick)
            GameObject.FindGameObjectWithTag("InControl").SetActive(false);          
            if (_playerkilled.CurrentDeath == 3) {
                PlayerPrefs.SetInt("GameOver", 1);
                GameOverStatus.text = "You Win!";
                YSOCorpAnalytics.Instance.GameFinish(true);
                YSOCorpAnalytics.Level++;

            } else if (PlayerPrefs.GetInt("GameOver") == 1 || _player.currentHealth <= 0) {
                GameOverStatus.text = "You lose!";
                YSOCorpAnalytics.Instance.GameFinish(false);
            }
        }

        private static void RestartLevel() {
#if  UNITY_2020
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
#else
            Application.LoadLevel(Application.loadedLevel);
#endif
        }
    }
}
