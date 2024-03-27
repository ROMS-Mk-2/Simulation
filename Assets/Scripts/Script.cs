using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.InteropServices;


public class Script : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    
    public GameObject customerBurger;
    public GameObject customerPizza;
    public GameObject customerHotDog;

    public GameObject[] customerList = new GameObject[6];

    private string currentCustomer;
    
    private GameObject currentCustomerObject;
    
    private int score = 0;

    public int customerNum = 0;
    
    private int random = 0;

    [DllImport("__Internal")]
    private static extern void SendData(int data);

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

    public void SendDataToReact(string data)
    {
        random = Random.Range(0, 1000);
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            SendData (random);
        #endif
    }

    void Start()
    {
        customerList[0] = customerBurger;
        customerList[1] = customerPizza;
        customerList[2] = customerHotDog;
        customerNum = Random.Range(1, 8);
        Debug.Log(customerNum);
        //text.text = "This text will be replaced in React!";
        random = Random.Range(0, 2);
        currentCustomerObject = Instantiate(customerList[random], this.transform);
        currentCustomerObject.transform.Find("bubble").gameObject.SetActive(false);
        currentCustomer = random.ToString();
        // StartCoroutine(NewCustomer());
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
        
        for (int i = 0; i < customerNum; i++)
        {        
            //spawn customernumber amount of customers with random food option
            
            //this dictates the food number
            random = Random.Range(0, 6);

            currentCustomerObject = Instantiate(customerList[random], new Vector3(transform.position.x + (i*2), transform.position.y, transform.position.z), transform.rotation);
            currentCustomer = random.ToString();
            //rename customer to fit number
            currentCustomerObject.name = "Customer " + (i+1);
        }
    }
}
