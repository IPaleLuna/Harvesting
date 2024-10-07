using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;

public static class NumToStringBuffer
{
    private const int START_CAPACITY = 100;

    private static List<string> _numbersBuffer;
    private static List<string> _numbersTimeFormatBuffer;

    public static string GetIntToStringHash(int num)
    {
        if (_numbersBuffer == null) InitList();
        if (num >= _numbersBuffer.Count) ResizeBuffer(num);

        return _numbersBuffer[num];
    }

    public static string GetIntToStringTimeHash(int num)
    {
        if (_numbersBuffer == null) InitList();
        if (num >= _numbersBuffer.Count) ResizeBuffer(num);

        return _numbersTimeFormatBuffer[num];
    }

    private static void InitList()
    {
        _numbersBuffer = new List<string>(START_CAPACITY);
        _numbersTimeFormatBuffer = new List<string>(START_CAPACITY);
        ResizeBuffer(START_CAPACITY);
    }
    private static void ResizeBuffer(int newCapacity)
    {
        for (int i = _numbersBuffer.Count; i < newCapacity; i++)
        {
            _numbersBuffer.Add(i.ToString());
            _numbersTimeFormatBuffer.Add(string.Format("{0:D2}", i));
        }
    }
}
