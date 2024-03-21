using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WalkUpAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator CustomerWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    
    public IEnumerator WalkAwayAnim()
    {
        while (this.transform.localScale.x > 0.01)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 5f;
        }
        Destroy(this.gameObject);
    }
    
    private IEnumerator WalkUpAnim()
    {
        while (this.transform.localScale.x < 1)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 5f;
        }
        transform.Find("bubble").gameObject.SetActive(true);
        //Destroy(this.gameObject);
    }
}
