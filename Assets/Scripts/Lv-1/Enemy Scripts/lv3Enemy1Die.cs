using UnityEngine;

public class lv3Enemy1Die : MonoBehaviour
{
    public GameObject Hp_line;
    public GameObject Hp_line_vertical;
    public GameObject arrowRight;
    public GameObject arrowRight2;
    public GameObject arrowDown;
    public GameObject arrowUP;

    void Start()
    {
        arrowDown.SetActive(false);
        arrowUP.SetActive(false);
    }

    void Update()
    {
        if (Enemy.Instance != null && Enemy.Instance.Enemyhealth == 0)
        {
            //Hp_line.SetActive(false);
            Hp_line.transform.position = new Vector3(8.391169f, -21.52f, 0f);
            Hp_line_vertical.SetActive(true);
            arrowDown.SetActive(true);
            arrowRight.transform.rotation = Quaternion.Euler(0, 0, 180);
            arrowRight2.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
