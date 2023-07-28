using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SCR_HeatlhBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public SCR_Health healthManager;
    List<SCR_Heart> healths = new List<SCR_Heart>();

    private void Start()
    {
        DrawHearts();
    }

    private void OnEnable()
    {
        SCR_Health.onPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        SCR_Health.onPlayerDamaged -= DrawHearts;
    }

    public void DrawHearts()
    {
        ClearHealth();

        int heartsToMake = (healthManager.healthMax);

        for(int i = 0; i < heartsToMake; i++)
        {
            CreateHeart();
        }

        for (int i = 0; i < healths.Count; i++)
        {
            int heartStatus = Mathf.Clamp(healthManager.health - i, 0, 1);
            healths[i].SetHeartImage((HeartStatus)heartStatus);
        }
    }

    public void CreateHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        SCR_Heart heartcomp = newHeart.GetComponent<SCR_Heart>();
        heartcomp.SetHeartImage(HeartStatus.Empty);
        healths.Add(heartcomp);
    }

    public void ClearHealth()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        healths = new List<SCR_Heart>();
    }
}
