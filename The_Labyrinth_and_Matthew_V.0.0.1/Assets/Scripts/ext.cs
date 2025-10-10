using System;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ext : MonoBehaviour
{
    [Header("TMP Text to Modify")]
    [SerializeField] private TMP_Text textToModify; // ����� ������� ����� ��������

    [Header("Buttons")]
    [SerializeField] private Button decryptBtn;
    [SerializeField] private Button peelBtn;
    [SerializeField] private Button correctBtn;
    [SerializeField] private Button encryptBtn;

    private void Start()
    {
        // ��������� ������� ������
        if (decryptBtn != null)
            decryptBtn.onClick.AddListener(Decrypt);
        else
            Debug.LogError("Decrypt button not assigned in inspector!");

        if (peelBtn != null)
            peelBtn.onClick.AddListener(Peel);
        else
            Debug.LogError("Peel button not assigned in inspector!");

        if (correctBtn != null)
            correctBtn.onClick.AddListener(Correct);
        else
            Debug.LogError("Correct button not assigned in inspector!");

        if (encryptBtn != null)
            encryptBtn.onClick.AddListener(Encrypt);
        else
            Debug.LogError("Encrypt button not assigned in inspector!");

        if (textToModify == null)
            Debug.LogError("Text to Modify not assigned in inspector!");
    }

    // ����� Decrypt - �������� ��������� ����� * �� ������ (U)
    public void Decrypt()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("����� ����");
            return;
        }

        // ����������� � ������ �������� ��� ���������
        char[] chars = textToModify.text.ToCharArray();

        // �������� ��� '*' �� 'u'
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '*')
            {
                chars[i] = 'u';
            }
        }

        textToModify.text = new string(chars);
        Debug.Log("Decrypt completed: " + textToModify.text);
    }

    // ����� Correct - �������� ����������� '>' �� ������ ����� ������
    public void Correct()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("����� ����");
            return;
        }

        // �������� ��� '>' �� ������ ����� ������
        textToModify.text = textToModify.text.Replace(">", "\n");
        Debug.Log("Correct completed: " + textToModify.text);
    }

    // ����� Peel - ������� ������� $ � ������ ����� � �����
    public void Peel()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("����� ����");
            return;
        }

        // ������� ��� ������� $
        string cleanedText = textToModify.text.Replace("$", "");

        // ������� ������ ����� � ����� (���� ��� ����)
        cleanedText = RemoveTrailingLetters(cleanedText);

        textToModify.text = cleanedText;
        Debug.Log("Peel completed: " + textToModify.text);
    }

    // ��������������� ����� ��� �������� ���� � ����� ������
    private string RemoveTrailingLetters(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        int lastValidIndex = text.Length - 1;

        // ���� ������� ���������� ��-���������� �������
        for (int i = text.Length - 1; i >= 0; i--)
        {
            if (!char.IsLetter(text[i]))
            {
                lastValidIndex = i;
                break;
            }
        }

        // ���� ����� ��-��������� ������, �������� �� ���� + 1
        if (lastValidIndex < text.Length - 1)
        {
            return text.Substring(0, lastValidIndex + 1);
        }

        return text;
    }

    // ����� Encrypt - ����������� ������ ���������� � 2-3 ����������
    public void Encrypt()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("����� ����");
            return;
        }

        string encryptedText = textToModify.text;

        // �������� 1: Caesar cipher (����� �� 3 �������)
        encryptedText = CaesarCipher(encryptedText, 3);

        // �������� 2: Reverse string
        encryptedText = ReverseString(encryptedText);

        // �������� 3: XOR ���������� � ������
        encryptedText = XORCipher(encryptedText, "SECRET");

        textToModify.text = encryptedText;
        Debug.Log("Encrypt completed: " + textToModify.text);
    }

    // ���� ������
    private string CaesarCipher(string text, int shift)
    {
        StringBuilder result = new StringBuilder();

        foreach (char character in text)
        {
            if (char.IsLetter(character))
            {
                char baseChar = char.IsUpper(character) ? 'A' : 'a';
                char shifted = (char)(((character - baseChar + shift) % 26 + 26) % 26 + baseChar);
                result.Append(shifted);
            }
            else
            {
                result.Append(character);
            }
        }

        return result.ToString();
    }

    // ������ ������
    private string ReverseString(string text)
    {
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    // XOR ����������
    private string XORCipher(string text, string key)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            char textChar = text[i];
            char keyChar = key[i % key.Length];

            // XOR �������� ����� ���������
            char encryptedChar = (char)(textChar ^ keyChar);
            result.Append(encryptedChar);
        }

        return result.ToString();
    }
}