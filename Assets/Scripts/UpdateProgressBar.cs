using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateProgressBar : MonoBehaviour
{

    [SerializeField] LinkScriptable linkSO;
    [SerializeField] Slider progressBar;


    // Start is called before the first frame update
    void Start()
    {

        progressBar.value = linkSO.mainMenuProgressValue;

    }

    private void Update()
    {

        progressBar.value = linkSO.mainMenuProgressValue;

    }

}
