using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIRoot : MonoBehaviour
{
    public Toggle mToggle_TestPanel;
    public GameObject testPanel;
    // Start is called before the first frame update
    void Start()
    {
        testPanel.SetActive(mToggle_TestPanel.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleTestPanel()
    {
        testPanel.SetActive(mToggle_TestPanel.isOn);
    }
}
