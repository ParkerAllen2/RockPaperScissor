using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] animValues fient;
    [SerializeField] animValues counter;
    [SerializeField] animValues slash;

    [SerializeField] GameObject swordPrefab;
    [SerializeField] bool left;

    public bool win;
    public bool tie;

    public Color Slash(bool mastery)
    {
        GameObject go = Instantiate(swordPrefab, transform);
        SwordProjectile sp = go.GetComponent<SwordProjectile>();
        sp.setColor(slash.color);
        sp.setPoints(slash.points);
        sp.setTimes(slash.times);
        sp.setMaster(mastery);
        sp.won = win;
        sp.left = left;
        sp.attackType = 1;
        sp.tie = tie;
        return slash.color;
    }

    public Color Counter(bool mastery)
    {
        GameObject go = Instantiate(swordPrefab, transform);
        SwordProjectile sp = go.GetComponent<SwordProjectile>();
        sp.setColor(counter.color);
        sp.setPoints(counter.points);
        sp.setTimes(counter.times);
        sp.setMaster(mastery);
        sp.won = win;
        sp.left = left;
        sp.attackType = 2;
        sp.tie = tie;
        return counter.color;
    }

    public Color Fient(bool mastery)
    {
        GameObject go = Instantiate(swordPrefab, transform);
        SwordProjectile sp = go.GetComponent<SwordProjectile>();
        sp.setColor(fient.color);
        sp.setPoints(fient.points);
        sp.setTimes(fient.times);
        sp.setMaster(mastery);
        sp.won = win;
        sp.left = left;
        sp.attackType = 3;
        sp.tie = tie;
        return fient.color;
    }

    [System.Serializable]
    public struct animValues
    {
        public Color color;
        public List<Transform> points;
        public List<float> times;
    }
}
