using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    void Start()
    {
      //add Tower defense music start 
    }
    private void Awake()
    {

        main = this;
    }


}
