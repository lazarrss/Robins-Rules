using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    public ItemManager itemManager;

    public TileManager tileManager;

    public UIManager uiManager;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        itemManager = GetComponent<ItemManager>();

        tileManager = GetComponent<TileManager>();

        uiManager = GetComponent<UIManager>();

        player = FindObjectOfType<Player>();
    }
}
