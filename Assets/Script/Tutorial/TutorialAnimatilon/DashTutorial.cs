using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTutorial : MonoBehaviour
{

    CameraMoveTutorial cameraMove;
    Camera camera;

    [SerializeField] Transform targetTransform;
    [SerializeField] GameObject targetObject;
    [SerializeField] Transform enemy;

    [SerializeField] PlayerMoveTutorial playerMove;
    [SerializeField] AttackedRange attackedRange;

    [SerializeField] GameObject dashAttackGuideText;
    [SerializeField] GameObject tutorialShift;
    [SerializeField] GameObject tutorialDashMove;

    [SerializeField] GameObject bullet;

    [SerializeField] float executionPosition;

    public int inputTutorialDashMove = 0;

    void Start()
    {
        cameraMove = GetComponent<CameraMoveTutorial>();
        camera = GetComponent<Camera>();
    }

    void Update()
    {

        if( targetTransform.position.x > executionPosition )
        {

            if( !cameraMove.screenTransition)
            {

                cameraMove.screenTransition = true;
                StartCoroutine( TutorialAnimation() );

            }

        }

        switch( inputTutorialDashMove )
        {

            case 1:

                tutorialShift.SetActive(true);
                tutorialDashMove.SetActive(false);

                if( Input.GetKey( KeyCode.LeftShift ) )
                {
                    inputTutorialDashMove = 2;
                }

                break;

            case 2:

                tutorialShift.SetActive(false);
                tutorialDashMove.SetActive(true);

                if( Input.GetAxisRaw("Horizontal") != 0 | Input.GetAxisRaw("Vertical") != 0 )
                {
                    inputTutorialDashMove = 3;
                }

                if( !Input.GetKey( KeyCode.LeftShift ) )
                {
                    inputTutorialDashMove = 1;
                }
                
                break;

            case 3:

                if( playerMove.tutorial == 3 )
                {
                    cameraMove.screenTransition = false;
                    tutorialShift.SetActive(false);
                    tutorialDashMove.SetActive(false);
                    dashAttackGuideText.SetActive(false);
                    inputTutorialDashMove = 4;
                    playerMove.nowUltimateAttackGauge -= 0.01f;
                    
                    this.gameObject.GetComponent<DashTutorial>().enabled = false;
                }

                break;
     
        }

    }

    IEnumerator TutorialAnimation()
    {

        targetObject.GetComponent<PlayerMoveTutorial>().enabled = false;
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

        Instantiate( bullet, enemy.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        targetObject.GetComponent<PlayerMoveTutorial>().enabled = true;
        targetObject.GetComponent<PlayerSound>().enabled = true;
        dashAttackGuideText.SetActive(true);
        playerMove.tutorial += 1;

        inputTutorialDashMove = 1;


    }

}
