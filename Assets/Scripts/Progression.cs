using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{
    [System.Serializable]
    private struct Goal
    {
        [SerializeField]
        private float score;

        public float Score 
        {
            get 
            { 
                return score; 
            }
        }
    }

    [SerializeField]
    private Slider progressionSlider = null;

    [Tooltip("Note the goals must be in order 0 -> smaller score, Lenght-1 -> max score")]
    [SerializeField]
    private Goal[] goals = null;

    [SerializeField]
    private float scoreMultiplier = 0.0f;

    private int currentGoalIndex;
    private float maxGoalScore;

    private void Awake()
    {
        maxGoalScore = goals[goals.Length-1].Score;
    }

    private void Update()
    {
        float scaledCurrentScore = MovableObject.Score * scoreMultiplier;

        if (scaledCurrentScore > goals[currentGoalIndex].Score)
        {
            currentGoalIndex++;
        }

        progressionSlider.value = scaledCurrentScore / maxGoalScore;
    }
}
