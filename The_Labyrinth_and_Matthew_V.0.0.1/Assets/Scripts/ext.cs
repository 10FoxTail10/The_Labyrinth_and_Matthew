using System;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ext : MonoBehaviour
{
    [Header("TMP Text to Modify")]
    [SerializeField] private TMP_Text textToModify; // Текст который будем изменять

    [Header("Buttons")]
    [SerializeField] private Button decryptBtn;
    [SerializeField] private Button peelBtn;
    [SerializeField] private Button correctBtn;
    [SerializeField] private Button encryptBtn;

    private void Start()
    {
        // Проверяем наличие ссылок
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

    // Метод Decrypt - заменяет секретную букву * на нужную (U)
    public void Decrypt()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("Текст пуст");
            return;
        }

        // Преобразуем в массив символов для изменения
        char[] chars = textToModify.text.ToCharArray();

        // Заменяем все '*' на 'u'
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

    // Метод Correct - заменяет разделители '>' на символ новой строки
    public void Correct()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("Текст пуст");
            return;
        }

        // Заменяем все '>' на символ новой строки
        textToModify.text = textToModify.text.Replace(">", "\n");
        Debug.Log("Correct completed: " + textToModify.text);
    }

    // Метод Peel - убирает символы $ и лишние буквы в конце
    public void Peel()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("Текст пуст");
            return;
        }

        // Убираем все символы $
        string cleanedText = textToModify.text.Replace("$", "");

        // Удаляем лишние буквы в конце (если они есть)
        cleanedText = RemoveTrailingLetters(cleanedText);

        textToModify.text = cleanedText;
        Debug.Log("Peel completed: " + textToModify.text);
    }

    // Вспомогательный метод для удаления букв в конце строки
    private string RemoveTrailingLetters(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        int lastValidIndex = text.Length - 1;

        // Ищем позицию последнего не-буквенного символа
        for (int i = text.Length - 1; i >= 0; i--)
        {
            if (!char.IsLetter(text[i]))
            {
                lastValidIndex = i;
                break;
            }
        }

        // Если нашли не-буквенный символ, обрезаем до него + 1
        if (lastValidIndex < text.Length - 1)
        {
            return text.Substring(0, lastValidIndex + 1);
        }

        return text;
    }

    // Метод Encrypt - собственный способ шифрования с 2-3 итерациями
    public void Encrypt()
    {
        if (string.IsNullOrEmpty(textToModify.text))
        {
            Debug.Log("Текст пуст");
            return;
        }

        string encryptedText = textToModify.text;

        // Итерация 1: Caesar cipher (сдвиг на 3 позиции)
        encryptedText = CaesarCipher(encryptedText, 3);

        // Итерация 2: Reverse string
        encryptedText = ReverseString(encryptedText);

        // Итерация 3: XOR шифрование с ключом
        encryptedText = XORCipher(encryptedText, "SECRET");

        textToModify.text = encryptedText;
        Debug.Log("Encrypt completed: " + textToModify.text);
    }

    // Шифр Цезаря
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

    // Реверс строки
    private string ReverseString(string text)
    {
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    // XOR шифрование
    private string XORCipher(string text, string key)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            char textChar = text[i];
            char keyChar = key[i % key.Length];

            // XOR операция между символами
            char encryptedChar = (char)(textChar ^ keyChar);
            result.Append(encryptedChar);
        }

        return result.ToString();
    }
}