using UnityEngine;
using UnityEngine.UI;


namespace UI.InGameUIs
{
    [RequireComponent(typeof(Text))]
    public class ScoreTextController : MonoBehaviour
    {

        private int score;
        public string frontText;
        private Text scoreText;

        public void AddScore(int addScore)
        {
            score += addScore;
            scoreText.text = frontText + score.ToString();
        }

        public int GetScore()
        {
            return score;
        }

        // Use this for initialization
        void Start()
        {
            scoreText = GetComponent<Text>();
            score = 0;
            scoreText.text = frontText + score.ToString();
        }
    }
}