/*
 *  Ignore all code in here I was messing around with procdural plant generation.
 *  Code tutorial can be found at https://www.youtube.com/watch?v=wo3VGyF5z8Q
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlantGenerator : MonoBehaviour
{
    public int iterations;
    public float angle;
    public float width;
    public float minLeafLength;
    public float maxLeafLength;
    public float minBranchLength;
    public float maxBranchLength;
    public float variance;

    public GameObject tree;
    public GameObject branch;
    public GameObject leaf;

    private const string axiom = "X";

    private Dictionary<char, string> rules = new Dictionary<char, string>();
    private Stack<SavedTransform> transformStack = new Stack<SavedTransform>();
    private Vector3 initalPositon;

    private string currentPath = "";
    private float[] randomRotations;

    private void Awake()
    {
        randomRotations = new float[1000];
        for (int i = 0; i < randomRotations.Length; i++)
        {
            randomRotations[i] = Random.Range(-1.0f, 1.0f);
        }

        rules.Add('X', "[-FX][+FX][FX]");
        rules.Add('F', "FF");

        Generate();
    }

    private void Generate()
    {
        currentPath = axiom;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            char[] currentPathChars = currentPath.ToCharArray();
            for (int j = 0; j < currentPathChars.Length; j++)
            {
                sb.Append(rules.ContainsKey(currentPathChars[j]) ? rules[currentPathChars[j]] : currentPathChars[j].ToString());
            }

            currentPath = sb.ToString();
            sb = new StringBuilder();
        }

        for (int k = 0; k < currentPath.Length; k++)
        {
            switch (currentPath[k])
            {
                case 'F':
                    initalPositon = transform.position;
                    bool isLeaf = false;

                    GameObject currentElement = null;
                    try
                    {
                        if (currentPath[k + 1] % currentPath.Length == 'X' || currentPath[k + 3] % currentPath.Length == 'F' && currentPath[k + 4] % currentPath.Length == 'X')
                        {
                            currentElement = Instantiate(leaf, transform.position, transform.rotation);
                            isLeaf = true;
                        }
                    }
                    catch { }
                    if (currentElement == null) currentElement = Instantiate(branch, transform.position, transform.rotation);


                    currentElement.transform.SetParent(tree.transform);

                    TreeElement currentTreeElement = currentElement.GetComponent<TreeElement>();

                    if (isLeaf)
                    {
                        float length = Random.Range(minLeafLength, maxLeafLength);
                        currentTreeElement.lineRenderer.SetPosition(1, new Vector3(0, length, 0));
                        transform.Translate(Vector3.up * 2f * Random.Range(minLeafLength, maxLeafLength));
                    }
                    else
                    {
                        float length = Random.Range(minBranchLength, maxBranchLength);
                        currentTreeElement.lineRenderer.SetPosition(1, new Vector3(0, length, 0));
                        transform.Translate(Vector3.up * 2f * Random.Range(minBranchLength, maxBranchLength));
                    }

                    currentTreeElement.lineRenderer.startWidth = currentTreeElement.lineRenderer.startWidth * width;
                    currentTreeElement.lineRenderer.endWidth = currentTreeElement.lineRenderer.endWidth * width;
                    currentTreeElement.lineRenderer.sharedMaterial = currentTreeElement.material;
                    break;

                case 'X':
                    break;

                case '+':
                    transform.Rotate(Vector3.forward * angle * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
                    break;

                case '-':
                    transform.Rotate(Vector3.back * angle * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
                    break;

                case '*':
                    transform.Rotate(Vector3.up * 120f * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
                    break;

                case '/':
                    transform.Rotate(Vector3.down * 120f * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
                    break;

                case '[':
                    transformStack.Push(new SavedTransform()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    SavedTransform savedTransform = transformStack.Pop();

                    transform.position = savedTransform.position;
                    transform.rotation = savedTransform.rotation;
                    break;
            }
        }
    }
}
