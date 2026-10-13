using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialSelector : MonoBehaviour
{
    [SerializeField] GameObject gamepadTutorial;
    [SerializeField]GameObject keyboardTutorial;
    [SerializeField] GameObject virtualController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireTutorial()
    {
        if(Application.isMobilePlatform)
        {
            if(PlayerPrefs.GetInt("usingVirtualInput")>0) virtualController.SetActive(true);
            return;
        }
        if(Gamepad.all.Count>0) gamepadTutorial.gameObject.SetActive(true);
        else if(Application.platform==RuntimePlatform.WebGLPlayer)
        {
            if(PlayerPrefs.GetInt("usingVirtualInput")>0) virtualController.SetActive(true);
        }      
        else keyboardTutorial.gameObject.SetActive(true);
        
    }
}
