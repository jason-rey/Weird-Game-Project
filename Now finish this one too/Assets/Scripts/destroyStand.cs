using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyStand : MonoBehaviour
{
    public stopTimeController timeController;
    public GameObject player;
    public spawnTheWorldSprite worldController;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeController = player.GetComponent<stopTimeController>();
        worldController = player.GetComponent<spawnTheWorldSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeController.fullSize)
        {
            StartCoroutine("DestroyStand");
        }
    }

    private IEnumerator DestroyStand()
    {
        yield return new WaitForSeconds(worldController.theWorldDestroyDelay);

        if (timeController.fullSize)
        {
            Destroy(this.gameObject);
        }
    }
}
