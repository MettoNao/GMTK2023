using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    [SerializeField] private List<Bullet> Start_Objs = new List<Bullet>();
    [SerializeField] private List<int> start_counts = new List<int>();

    private List<GameObject> effects = new List<GameObject>();

    private List<Bullet> bullets = new List<Bullet>();

    private Transform pool;
    private bool start;

    private void Start()
    {
        start = true;

        pool = new GameObject("エフェクトなど").transform;

        for (int i = 0; i < Start_Objs.Count; i++)
        {
            for (int e = 0; e <= start_counts[i]; e++)
            {
                CreateBullet(Start_Objs[i]);
            }
        }

        start = false;
    }

    public GameObject CreateEffect(GameObject effect)
    {
        foreach (GameObject e in effects)
        {
            if (start) break;

            if (e.gameObject.name == effect.name && !e.gameObject.activeSelf)
            {
                e.gameObject.SetActive(true);
                return e;
            }
        }

        var o = Instantiate(effect, pool);
        effects.Add(o);
        o.gameObject.SetActive(!start);
        o.gameObject.name = effect.gameObject.name;
        return o;
    }

    public Bullet CreateBullet(Bullet bullet)
    {
        foreach (Bullet b in bullets)
        {
            if (start) break;

            if (b.gameObject.name == bullet.name && !b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(true);
                return b;
            }
        }

        var o = Instantiate(bullet, pool);
        bullets.Add(o);
        o.gameObject.SetActive(!start);
        o.gameObject.name = bullet.gameObject.name;
        return o;
    }

    private List<List<GameObject>> objectLists = new List<List<GameObject>>();
    public GameObject GenerateObject(GameObject obj)
    {
        int listIndex = -1;
        for (int i = 0; i < objectLists.Count; i++)
        {
            foreach (var o in objectLists[i])
            {
                if (o.name == obj.name)
                {
                    listIndex = i;
                    if (o.activeSelf == false)
                    {
                        o.SetActive(true);
                        return o;
                    }
                }
            }
        }

        if (listIndex != -1)
        {
            var o = Instantiate(obj);
            o.name = obj.name;
            objectLists[listIndex].Add(o);
            return o;
        }
        else
        {
            objectLists.Add(new List<GameObject>());
            var o = Instantiate(obj);
            o.name = obj.name;
            objectLists[objectLists.Count - 1].Add(o);
            return o;
        }
    }
}