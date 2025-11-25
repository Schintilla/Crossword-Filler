// File: CellOrStringConverter.cs
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crossword_Filler
{
	public class CellOrStringConverter : JsonConverter<Cell>
	{
		public override Cell Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				return new Cell { Value = reader.GetString() };
			}

			if (reader.TokenType == JsonTokenType.StartObject)
			{
				return (Cell)JsonSerializer.Deserialize(ref reader, typeof(Cell), options);
			}

			throw new JsonException("Expected a string or an object for cell.");
		}

		public override void Write(Utf8JsonWriter writer, Cell value, JsonSerializerOptions options)
		{
			// Implementation for serialization if needed
			throw new NotImplementedException();
		}
	}
}

