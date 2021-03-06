namespace AmazingTeam.MagicFight
{
    using Apex.LoadBalancing;
    using UnityEngine;

#if UNITY_5_3 || UNITY_2020
    using UnityEngine.SceneManagement;
#endif
    public class GameOverManager : MonoBehaviour
    {
        private static Animator _anim;                          

        private void Awake()
        {
            _anim = this.GetComponent<Animator>();
        }

        public static void GameOver()
        {
            _anim.SetTrigger("GameOver");
            LoadBalancer.defaultBalancer.ExecuteOnce(RestartLevel, 3f);
        }

        private static void RestartLevel()
        {
#if  UNITY_2020
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
#else
            Application.LoadLevel(Application.loadedLevel);
#endif
        }
    }
}