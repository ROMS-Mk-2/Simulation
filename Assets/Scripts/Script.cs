using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Script : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    
    public GameObject customerBurger;
    public GameObject customerPizza;
    public GameObject customerHotDog;

    public GameObject[] customerList = new GameObject[3];

    private string currentCustomer;
    
    private GameObject currentCustomerObject;
    
    private int score = 0;

    private int random = 0;
    
    public void ReceiveData(string data)
    {
        Debug.Log("Received data from React: " + data);
        // Use the received data as needed
        if (currentCustomerObject != null)
        {
            if (data == currentCustomer)
            {
                score++;
            }
            text.text = score.ToString();
            StartCoroutine(NewCustomer());
        }
    }

    void Start()
    {
        customerList[0] = customerBurger;
        customerList[1] = customerPizza;
        customerList[2] = customerHotDog;
        //text.text = "This text will be replaced in React!";
        random = Random.Range(0, 2);
        currentCustomerObject = Instantiate(customerList[random], this.transform);
        currentCustomerObject.transform.Find("bubble").gameObject.SetActive(false);
        currentCustomer = random.ToString();
    }

    private string GetCurrentCustomerWant()
    {
        return currentCustomer;
    }

    private IEnumerator NewCustomer()
    {
        Customer currentCustomerScript = currentCustomerObject.GetComponent<Customer>();
        StartCoroutine(currentCustomerScript.WalkAwayAnim());
        yield return new WaitForSeconds(1f);
        Destroy(currentCustomerObject);
        
        random = Random.Range(0, 3);
        currentCustomerObject = Instantiate(customerList[random], this.transform);
        currentCustomer = random.ToString();
    }
}
