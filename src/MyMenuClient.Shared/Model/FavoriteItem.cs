
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyMenu
{
	[DataContract (Name = "favorite")]
	public class FavoriteItem : BaseModel
	{
		
		[JsonProperty ("foodItemId")]
		public string FoodItemId {
			get;
			set;
		}

		[JsonProperty ("userId")]
		public string UserId {
			get;
			set;
		}

		[JsonProperty("isremoved")]
		public bool IsRemoved{
			get;set;
		}
	}
}

