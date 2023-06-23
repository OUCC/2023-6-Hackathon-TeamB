using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class History
{

	public static List<MessageData> list = new List<MessageData>();


	public static void PrintLog()
	{
		string str = "";
		foreach (var data in list)
		{
			str+=data.ToString()+"\n";
		}
		Debug.Log(str);
	}
	
}
