using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] List<Vector3> points;
    [SerializeField] List<float> times;
    [SerializeField] bool master;
    [SerializeField] ParticleSystem masterParticals;
    ParticleSystem sparks;

    public bool won;
    public bool tie;
    public bool left;
    public int attackType;

    float counter;
    Vector3 velocity;
    Vector3 startPos;

    [SerializeField] TrailRenderer trail;

    public void Start()
    {
        trail.endColor = color;
        trail.startColor = color;

        if (master)
        {
            masterParticals.Play();
            ParticleSystem.MainModule settings = masterParticals.main;
            settings.startColor = color;
        }

        if (!won)
        {
            points.RemoveAt(points.Count - 1);
            points.RemoveAt(points.Count - 1);
        }
        startPos = transform.position;
        SetOrientation();
    }

    public void Update()
    {
        if(points.Count > 0)
        {
            counter += Time.deltaTime;
            if (counter > times[0])
            {
                HitPoint();
            }
            else
            {
                velocity = Vector3.Lerp(startPos, points[0], counter / times[0]);
                transform.position = velocity;
            }
        }
    }

    void HitPoint()
    {
        counter = 0;
        transform.position = points[0];
        startPos = points[0];
        points.RemoveAt(0);
        times.RemoveAt(0);
        if(points.Count == 0)
        {
            StartCoroutine(SelfDestruct());

            if (tie)
            {
                GameObject.Find("Clang").GetComponent<AudioSource>().Play();
                StartCoroutine(activateSparks());
            }
            
        }
        else if (!won && attackType == 1 && points.Count == 1)
        {
            GameObject.Find("Clang").GetComponent<AudioSource>().Play();
            StartCoroutine(activateSparks());
        }
        else if(won && points.Count == 2)
        {
            AudioSource aS = GameObject.Find("Slash").GetComponent<AudioSource>();
            aS.panStereo = (left) ? -.7f : .7f;
            aS.Play();
        }
        else if(attackType == 3 && ((won && points.Count == 3) || (!won && points.Count == 1)))
        {
            GameObject.Find("Wif").GetComponent<AudioSource>().Play();
        }
        else
        {
            SetOrientation();
        }
    }

    void SetOrientation()
    {
        float angle = Vector3.SignedAngle(Vector3.right, points[0], Vector3.forward);
        angle += Random.Range(-45, 46);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void setColor(Color c)
    {
        color = c;
    }

    public void setPoints(List<Transform> p)
    {
        foreach(Transform t in p)
        {
            points.Add(t.position);
        }
    }

    public void setTimes(List<float> t)
    {
        foreach (float f in t)
        {
            times.Add(f);
        }
    }

    public void setMaster(bool m)
    {
        master = m;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(trail.time);

        if(sparks != null)
            sparks.Stop();

        Destroy(gameObject);
    }

    IEnumerator activateSparks()
    {
        sparks = GameObject.Find("Sparks").GetComponent<ParticleSystem>();
        sparks.transform.position = new Vector2(0, transform.position.y);
        sparks.Play();
        yield return new WaitForSeconds(.2f);
        sparks.Stop();
    }
}
