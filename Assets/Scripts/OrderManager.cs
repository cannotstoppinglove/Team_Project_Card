using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI orderText;
    public TextMeshProUGUI orderPanelText;
    public GameObject orderPanelUI;


    private string[] iceCreamFlavors = { "�ٴҶ�", "���ݸ�", "����", "����", "�ٳ���" };
    private string[] toppings = { "�ø���", "������Ŭ" };

    private string[] orderTemplates = {
        "���� {0}���� �ֽð�, ������ {1}���� �ּ���",
        "{0} �� ���̽�ũ���� {1} ���� ��Ź�ؿ�",
        "�� {1} ���� {0} ���̽�ũ���� �����ؿ�",
        "{0} �� ���̽�ũ�� �ϳ��� {1} �߰����ּ���",
        "{0}���� �ֽð��, {1} ���ε� �־��ּ���",
        "��, {0}, {1}���� �� "
    };

    public int numberOfOrders = 1;
    private string lastOrderMessage;

    public static OrderManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public IceCreamOrder currentOrder { get; private set; }

    void Start()
    {
        GenerateOrders();
    }

    private void GenerateOrders()
    {
        string fullOrder = "";


        for (int i = 0; i < numberOfOrders; i++)
        {
            string flavor = iceCreamFlavors[Random.Range(0, iceCreamFlavors.Length)];
            string topping = toppings[Random.Range(0, toppings.Length)];
            string template = orderTemplates[Random.Range(0, orderTemplates.Length)];
            currentOrder = new IceCreamOrder(flavor, topping);

            string orderLine = string.Format(template, flavor, topping);
            orderText.text = orderLine;
            lastOrderMessage = orderLine;

            fullOrder += orderLine;

            if (i < numberOfOrders - 1)
                fullOrder += "\n";


        }

        orderText.text = fullOrder;

    }

    public void ShowOrderPanel()
    {
        string msg = OrderManager.Instance.GetLastOrderMessage();

        if (string.IsNullOrEmpty(msg))
        {
            Debug.LogWarning("�ֹ� �޽����� �����ϴ�!");
            return;
        }

        orderPanelText.text = msg;
        orderPanelUI.SetActive(true);
    }

    public string GetLastOrderMessage()
    {
        return lastOrderMessage;
    }
}