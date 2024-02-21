using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPlayer : MonoBehaviour
{
    public List<GameObject> Player;

    public Transform PosInstant;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Player[PlayerPrefs.GetInt("Player")], PosInstant.position, PosInstant.rotation);
    }
}
