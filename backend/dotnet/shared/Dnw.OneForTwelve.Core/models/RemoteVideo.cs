using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Core.models;

[UsedImplicitly]
public record RemoteVideo(string VideoId, int StartAt, int EndAt, RemoteVideoSources Source);