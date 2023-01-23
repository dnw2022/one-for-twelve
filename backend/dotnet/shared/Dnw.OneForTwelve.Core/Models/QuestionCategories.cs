using System.Text.Json.Serialization;

namespace Dnw.OneForTwelve.Core.Models;

[JsonConverter(typeof(EnumStringJsonConverter<QuestionCategories>))]
public enum QuestionCategories {
  Unknown,
  Geography,
  Bible,
  Biology,
  Cryptic,
  Economy,
  History,
  Art,
  Literature,
  Music,
  Politics,
  Sports,
  ScienceOrMaths
}