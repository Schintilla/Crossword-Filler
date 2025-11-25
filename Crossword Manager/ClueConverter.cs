using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crossword_Filler
{
	public class ClueConverter : JsonConverter<Clue>
	{
		public override Clue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
			{
				throw new JsonException("Expected StartArray for Clue array.");
			}

			var clue = new Clue();

			// Read the label (number)
			reader.Read();
			if (reader.TokenType == JsonTokenType.Number)
			{
				clue.Number = reader.GetInt32().ToString();
			}
			else if (reader.TokenType == JsonTokenType.String)
			{
				clue.Number = reader.GetString();
			}
			else
			{
				throw new JsonException("Expected number or string for Clue label.");
			}

			// Read the text
			reader.Read();
			if (reader.TokenType == JsonTokenType.String)
			{
				clue.Text = reader.GetString();
			}
			else
			{
				throw new JsonException("Expected string for Clue text.");
			}

			// Read the EndArray token
			reader.Read();
			if (reader.TokenType != JsonTokenType.EndArray)
			{
				throw new JsonException("Expected EndArray for Clue array.");
			}

			return clue;
		}

		public override void Write(Utf8JsonWriter writer, Clue value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			writer.WriteStringValue(value.Number);
			writer.WriteStringValue(value.Text);
			writer.WriteEndArray();
		}
	}
}

