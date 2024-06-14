using UnityEngine;

public class TargetPool : MonoBehaviour
{
    public static TargetPool Instance;

    private GameObject targetObject;
    public GameObject targetPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        targetObject = Instantiate(targetPrefab);
        targetObject.SetActive(false);
    }

    public GameObject GetTarget()
    {
        if (!targetObject.activeInHierarchy)
        {
            targetObject.SetActive(true);
        }
        return targetObject;
    }

    public void ReturnTarget()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
