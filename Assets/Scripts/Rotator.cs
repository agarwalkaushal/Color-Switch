using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        int r1 = Random.Range(-14, -9);
        int r2 = Random.Range(10, 15);
        int r3 = Random.Range(0, 2);
        if (r3 == 0)
            speed = 10 * r1;
        else
            speed = 10 * r2;

        if(gameObject.tag == "Plus")
        {
            if(gameObject.transform.position.x > 0)
            {
                speed = 10 * r1;
            }
            else
            {
                speed = 10 * r2;
            }

            Transform[] transforms = this.GetComponentsInChildren<Transform>();

            foreach (Transform t in transforms)
            {
                int r = Random.Range(0, 4);
                if (t.gameObject.tag == "Golden")
                {
                    if (r == 0)
                        t.gameObject.transform.localPosition = new Vector3(-0.75f, -0.75f, t.gameObject.transform.position.z);
                    else if (r == 1)
                        t.gameObject.transform.localPosition = new Vector3(0.75f, -0.75f, t.gameObject.transform.position.z);
                    else if (r == 2)
                        t.gameObject.transform.localPosition = new Vector3(-0.75f, 0.75f, t.gameObject.transform.position.z);
                    else
                        t.gameObject.transform.localPosition = new Vector3(0.75f, 0.75f, t.gameObject.transform.position.z);
                    break;
                }
    

            }
        }

        

       

    }

    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime, Space.Self);
    }
}
