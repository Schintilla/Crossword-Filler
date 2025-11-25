using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crossword_Filler
{
	public class CharArray2DConverter : JsonConverter<char[,]>
	{
		public override char[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException("Deserialization of char[,] is not implemented.");
		}

		public override void Write(Utf8JsonWriter writer, char[,] array, JsonSerializerOptions options)
		{
			writer.WriteStartArray(); // Start the outer array (rows)
			var rowsLastIndex = array.GetUpperBound(0);
			var columnsLastIndex = array.GetUpperBound(1);

			for (var i = array.GetLowerBound(0); i <= rowsLastIndex; i++)
			{
				writer.WriteStartArray(); // Start the inner array (columns)
				for (var j = array.GetLowerBound(1); j <= columnsLastIndex; j++)
				{
					writer.WriteStringValue(array[i, j].ToString());
				}
				writer.WriteEndArray();
			}

			writer.WriteEndArray();
		}
	}
}

