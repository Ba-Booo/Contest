using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateTutorial : MonoBehaviour
{
    CameraMove cameraMove;
    Camera camera;

    [SerializeField] Transform targetTransform;
    [SerializeField] GameObject targetObject;
    [SerializeField] Transform enemy;

    [SerializeField] PlayerMove playerMove;

    [SerializeField] GameObject ultimateGuideText;
    [SerializeField] GameObject tutorialQ;

    [SerializeField] float executionPosition;

    public int inputTutorialUltimateMove = 0;

    void Start()
    {
        cameraMove = GetComponent<CameraMove>();
        camera = GetComponent<Camera>();
    }

    void Update()
    {

        if( targetTransform.position.x > executionPosition && inputTutorialUltimateMove == 0 )
        {

            if( !cameraMove.screenTransition)
            {

                cameraMove.screenTransition = true;
                StartCoroutine( TutorialAnimation() );

            }

        }

        switch( inputTutorialUltimateMove )
        {

            case 1:

                if( Input.GetKey( KeyCode.Q ) )
                {
                    tutorialQ.SetActive(false);
                    ultimateGuideText.SetActive(false);
                }

                break;


        }

    }

    IEnumerator TutorialAnimation()
    {

        targetObject.GetComponent<PlayerMove>().enabled = false;
        targetObject.GetComponent<PlayerSound>().enabled = false;

        while( transform.position.x < enemy.position.x - 0.1f)
        {

            transform.position = Vector3.Lerp( transform.position, enemy.position, 5f * Time.deltaTime );
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            camera.orthographicSize = Mathf.Lerp( camera.orthographicSize, 4f, 5f * Time.deltaTime );
            yield return new WaitForSeconds(0.01f);

        }

        yield return new WaitForSeconds(1f);

        while( transform.position.x > ( ( enemy.position.x + targetTransform.position.x ) / 2f ) + 0.1f)
        {

            transform.position = Vector3.Lerp( transform.position, ( enemy.position + targetTransform.position ) / 2f, 5f * Time.deltaTime );
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            camera.orthographicSize = Mathf.Lerp( camera.orthographicSize, 8f, 5f * Time.deltaTime );
            yield return new WaitForSeconds(0.01f);

        }

        yield return new WaitForSeconds(1f);

        playerMove.nowUltimateAttackGauge += 1f;
        playerMove.tutorial = false;

        targetObject.GetComponent<PlayerMove>().enabled = true;
        targetObject.GetComponent<PlayerSound>().enabled = true;

        tutorialQ.SetActive(true);
        ultimateGuideText.SetActive(true);

        inputTutorialUltimateMove = 1;


    }

}
