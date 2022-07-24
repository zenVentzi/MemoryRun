using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEditor.XCodeEditorChartboost
{
	public class PBXList : ArrayList
	{
		public PBXList()
		{
			
		}
		
		public PBXList( object firstValue )
		{
			this.Add( firstValue );
		}
	}
	
//	public class PBXList<T> : ArrayList
//	{
//		public int AddEnergy( T value )
//		{
//			return (ArrayList)this.AddEnergy( value );
//		}
//	}
}
