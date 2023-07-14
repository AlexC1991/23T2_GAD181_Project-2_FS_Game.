using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGridScript : MonoBehaviour

    {
        private void Start()
        {
            Grid grid = new Grid(7, 6, 3.9f, transform);
        }
    }

