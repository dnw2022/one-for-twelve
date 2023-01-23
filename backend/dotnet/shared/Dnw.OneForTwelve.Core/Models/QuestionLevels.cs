using System.Text.Json.Serialization;

namespace Dnw.OneForTwelve.Core.Models;

[JsonConverter(typeof(EnumStringJsonConverter<QuestionLevels>))]
public enum QuestionLevels {
  Easy,
  Normal, 
  Hard
}