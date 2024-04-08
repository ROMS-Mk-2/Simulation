using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public TextMeshProUGUI text;
   

    public GameObject[] customerList = new GameObject[6];
    public List<GameObject> currentCustomers = new List<GameObject>();
    
    private string currentCustomer;
    private GameObject currentCustomerObject;

    private List<string> foodList = new List<string>();
    private List<int> cart = new List<int>();
    
    private int score = 0;
    private int random = 0;
    private int customerNum = 4;

    int test = 0;

    [DllImport("__Internal")]
    private static extern void SendData(int data);

    public void ReceiveData(string data)
    {
        for (int i = cart.Count - 1; i >= 0; i--)
        {
            foreach(GameObject o in currentCustomers)
            {
                Customer customerScript = o.GetComponent<Customer>();
                print("Food Bool: " + foodList[cart[i]].Equals(customerScript.Food) + " Check Bool: " + (customerScript.Check == false));
                if (foodList[cart[i]].Equals(customerScript.Food) && customerScript.Check == false)
                {
                    score++;
                    customerScript.Check = true;
                    cart.RemoveAt(i);
                }
            }
        }

        foreach(int i in cart)
        {
            score--;
        }

        text.text = score.ToString();
        StartCoroutine(NewCustomer());
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
        foodList.Add("HotDog");
        foodList.Add("Hamburger");
        foodList.Add("Pizza");
        foodList.Add("Fry");
        foodList.Add("Fish");
        foodList.Add("Chicken");

        StartCoroutine(NewCustomer());
    }

    private IEnumerator NewCustomer()
    {
        foreach (Button b in FindObjectsOfType<Button>())
        {
            b.enabled = false;
        }
        foreach(GameObject o in currentCustomers)
        {
            Customer currentCustomerScript = o.GetComponent<Customer>();
            StartCoroutine(currentCustomerScript.WalkAwayAnim());
            
            yield return new WaitForSeconds(1f);
        }
        currentCustomers.Clear();
        cart.Clear();
        
        for (int i = 0; i < customerNum; i++)
        {        
            int newRandom = Random.Range(0, 6);
            GameObject currentCustomerObject = Instantiate(customerList[newRandom], new Vector3(transform.position.x + (i*2), transform.position.y, transform.position.z), transform.rotation);
            currentCustomerObject.name = "Customer " + (i+1);
            currentCustomers.Add(currentCustomerObject);
        }
        test++;
        if(test > 5)
        {
            test = 0;
        }
        foreach (Button b in FindObjectsOfType<Button>())
        {
            b.enabled = true;
        }
    }

    public void PrintList()
    {
        foreach(GameObject o in currentCustomers)
        {
            Customer customerScript = o.GetComponent<Customer>();
            print("Customer: " + o.name + " Food: " + customerScript.Food);
        }
    }

    public void addItemToCart(int item)
    {
        cart.Add(item);
    }
}
