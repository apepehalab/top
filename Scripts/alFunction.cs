using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alFunction : MonoBehaviour
{
	public static ArrayList parsing_string(string str, char chr)
	{
		ArrayList list = new ArrayList();
		string[] s = str.Split(chr);
		foreach (var element in s)
		{
			list.Add(element.Trim());
		}
		return list;
	}
}
