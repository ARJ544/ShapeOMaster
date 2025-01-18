using UnityEngine;

public class lv3Enemy2Die : MonoBehaviour
{
    public GameObject Hp_line;
    public GameObject Hp_line_vertical;
    public GameObject arrowRight;
    public GameObject arrowRight2;
    public GameObject arrowDown;
    public GameObject arrowUP;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Enemy2.Instance != null && Enemy2.Instance.Enemyhealth2 == 0)
        {
            Hp_line.SetActive(false);
            Hp_line_vertical.SetActive(false);
            arrowUP.SetActive(true);
            arrowDown.SetActive(true);
            arrowRight.SetActive(true);
            arrowRight2.SetActive(true);
            arrowDown.transform.rotation = Quaternion.Euler(0, 0, 180);
            arrowDown.transform.position = new Vector3(8.766153f, -16f, 0f);
            arrowRight.transform.position = new Vector3(16.21201f, 3.2f, 0f);
            arrowRight2.transform.position = new Vector3(106.2f, 3.2f, 0f);
            arrowRight.transform.rotation = Quaternion.Euler(0, 0, 0);
            arrowRight2.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
