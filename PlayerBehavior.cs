using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    bool SwichFlag;//Swich between move and attack mode
    int count = 0;//For test will be replaced
    [SerializeField] List<Vector3> vektor3list = new List<Vector3>(); //Vector List for Queue

    [SerializeField] PlayerScript.State AgentState;
    

    void Start()
    {
        AgentState = PlayerScript.State.Stop;
        SwichFlag = true;//Move Mode
    }

    void Update()
    {
        SlowTime();
        if (SwichFlag)
        {
            moveFromList();

            moveOnRClick();
        }
    }

    void moveFromList()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Vector3 Temppoint = new Vector3(hit.point.x, agent.transform.position.y, hit.point.z);

            //İf Click is out of walkable surface bounds do not add to list
            //Add position check for size bounds on walkable obj because of Nav mesh's Bake Area
            if (!(Temppoint.x == 0 || Temppoint.z == 0))
            {
                vektor3list.Add(Temppoint);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Error(Index was out of range) if list is empty
            if ( vektor3list.Count > 0)
                AgentState = PlayerScript.State.Play;
        }

        switch (AgentState)
        {
            case PlayerScript.State.Check:

                if (vektor3list.Count == 0)
                {   //When all items deleted From the Vector List Go back to start
                    AgentState = PlayerScript.State.Stop;
                }

                else
                    AgentState = PlayerScript.State.Play;
                break;

            case PlayerScript.State.Dec:
                //Remove The Vector that char. moved
                AgentState = PlayerScript.State.Check;
                vektor3list.RemoveAt(0);
                break;

            case PlayerScript.State.Play:
                    //if not reached set dest.
                if (agent.transform.position != vektor3list[0])
                    agent.destination = vektor3list[0];
                // if reached check and delete
                if (agent.transform.position == vektor3list[0])
                    AgentState = PlayerScript.State.Dec;

                break;
        }
    }

    void moveOnRClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Vector3 Temppoint = new Vector3(hit.point.x, agent.transform.position.y, hit.point.z);
            
            if (agent.transform.position != Temppoint)
                agent.destination = Temppoint;
        }
    }
    void SlowTime()
    {
        if (Input.GetKeyDown("space"))
        {
            moveFromList();
            if (count % 2 == 0)
            {
                print("space key was pressed");
                Time.timeScale = 0.01f;
            }
            else
            { Time.timeScale = 1f; }

            count++;
        }
    }
}