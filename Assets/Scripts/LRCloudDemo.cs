using UnityEngine;

public class LRCloudDemo : MonoBehaviour
{
    LRCloudClient lrClient;
    private bool peerActive;

	// Use this for initialization
	void Start ()
    {
        Application.runInBackground = true;
        lrClient = new LRCloudClient();
        peerActive = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(peerActive)
        {
            lrClient.Service();
        }
	}

    void OnGUI()
    {
        if(GUI.Button(new Rect(10f,10f,100f,24f),"Connect"))
        {
            peerActive = lrClient.Connect();
        }

        if(GUI.Button(new Rect(10f,40f,100f,24f),"Disconnect"))
        {
            lrClient.Disconnect();
        }
    }
}
