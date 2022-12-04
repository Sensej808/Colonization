using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    public enum MiningState { IDLE,GoToSource, Mining, GoToStorage, BringMaterials};
    public MiningState miningState;
    public int RecoursesInInventory;
    public SourseOfRecourses source;
    public BaseStructClass storage;
    public int Portion = 5;
    public bool StartMining;
    public bool EndMining;
    public Vector3 MiningPos;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GetComponent<AllyMoving>().onMovingStart += StopMining;
        miningState = MiningState.IDLE;

    }

    // Update is called once per frame
    public void Update()
    {
        switch (miningState)
        {
            case MiningState.IDLE:
                EndMining = false;
                Debug.Log("Go to mine");
                MiningPos = GetComponent<AllyMoving>().MoveTo(source.gameObject);
                miningState = MiningState.GoToSource;
                GetComponent<AllyMoving>().onMovingEnd += StartMine;
                break;
            case MiningState.Mining:
                Debug.Log("Mining");

                if(EndMining)
                    miningState = MiningState.GoToStorage;
                Debug.Log("!" + miningState);
                break;
            case MiningState.GoToStorage:
                Debug.Log("Go to storage");
                GetComponent<AllyMoving>().MoveTo(storage.gameObject);
                miningState = MiningState.BringMaterials;
                GetComponent<AllyMoving>().onMovingEnd += BringRes;
                miningState = MiningState.BringMaterials;
                break;
            default:
                break;
        }
    }

    private void StopMining()
    {
        GetComponent<AllyMoving>().onMovingStart -= StopMining;
        this.enabled = false;
    }

    private void StartMine()
    {
        if(transform.position == MiningPos)
        {
            GetComponent<AllyMoving>().onMovingEnd -= StartMine;
            
            StartMining = true;
            miningState = MiningState.Mining;
            StartCoroutine(miningProcces());
            if (transform.position == MiningPos)
            {
                RecoursesInInventory = source.TakeRecourses(Portion);
                
            }
            else
                StopMining();
        }
    }
    private IEnumerator miningProcces()
    {
        Debug.Log("Start waiting");
        yield return new WaitForSeconds(2);
        Debug.Log("End waiting");
        EndMining = true;
    }

    private void BringRes()
    {
        miningState = MiningState.IDLE;
        RecourceManager.Instance.AddMetal(RecoursesInInventory);
        RecoursesInInventory = 0;
    }
}

/*
 switch (miningState)
        {
            
            case MiningState.IDLE:
                Debug.Log("Go to mine");
                MiningPos = GetComponent<AllyMoving>().MoveTo(source.gameObject);
                miningState = MiningState.GoToSource;
                GetComponent<AllyMoving>().onMovingEnd += () => StartMine();
                GetComponent<AllyMoving>().onMovingEnd += () => miningState = MiningState.Mining;
                break;
            case MiningState.Mining:
                GetComponent<AllyMoving>().onMovingStart += () => this.enabled = false;
                if (this.enabled && EndMining)
                {
                    source.TakeRecourses(Portion);
                    GetComponent<AllyMoving>().onMovingStart -= () => this.enabled = false;
                    miningState = MiningState.GoToStorage;
                }
                break;
            case MiningState.GoToStorage:
                GetComponent<AllyMoving>().MoveTo(storage.gameObject);
                break;
            default:
                break;
        }
 */
