using UnityEngine;

public class GirlfriendSpriteSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] safeGameObjects = null;
    [SerializeField]
    private GameObject[] notSafeGameObjects = null;

    [SerializeField]
    private bool isSafeToWork = false; 

    private void Awake()
    {
        for (int s = 0; s < safeGameObjects.Length; s++)
        {
            safeGameObjects[s].SetActive(isSafeToWork);
        }

        for (int s = 0; s < notSafeGameObjects.Length; s++)
        {
            notSafeGameObjects[s].SetActive(!isSafeToWork);
        }
    }
}
