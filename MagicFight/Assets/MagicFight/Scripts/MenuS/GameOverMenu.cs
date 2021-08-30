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
            SceneManager.LoadScene("MagicFight_2(Arena)", LoadSceneMode.Single);
            //LoadBalancer.defaultBalancer.ExecuteOnce(RestartLevel, 4f);           
        }

        protected override void Awake() {
            base.Awake();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<Player>();
        }

        // Start is called once 
        void Start() {

            GameObject.FindGameObjectWithTag("InControl").SetActive(false);
            if (_playerkilled.CurrentDeath == 3) {
                PlayerPrefs.SetInt("GameOver", 1);
                GameOverStatus.text = "You Win!";
            } else if (PlayerPrefs.GetInt("GameOver") == 1 || _player.currentHealth <= 0) {
                GameOverStatus.text = "You lose!";
            }
        }

        private static void RestartLevel() {
#if  UNITY_2020
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
#else
            Application.LoadLevel(Application.loadedLevel);
#endif
        }
    }
}
