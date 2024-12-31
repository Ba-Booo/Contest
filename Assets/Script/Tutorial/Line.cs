using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Line : MonoBehaviour
{

    CameraMove cameraMove;
    Camera camera;

    [SerializeField] GameObject MPCLineTextOne;
    [SerializeField] GameObject MPCLineTextTwo;

    [SerializeField] Transform MPCPosition;
    [SerializeField] Transform playerPosition;

    TMP_Text textOne;
    TMP_Text textTwo;

    Vector3 originalPosition;
    Vector3 hiddenPositionDown;
    Vector3 hiddenPositionUp;

    Color textColorOne;
    Color textColorTwo;

    int lineOrder = 0;
    int lineInput = 0;


    [SerializeField] PlayerMove playerMove;
    [SerializeField] GameObject playerScript;

    void Start()
    {

        cameraMove = GetComponent<CameraMove>();
        camera = GetComponent<Camera>();

        textOne = MPCLineTextOne.GetComponent<TMP_Text>();
        textTwo = MPCLineTextTwo.GetComponent<TMP_Text>();

        textColorOne = MPCLineTextOne.GetComponent<TMP_Text>().color;
        textColorTwo = MPCLineTextTwo.GetComponent<TMP_Text>().color;

        originalPosition = MPCLineTextOne.transform.position;
        hiddenPositionDown = new Vector3( MPCLineTextOne.transform.position.x, MPCLineTextOne.transform.position.y -1f, MPCLineTextOne.transform.position.z );
        hiddenPositionUp = new Vector3( MPCLineTextOne.transform.position.x, MPCLineTextOne.transform.position.y + 1f, MPCLineTextOne.transform.position.z );

        MPCLineTextOne.transform.position = hiddenPositionDown;
        MPCLineTextTwo.transform.position = hiddenPositionDown;

        cameraMove.screenTransition = true;
        
    }


    void Update()
    {
        
        if( Input.GetKeyDown( KeyCode.E  ) )
        {
            lineInput += 1;
        }

        if( lineInput != 4 )
        {
            StartCoroutine( LineCameraMove( false, MPCPosition ) );
        }

        switch( lineInput )
        {

            case 0:

                if( lineOrder == 0 )
                {
                    textOne.text = "안녕?";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne) );
                    lineOrder += 1;
                }
                break;

            case 1:

                if( lineOrder == 1 )
                {
                    textTwo.text = "상황을 보아하니...";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;

            case 2:

                if( lineOrder == 2 )
                {
                    textOne.text = " 저 실험실을 탈출하려 했던 것 같은데...";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne ) );
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                }
                break;

            case 3:

                if( lineOrder == 3 )
                {
                    textTwo.text = "맞니?";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;

            case 4:

                StartCoroutine( LineCameraMove( true, playerPosition ) );

                if( lineOrder == 4 )
                {
                    textOne.text = " ㅤ                                                          ...";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne ) );
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                }
                break;
                
            case 5:

                if( lineOrder == 5 )
                {
                    textTwo.text = "너무 경계하지 마";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;
            
            case 6:

                if( lineOrder == 6 )
                {
                    textOne.text = "이래봐도 내가 치료해 줬는걸?";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne ) );
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                }
                break;

            case 7:

                if( lineOrder == 7 )
                {
                    textTwo.text = "아니면 그냥 단순히 실험 부작용인가?";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;

            case 8:

                if( lineOrder == 8 )
                {
                    textOne.text = "아무튼,";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne ) );
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                }
                break;

            case 9:

                if( lineOrder == 9 )
                {
                    textTwo.text = "내가 길마다 나가기 쉽도록 신체재생장치를 몇 개 놓아둘게";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;

            case 10:

                if( lineOrder == 10 )
                {
                    textOne.text = "나는 널 돕는 나만의 이유가 있으니 부담 갖지 마";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne ) );
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                }
                break;

            case 11:

                if( lineOrder == 11 )
                {
                    textTwo.text = "이유는 너가 내 나이 되면 알려줄게";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;

            case 12:

                if( lineOrder == 12 )
                {
                    textOne.text = "그때되면 내가 죽어있지 않을까?";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne ) );
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                }
                break;

            case 13:

                if( lineOrder == 13 )
                {
                    textTwo.text = "하하";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;
                
            case 14:

                if( lineOrder == 14 )
                {
                    textOne.text = "길은 바로 앞으로 가면 돼";
                    StartCoroutine( TextAppear( MPCLineTextOne, textColorOne ) );
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                }
                break;

            case 15:

                if( lineOrder == 15 )
                {
                    textTwo.text = "행운을 빌어";
                    StartCoroutine( TextAppear( MPCLineTextTwo, textColorTwo ) );
                    StartCoroutine( TextDisappear( MPCLineTextOne, textColorOne ) );
                    lineOrder += 1;
                }
                break;

            case 16:

                if( lineOrder == 16 )
                {
                    StartCoroutine( TextDisappear( MPCLineTextTwo, textColorTwo ) );
                    lineOrder += 1;
                    cameraMove.cameraZoomSize = 13f;
                    
                    StartCoroutine( CameraSizeChange() );

                    playerScript.GetComponent<PlayerMove>().enabled = true;
                    playerScript.GetComponent<PlayerSound>().enabled = true;
                    cameraMove.screenTransition = false;
                    this.gameObject.GetComponent<Line>().enabled = false;

                }
                break;



        }


    }

    IEnumerator TextAppear( GameObject gameObjects, Color colors )
    {

        colors = gameObjects.GetComponent<TMP_Text>().color;

        while( colors.a < 1f && gameObjects.transform.position.y < originalPosition.y - 0.1f )
        {

            colors.a += 5f * Time.deltaTime;
            gameObjects.transform.position = Vector3.Lerp( gameObjects.transform.position, originalPosition, 5f * Time.deltaTime);

            gameObjects.GetComponent<TMP_Text>().color = colors;

            yield return new WaitForSeconds(0.01f);

        }

        colors = gameObjects.GetComponent<TMP_Text>().color;

    }

    IEnumerator TextDisappear( GameObject gameObjects, Color colors )
    {

        colors = gameObjects.GetComponent<TMP_Text>().color;

        while( colors.a > 0f && gameObjects.transform.position.y < hiddenPositionUp.y - 0.1f )
        {

            colors.a -= 5f * Time.deltaTime;
            gameObjects.transform.position = Vector3.Lerp( gameObjects.transform.position, hiddenPositionUp, 5f * Time.deltaTime);

            gameObjects.GetComponent<TMP_Text>().color = colors;

            yield return new WaitForSeconds(0.01f);

        }

        gameObjects.transform.position = hiddenPositionDown;

    }

    IEnumerator LineCameraMove( bool direction, Transform objectPosition )
    {

        if( direction )     //오른쪽
        {

            while( transform.position.x < objectPosition.position.x - 0.1f )
            {

                transform.position = Vector3.Lerp(  transform.position, objectPosition.position, Time.deltaTime );
                transform.position = new Vector3( transform.position.x,  transform.position.y, -10f );
                yield return new WaitForSeconds(0.01f);

            }

        }
        else                //왼쪽
        {

            while( transform.position.x > objectPosition.position.x + 0.1f )
            {

                transform.position = Vector3.Lerp(  transform.position, objectPosition.position, Time.deltaTime );
                transform.position = new Vector3( transform.position.x,  transform.position.y, -10f );
                yield return new WaitForSeconds(0.01f);

            }

        }

    }

    IEnumerator CameraSizeChange()
    {
        while( camera.orthographicSize < 12.9f)
        {
            camera.orthographicSize = Mathf.Lerp( camera.orthographicSize, 13f, Time.deltaTime );
            yield return new WaitForSeconds(0.01f);
        }
    }
        
}
