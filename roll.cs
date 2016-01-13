// roll
// 
// Copyright (c) 2016 Robin Southern -- github.com/r57s/roll
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using UnityEngine;

public class Roll
{
    public static Roll d6 = new Roll("d6");
    public static Roll d10 = new Roll("d10");
    public static Roll d20 = new Roll("d10");
    public static Roll d100 = new Roll("d100");
    
    public int Count { get; private set; }
    public int Face { get; private set; }
    public int Add { get; private set; }
    
    public Roll(string m)
    {
        int it = 0;
        SkipWhiteSpace(m, ref it);
        
        // Check for Count
        if (char.IsNumber(m[it]))
        {
            Count = ReadPositiveInteger(m, ref it);
        }
        else
        {
            Count = 1;
        }
        
        if (SkipConstant(m, 'd', ref it) == false)
        {
            return;
        }
        
        SkipWhiteSpace(m, ref it);
        
        if (char.IsNumber(m[it]))
        {
            Face = ReadPositiveInteger(m, ref it);
        }
        else
        {
            return;
        }
        
        if (SkipConstant(m, '+', ref it))
        {
            SkipWhiteSpace(m, ref it);
            if (char.IsNumber(m[it]))
            {
                Add = ReadPositiveInteger(m, ref it);
            }
        }
    }
    
    public Roll(int count, int face, int add)
    {
        Count = count;
        Face = face;
        Add = add;   
    }
    
    public int Dice()
    {
        int roll = Add;
       
        for (var i = 0; i < Count; i++)
            roll += Random(1, Face);
        
        return roll;
    }
    
    protected virtual int Random(int min, int max)
    {
        return Mathf.RoundToInt(min + UnityEngine.Random.value * (max - min));
    }
     
     
    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(10);
        if (Count != 1)
            sb.Append(Count);
        sb.Append('d');
        sb.Append(Face);
        if (Add != 0)
            sb.AppendFormat("{0:+0}", Add);
        return sb.ToString();
    }
    
    private static void SkipWhiteSpace(string str, ref int it)
    {
        for(;it < str.Length;it++)
        {
            if (char.IsWhiteSpace(str[it]) == false)
                break;
        }
    }
    
    public static bool SkipConstant(string str, char c, ref int it)
    {
        if (it >= str.Length)
            return false;
        
        SkipWhiteSpace(str, ref it);
        
        if (str[it] == c)
        {
            it++;
            return true;
        }
        return false;
    }
    
    private static int ReadPositiveInteger(string str, ref int it)
    {
        int n = 0;
        for(;it < str.Length;++it)
        {
            char ch = str[it];
            if (char.IsDigit(ch) == false)
                return n;
            int v = (ch - '0');
            
            n = (n * 10) + v; 
        }
        return n;
    }
    
    #region Tests
    
    public static void Test()
    {
        bool x = Test("d6");
        x |= Test("d60");
        x |= Test("2d6");
        x |= Test("20d60");
        x |= Test("d6+1");
        x |= Test("d60+10");
        x |= Test("2d6+1");
        x |= Test("    20       d        60       +           10              ");
        if (!x)
            Debug.Log("Failed");
        else
            Debug.Log("Passed");
    }
    
    private static bool Test(string m)
    {
        var r = new Roll(m);
        if (r.ToString() != m.Replace(" ", ""))
        {
            Debug.LogErrorFormat("Failed: '{0}' => '{1}'", m, r.ToString());
            return false;
        }
        return true;
    }
    #endregion
}