using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject tracking;
    public GameObject playerIcon;

    float storedShadowDistance;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(tracking.transform.position.x, transform.position.y, tracking.transform.position.z);
        playerIcon.transform.rotation = Quaternion.Euler(0, 0, -tracking.transform.rotation.eulerAngles.y);
    }

    void OnPreRender()
    {
        storedShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0.0f;
    }

    private void OnPostRender()
    {
        QualitySettings.shadowDistance = storedShadowDistance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(tracking.transform.position.x, transform.position.y, tracking.transform.position.z);
        playerIcon.transform.rotation = Quaternion.Euler(0, 0, -tracking.transform.rotation.eulerAngles.y);
    }
}
