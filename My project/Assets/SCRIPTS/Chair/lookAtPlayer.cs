using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    [SerializeField] private float updateEverySecond = 0.3f;

    public Transform CamPlayer;

    private Vector3 lastUpdatePos = Vector3.zero;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LookAt());
    }
    private IEnumerator LookAt()
    {
        while (CamPlayer)
        {
            LookAtPlayerCam();
            yield return new WaitForSeconds(updateEverySecond);
        }
    }
    private void LookAtPlayerCam()
    {
        Vector3 targetPostition = new Vector3(CamPlayer.position.x, transform.position.y, CamPlayer.position.z);

        if(targetPostition != lastUpdatePos)
        {
            transform.LookAt(targetPostition);
        }

        lastUpdatePos = targetPostition;
    }
	/*void LateUpdate(){
		
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}*/
}
