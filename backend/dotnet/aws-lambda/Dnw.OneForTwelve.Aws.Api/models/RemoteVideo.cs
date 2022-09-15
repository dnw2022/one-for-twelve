using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Aws.Api.models;

[UsedImplicitly]
public record RemoteVideo(string VideoId, int StartAt, int EndAt, RemoteVideoSources Source);