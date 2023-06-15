using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class History
{

	public static List<MessageData> list = new List<MessageData>();


	public static void PrintLog()
	{
		foreach (var data in list)
		{
			Debug.Log(data.ToString());
		}
	}
	
}
