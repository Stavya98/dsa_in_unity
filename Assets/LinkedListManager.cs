using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkedListManager : MonoBehaviour
{
    public GameObject nodePrefab;   // Prefab (block + texts)
    public Transform parent;
    public float spacing = 3.5f;

    private List<GameObject> nodes = new List<GameObject>();
    private int currentValue = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddNode();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveNode();
        }
    }

    void AddNode()
    {
        GameObject newNode = Instantiate(nodePrefab, parent);
        newNode.transform.localPosition = new Vector3(nodes.Count * spacing, 0, 0);

        // Generate random "next address"
        string randomAddress = "0x" + Random.Range(0x1000, 0xFFFF).ToString("X");

        // Get TextMeshPro children
        TextMeshPro[] texts = newNode.GetComponentsInChildren<TextMeshPro>();
        if (texts.Length >= 2)
        {
            texts[0].text = currentValue.ToString(); // Data part
            texts[1].text = "NULL";                  // Default Next
        }

        // Update previous node’s Address text
        if (nodes.Count > 0)
        {
            TextMeshPro[] prevTexts = nodes[nodes.Count - 1].GetComponentsInChildren<TextMeshPro>();
            if (prevTexts.Length >= 2)
            {
                prevTexts[1].text = randomAddress; // Replace "NULL" with random memory addr
            }
        }

        nodes.Add(newNode);
        currentValue++;
    }

    void RemoveNode()
    {
        if (nodes.Count == 0) return;

        // Remove the last node
        GameObject lastNode = nodes[nodes.Count - 1];
        nodes.RemoveAt(nodes.Count - 1);
        Destroy(lastNode);

        // Update the new last node's next pointer to NULL
        if (nodes.Count > 0)
        {
            TextMeshPro[] lastTexts = nodes[nodes.Count - 1].GetComponentsInChildren<TextMeshPro>();
            if (lastTexts.Length >= 2)
            {
                lastTexts[1].text = "NULL";
            }
        }

        currentValue = Mathf.Max(1, currentValue - 1);
    }
}
