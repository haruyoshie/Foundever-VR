using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSelect : MonoBehaviour
{
    public List<GameObject> Personajes;

    public int currentPersonaje = 0;

    public GameObject player;

    public void changeCharacter(string dir)
    {
        switch (dir)
        {
            case ("Izquierda"):
                currentPersonaje--;
                if (currentPersonaje == -1)
                {
                    currentPersonaje = Personajes.Count - 1;
                }
                break;
            case ("Derecha"):
                currentPersonaje++;
                if (currentPersonaje == Personajes.Count)
                {
                    currentPersonaje = 0;
                }
                break;
            default:
                break;
        }

        GameObject aux = Instantiate(Personajes[currentPersonaje], player.transform.position,
            player.transform.rotation);
        Destroy(player);
        player = aux;
        PlayerPrefs.SetInt("Player",currentPersonaje);
    }

    public void nextScene()
    {
        SceneManager.LoadScene("EXTERIOR");
    }
}
