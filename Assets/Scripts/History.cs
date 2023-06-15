using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class History
{


	public class Data
	{
		public Data(string author, string message)
		{
			this.author = author;
			this.message = message;
		}

		public string author;
		public string message;

		override
		public string ToString()
		{
			return author +"Åu"+ message +"Åv";
		}
	}

	public static List<Data> list = new List<Data>();


	public static void PrintLog()
	{
		foreach (var data in list)
		{
			Debug.Log(data.ToString());
		}
	}
	
}
