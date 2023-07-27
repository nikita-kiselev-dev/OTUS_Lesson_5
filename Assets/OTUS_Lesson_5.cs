using System;
using System.IO;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OTUS_Lesson_5 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private Toggle isIntArrayToggle;
    
    private int[] intArray;
    private float[] floatArray;

    private string stringResult;
    
    private string saveFileName = "StringResult.txt";
    
    public void GetStart(bool isIntArray)
    {

        //Блок try-catch-finally
        try
        {
            if (isIntArrayToggle.isOn)
            {
                intArray = CreateArray<int>();
                intArray = FillArray(intArray);
            }
            else
            {
                floatArray = CreateArray<float>();
                floatArray = FillArray(floatArray);
            }
        }
        catch
        {
            //Ловим exception
            Debug.LogException(new Exception("Index Out Of Range!!!"));
        }
        finally
        {
            Debug.Log("CreateArray Completed!");

            if (isIntArrayToggle.isOn)
            {
                LogArray(intArray);
                
                //Используем ref
                CalculateRef(ref intArray[0]);
                
                //Используем out
                CalculateOut(intArray[0], intArray[1], out int sum, out int substract);
                Debug.Log($"{intArray[0]} + {intArray[1]} = {sum}");
                Debug.Log($"{intArray[0]} - {intArray[1]} = {substract}");

                ConvertToString(intArray);
            }
            else
            {
                LogArray(floatArray);
                //Используем ref
                CalculateRef(ref floatArray[0]);
                
                //Используем out
                CalculateOut(floatArray[0], floatArray[1], out float sum, out float substract);
                Debug.Log($"{floatArray[0]} + {floatArray[1]} = {sum}");
                Debug.Log($"{floatArray[0]} - {floatArray[1]} = {substract}");
                
                ConvertToString(floatArray);
            }
        }
        
    }
    private T[] CreateArray<T>()
    {
        T[] array = new T[Random.Range(5, 10)];

        debugText.text = "Array created!";

        return array;
    }
    private T[] FillArray<T>(T[] array)
    {
        Type type = typeof(T);

        if (type == typeof(int))
        {
            array[0] = (T)(object)Random.Range(2, 10);
        }
        else if (type == typeof(float))
        {
            array[0] = (T)(object)Random.Range(2.0f, 10.0f);
        }
        
        for (int i = 1; i < array.Length + 1; i++)
        {
            dynamic value = array[i - 1];
            array[i] = (T)(object)(value * value);
        }

        debugText.text = "Array filled!";
        
        return array;
    }
    private void LogArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log($"{typeof(T)} array cell [{i}] value: {array[i]}");
        }

        debugText.text = "Array logged!";
    }
    private void CalculateRef<T>(ref T firstCellValue)
    {
        if (typeof(T) == typeof(int))
        {
            int intValue = Convert.ToInt32(firstCellValue);
            intValue += 10;
            firstCellValue = (T)(object)intValue;
        }
        else if (typeof(T) == typeof(float))
        {
            float floatValue = Convert.ToSingle(firstCellValue);
            floatValue += 10.0f;
            firstCellValue = (T)(object)floatValue;
        }
        
        Debug.Log($"new {typeof(T)} array cell [{0}] value: {firstCellValue}");

        debugText.text = "Ref calculated!";
    }
    private void CalculateOut<T>(T firstCellValue, T secondCellValue, out T sum, out T substract)
    {
        sum = (T)(object)((dynamic)firstCellValue + (dynamic)secondCellValue);
        substract = (T)(object)((dynamic)firstCellValue - (dynamic)secondCellValue);

        debugText.text = "Out calculated!";
    }
    public void ConvertToString<T>(T[] array)
    {
        var sb = new StringBuilder();

        foreach (var value in array)
        {
            sb.Append(value + "\n");
        }

        stringResult = sb.ToString();

        debugText.text = "Values converted to string!";
    }

    public void SaveFile()
    {
        string saveFilePath = Application.persistentDataPath + "/" + saveFileName;
        
        File.WriteAllText(saveFilePath, stringResult);

        debugText.text = "File saved!";
        Debug.Log($"Your file saved here: {saveFilePath}");
    }

}
