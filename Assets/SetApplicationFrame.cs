using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetApplicationFrame : MonoBehaviour
{
    void Awake() {
        Application.targetFrameRate = 60;
        Debug.Log("프레임 : " +Application.targetFrameRate);
    }
}
