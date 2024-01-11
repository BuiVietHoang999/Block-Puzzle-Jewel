using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public bool isOk = false;
    [SerializeField] public Vector2 size;
    void Start()
    {
        List<float> listPosX = new List<float>();
        List<float> listPosY = new List<float>();
        foreach (Transform child in transform)
        {
            listPosX.Add(child.localPosition.x);
            listPosY.Add(child.localPosition.y);
        }
        size = new Vector2(listPosX.Max() + 1, listPosY.Max() + 1);
    }
}
