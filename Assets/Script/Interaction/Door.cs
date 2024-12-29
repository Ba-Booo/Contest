using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    [SerializeField] Transform cameraMovePosition;
    [SerializeField] CameraMove cameraMove;
    [SerializeField] Vector3 screenTransitionPosition;

    [SerializeField] Vector2 changeXRange;
    [SerializeField] Vector2 changeYRange;

    [SerializeField] GameObject wall;


    void OnTriggerEnter2D( Collider2D collision )
    {

        if(collision.gameObject.tag == "Player" && !cameraMove.screenTransition )
        {

            cameraMove.screenTransition = true;

            StartCoroutine( ChangeScreen() );

        }
        
    }

    IEnumerator ChangeScreen()
    {

        cameraMove.screenTransition = true;
        wall.SetActive(true);
        
        while( ( cameraMovePosition.position.x > screenTransitionPosition.x + 0.1f | cameraMovePosition.position.x < screenTransitionPosition.x - 0.1f) && ( cameraMovePosition.position.x > screenTransitionPosition.x + 0.1f | cameraMovePosition.position.x < screenTransitionPosition.x  - 0.1f ) )
        {
            cameraMovePosition.position = Vector3.Lerp( cameraMovePosition.position, screenTransitionPosition, 0.1f );
            yield return new WaitForSeconds(0.01f);
        }

        cameraMove.xRange = changeXRange;
        cameraMove.yRange = changeYRange;
        cameraMove.screenTransition = false;

    }

}
