using System.Collections.Generic;
using UnityEngine;

public class KeyCombinationManager : MonoBehaviour
{
    public GameObject keyPrefab; // Prefab to instantiate when K + E + Y combine
    public float proximityThreshold = 1.1f; // Adjust based on object size

    void Update()
    {
        GameObject[] kObjects = GameObject.FindGameObjectsWithTag("K");
        GameObject[] eObjects = GameObject.FindGameObjectsWithTag("E");
        GameObject[] yObjects = GameObject.FindGameObjectsWithTag("Y");

        foreach (GameObject k in kObjects)
        {
            foreach (GameObject e in eObjects)
            {
                foreach (GameObject y in yObjects)
                {
                    if (AreTouchingSideBySide(k.transform.position, e.transform.position, y.transform.position))
                    {
                        Vector3 spawnPosition = (k.transform.position + e.transform.position + y.transform.position) / 3f;

                        // Spawn KEY first (preserve position before destroying)
                        Instantiate(keyPrefab, spawnPosition, Quaternion.identity);

                        // Destroy the original pieces
                        Destroy(k);
                        Destroy(e);
                        Destroy(y);

                        return; // Exit after one combination
                    }
                }
            }
        }
    }

    bool AreTouchingSideBySide(Vector3 posA, Vector3 posB, Vector3 posC)
    {
        List<Vector3> positions = new List<Vector3> { posA, posB, posC };

        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                if (Vector3.Distance(positions[i], positions[j]) > proximityThreshold)
                    return false;
            }
        }
        return true;
    }
}