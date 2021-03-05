using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour
{
    public float cooldown;
    public float cooldown_timer;
    public GameObject[] shortcutList;

    void Awake()
    {
        Transform shortcuts = GameObject.Find("Shortcuts").transform;
        shortcutList = new GameObject[shortcuts.childCount-1];
        int y = 0;
        for (int i = 0; i < shortcuts.childCount; i++)
        {
            if (shortcuts.GetChild(i).gameObject.name != gameObject.name)
            {
                shortcutList[y] = shortcuts.GetChild(i).gameObject;
                y++;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (cooldown_timer < 0.1)
            {
                cooldown_timer = cooldown;
                GameObject exit_shortcut = shortcutList[Random.Range(0, shortcutList.Length)];
                exit_shortcut.GetComponent<Shortcut>().cooldown_timer = cooldown;
                Vector3 new_pos = exit_shortcut.transform.GetChild(0).position;
                other.gameObject.transform.position = new_pos;
            }
        }
    }

    public void Update()
    {
        if (cooldown_timer < 0.1)
        {
            return;
        }
        else
        {
            cooldown_timer -= Time.deltaTime;
        }
    }
}
