using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{
    [System.Serializable]
    private struct Goal
    {
        [SerializeField]
        private float score;
        [SerializeField]
        private GameObject[] dialogsToDeactivate;
        [SerializeField]
        private GameObject dialogToActivate;
        [SerializeField]
        private float amountOfObjectsToSpawn;
        [SerializeField]
        private MovableObject movableObjectToSpawn;

        public float Score 
        {
            get 
            { 
                return score; 
            }
        }
        public GameObject DialogToActivate 
        {
            get => dialogToActivate;
        }
        public float AmountOfObjectsToSpawn 
        {
            get => amountOfObjectsToSpawn;
        }
        public MovableObject MovableObjectToSpawn
        {
            get => movableObjectToSpawn; 
        }
        public GameObject[] DialogsToDeactivate
        {
            get => dialogsToDeactivate;
        }
    }

    [SerializeField]
    private Slider progressionSlider = null;
    [SerializeField]
    private TheSpawner theSpawner = null;

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

        Goal goal = goals[currentGoalIndex];
        if (scaledCurrentScore > goal.Score)
        {
            // Deactivate previous goal dialogs
            GameObject[] dialogsToDeactivate = goal.DialogsToDeactivate;
            if (dialogsToDeactivate != null)
            {
                for (int d = 0; d < dialogsToDeactivate.Length; d++)
                {
                    dialogsToDeactivate[d].SetActive(false);
                }
            }
            // Activate goal dialog
            GameObject dialogToActivate = goal.DialogToActivate;
            if (dialogToActivate != null)
            {
                dialogToActivate.SetActive(true);
            }

            theSpawner.SpawnRange(goal.MovableObjectToSpawn, goal.AmountOfObjectsToSpawn);

            if (currentGoalIndex < goals.Length - 1)
            {
                currentGoalIndex++;
            }
            else
            {
                this.enabled = false;
            }
        }

        progressionSlider.value = scaledCurrentScore / maxGoalScore;
    }
}
