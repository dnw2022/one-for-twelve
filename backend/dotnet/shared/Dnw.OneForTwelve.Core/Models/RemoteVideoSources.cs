using System.Text.Json.Serialization;

namespace Dnw.OneForTwelve.Core.Models;

[JsonConverter(typeof(EnumStringJsonConverter<RemoteVideoSources>))]
public enum RemoteVideoSources {
  // ReSharper disable once UnusedMember.Global
  Youtube,
  Vimeo
}