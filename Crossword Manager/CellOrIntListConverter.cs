using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crossword_Filler
{
	public class CellOrIntListConverter : JsonConverter<List<List<object>>>
	{
		public override List<List<object>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
			{
				throw new JsonException("Expected StartArray token for puzzle.");
			}

			var result = new List<List<object>>();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				if (reader.TokenType != JsonTokenType.StartArray)
				{
					throw new JsonException("Expected StartArray token for inner list.");
				}

				var innerList = new List<object>();
				while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
				{
					if (reader.TokenType == JsonTokenType.Number)
					{
						innerList.Add(reader.GetInt32());
					}
					else if (reader.TokenType == JsonTokenType.StartObject)
					{
						// Use a local options instance to avoid infinite recursion
						var localOptions = new JsonSerializerOptions(options);
						var cell = (Cell)JsonSerializer.Deserialize(ref reader, typeof(Cell), localOptions);
						innerList.Add(cell);
					}
					else if (reader.TokenType == JsonTokenType.String)
					{
						innerList.Add(reader.GetString());
					}
					else if (reader.TokenType == JsonTokenType.Null)
					{
						innerList.Add(null);
					}
					else
					{
						throw new JsonException($"Unexpected token type: {reader.TokenType}");
					}
				}
				result.Add(innerList);
			}
			return result;
		}

		public override void Write(Utf8JsonWriter writer, List<List<object>> value, JsonSerializerOptions options)
		{
			// For now, only reading is implemented.
			// throw new NotImplementedException();
		}
	}
}
