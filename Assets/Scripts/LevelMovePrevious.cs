using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMovePrevious : MonoBehaviour
{

    public int SceneBuildIndex;



    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");
        if (other.tag == "Player" && Input.GetButtonDown("E"))
        {
            print("Switch scenes to  "  + SceneBuildIndex);
            SceneManager.LoadScene(SceneBuildIndex-1 , LoadSceneMode.Single-1);
        }


    }







    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}




