using System.Text.Json.Serialization;

namespace Dnw.OneForTwelve.Core.Models;

[JsonConverter(typeof(EnumStringJsonConverter<QuestionSelectionStrategies>))]
public enum QuestionSelectionStrategies {
  Demo,
  RandomOnlyEasy,
  Random,
}