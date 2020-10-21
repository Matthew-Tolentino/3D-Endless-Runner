using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class SentenceCreater : MonoBehaviour
{
    public string axiom = "F";

    public int iterations = 1;

    //private StringBuilder sentence_;

    private Dictionary<char, string> rules = new Dictionary<char, string>();

    void Start()
    {
        FillRules();
        GenerateSentence();
    }

    public string GenerateSentence()
    {
        string sentence = axiom;
        for (int i = 0; i < iterations; i++)
        {
            sentence = ApplyRules(sentence);
            Debug.Log(sentence);
        }
        return "";
    }

    private string ApplyRules(string sentence)
    {
        string ruledSentence = "";
        foreach (char c in sentence)
        {
            ruledSentence += rules.ContainsKey(c) ? rules[c] : c.ToString();
        }

        return ruledSentence;
    }

    public void FillRules()
    {
        rules.Add('F', "FR");
        rules.Add('R', "LF");
        rules.Add('L', "F");
    }
}
