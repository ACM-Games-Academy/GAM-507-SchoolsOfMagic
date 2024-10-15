using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventChangeClass : MonoBehaviour
{
    int currentClass;

    public void ClassOne()
    {
        currentClass = 1;
        Debug.Log("Current Class is: " + currentClass);
    }

    public void ClassTwo()
    {
        currentClass = 2;
        Debug.Log("Current Class is: " + currentClass);
    }

    public void ClassThree()
    {
        currentClass = 3;
        Debug.Log("Current Class is: " + currentClass);
    }

    public void ClassFour()
    {
        currentClass = 4;
        Debug.Log("Current Class is: " + currentClass);
    }
}
