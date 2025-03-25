using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] objects;

    public void Lose()
    {
        objects[0].gameObject.SetActive(true);
    }

    public void Victory()
    {
        objects[1].gameObject.SetActive(true);
    }
}
