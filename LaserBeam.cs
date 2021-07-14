using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;
public class LaserBeam : MonoBehaviour
{
    private float laserBeamLength;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    //private MultiSceneLoader multiSceneLoader;

    //private variable to check if this client has another scene open(possibly could store this in PlayFab)
    private bool clientSceneActive;

    public bool laserFired;

    private GameObject canvasOuterUI;

    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
        laserBeamLength = 2.0f;
        clientSceneActive = false;

        laserFired = false;

        canvasOuterUI = GameObject.Find("CanvasOuterUI");

        lineRenderer.sortingLayerID = SortingLayer.NameToID("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireLaser(BoltConnection entityConnection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, laserBeamLength);

        if (hit)
        {
            if (hit.collider.isTrigger && clientSceneActive == false)
            {
                if(!laserFired)
                {
                    Debug.Log("multisceneload");
                    MultiSceneLoader.LoadScene(hit.collider.name, entityConnection);
                    //clientSceneActive = true;

                    laserFired = true;
                    //turn off the UI elements completely, they need to be disabled so the player can not move while inside
                    //canvasOuterUI.SetActive(false);

                    //hit.collider.enabled = false;
                }
                Debug.Log(hit.collider);
            }
        }
        
        lineRenderer.SetPosition(0, new Vector3(0, 2, -1));
    }

    public void StopLaser()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, new Vector3(0, 0, -1));
            //clientSceneActive = false;

            laserFired = false;
        }
    }
}