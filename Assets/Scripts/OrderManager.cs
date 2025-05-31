using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI orderText;
    public Button acceptButton;
    public Button rejectButton;

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

    public int numberOfOrders = 3;
    private int currentOrderIndex = 0;
    private List<string> generatedOrders = new List<string>();

    void Start()
    {
        acceptButton.onClick.AddListener(AcceptOrder);
        rejectButton.onClick.AddListener(() => StartCoroutine(HandleRejectOrder()));

        GenerateInitialOrders();
        ShowNextOrder();
    }

    private void GenerateInitialOrders()
    {
        generatedOrders.Clear();

        for (int i = 0; i < numberOfOrders; i++)
        {
            generatedOrders.Add(GenerateRandomOrder());
        }
    }

    private string GenerateRandomOrder()
    {
        string flavor = iceCreamFlavors[Random.Range(0, iceCreamFlavors.Length)];
        string topping = toppings[Random.Range(0, toppings.Length)];
        string template = orderTemplates[Random.Range(0, orderTemplates.Length)];

        return string.Format(template, flavor, topping);
    }

    private void ShowNextOrder()
    {
        if (currentOrderIndex < generatedOrders.Count)
        {
            orderText.text = generatedOrders[currentOrderIndex];
        }
        else
        {
            orderText.text = "��� �ֹ��� �������ϴ�!";
            acceptButton.interactable = false;
            rejectButton.interactable = false;
        }
    }

    private void AcceptOrder()
    {
        Debug.Log($"�ֹ� ������: {generatedOrders[currentOrderIndex]}");

        // ���⼭ ���� ȭ������ �̵��ϴ� ó��
        // ��: UI ��ȯ �Ǵ� �ٸ� �� �̵� (������ �α׸�)
        orderText.text = "���̽�ũ�� ���� â���� �̵� ��...";
    }

    private IEnumerator HandleRejectOrder()
    {
        Debug.Log($"�ֹ� ������: {generatedOrders[currentOrderIndex]}");

        // ��ư ��Ȱ��ȭ
        acceptButton.interactable = false;
        rejectButton.interactable = false;

        // ���� �ؽ�Ʈ �����
        orderText.text = "";

        // 0.1�� ��ٷ��� �ؽ�Ʈ ������ ���̱� (���û���)
        yield return new WaitForSeconds(0.1f);

        // ���� �޽��� ǥ��
        orderText.text = "�ֹ��� �����Ǿ����ϴ�.";

        // 2�� ���
        yield return new WaitForSeconds(2f);

        // ���� �ֹ��� ���� �����ؼ� ��ü
        generatedOrders[currentOrderIndex] = GenerateRandomOrder();

        // �� �ֹ� ǥ��
        ShowNextOrder();

        // ��ư �ٽ� Ȱ��ȭ
        acceptButton.interactable = true;
        rejectButton.interactable = true;
    }
}