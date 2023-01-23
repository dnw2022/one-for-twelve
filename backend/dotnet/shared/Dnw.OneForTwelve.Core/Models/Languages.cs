using System.Text.Json.Serialization;

namespace Dnw.OneForTwelve.Core.Models;

[JsonConverter(typeof(EnumStringJsonConverter<Languages>))]
public enum Languages {
  English,
  Dutch
}