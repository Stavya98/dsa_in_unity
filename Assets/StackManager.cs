using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StackManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform stackParent;
    public float blockHeight = 1.1f;

    private Stack<GameObject> stack = new Stack<GameObject>();
    private int currentValue = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))  // Push with 'P'
        {
            PushBlock();
        }

        if (Input.GetKeyDown(KeyCode.O))  // Pop with 'O'
        {
            PopBlock();
        }
    }

    public void PushBlock()
    {
        Debug.Log("Push Block Called");
        GameObject newBlock = Instantiate(blockPrefab, stackParent);
        StartCoroutine(AnimatePush(newBlock, stack.Count * blockHeight));
        newBlock.GetComponentInChildren<TextMeshPro>().text = currentValue.ToString();
        stack.Push(newBlock);
        currentValue++;

    }

    public void PopBlock()
    {
        Debug.Log("Pop Block Called");
        if (stack.Count == 0) return;

        GameObject topBlock = stack.Pop();
        StartCoroutine(AnimatePop(topBlock));

    }
    private System.Collections.IEnumerator AnimatePush(GameObject block, float targetY)
{
        float duration = 0.5f;
    float elapsed = 0f;
    Vector3 startPos = new Vector3(0, 5f, 0);  // start high in the air
    Vector3 endPos = new Vector3(0, targetY, 0);

    block.transform.localPosition = startPos;

    while (elapsed < duration)
    {
        block.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
        elapsed += Time.deltaTime;
        yield return null;
    }

    block.transform.localPosition = endPos;
}
private System.Collections.IEnumerator AnimatePop(GameObject block)
{
    float duration = 0.5f;
    float elapsed = 0f;
    Vector3 startPos = block.transform.localPosition;
    Vector3 endPos = startPos + Vector3.up * 5f;  // move upwards

    while (elapsed < duration)
    {
        block.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
        elapsed += Time.deltaTime;
        yield return null;
    }

    Destroy(block);
}


}
