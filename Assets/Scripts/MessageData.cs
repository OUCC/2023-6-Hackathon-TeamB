using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageData
{

	public MessageData(string author, string message)
	{
		this.author = author;
		this.message = message;
	}

	public string author;
	public string message;

	override
	public string ToString()
	{
		if (author.Length > 0)
			return author + "F" + message;
		else
			//’n‚Ì•¶
			return message;
	}
}
