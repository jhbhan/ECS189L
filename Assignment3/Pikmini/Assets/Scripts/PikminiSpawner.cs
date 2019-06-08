using UnityEngine;
using System.Collections;

public class PikminiSpawner : MonoBehaviour
{
    [SerializeField] private GameObject MiniPrefab;

    void Update()
    {

        if (Input.GetButton("Jump"))
        {
            Instantiate(this.MiniPrefab, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}