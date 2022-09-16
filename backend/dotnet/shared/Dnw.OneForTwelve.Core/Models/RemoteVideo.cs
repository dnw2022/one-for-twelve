using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Core.Models;

[UsedImplicitly]
public record RemoteVideo(string VideoId, int StartAt, int EndAt, RemoteVideoSources Source);