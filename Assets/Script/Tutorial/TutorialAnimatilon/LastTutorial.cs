using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastTutorial : MonoBehaviour
{

    CameraMove cameraMove;
    Camera camera;

    [SerializeField] Transform targetTransform;
    [SerializeField] Transform wall;
    [SerializeField] GameObject targetObject;

    [SerializeField] PlayerMove playerMove;

    [SerializeField] GameObject tutorialEnemy;

    [SerializeField] GameObject changeScene;

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

    }

    IEnumerator TutorialAnimation()
    {

        targetObject.GetComponent<PlayerMove>().enabled = false;
        targetObject.GetComponent<PlayerSound>().enabled = false;

        while( transform.position.x < wall.position.x - 0.1f)
        {

            transform.position = Vector3.Lerp( transform.position, wall.position, 5f * Time.deltaTime );
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            camera.orthographicSize = Mathf.Lerp( camera.orthographicSize, 8f, 5f * Time.deltaTime );
            yield return new WaitForSeconds(0.01f);

        }

        for( float i = 0; i < 10; i += 1.5f )
        {

            Instantiate( tutorialEnemy, new Vector3( wall.position.x - 20f - i, wall.position.y, wall.position.z ), Quaternion.identity);

        }

        yield return new WaitForSeconds(1f);

        while( transform.position.x > wall.position.x - 19.9f )
        {

            transform.position = Vector3.Lerp( transform.position, new Vector3( wall.position.x - 20f, wall.position.y, wall.position.z ), 5f * Time.deltaTime );
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            camera.orthographicSize = Mathf.Lerp( camera.orthographicSize, 8f, 5f * Time.deltaTime );
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1f);

        while( transform.position.x < targetTransform.position.x - 0.1f )
        {

            transform.position = Vector3.Lerp( transform.position, targetTransform.position, 5f * Time.deltaTime );
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            camera.orthographicSize = Mathf.Lerp( camera.orthographicSize, 8f, 5f * Time.deltaTime );
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1f);

        changeScene.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene( "GameScene" );

    }

}