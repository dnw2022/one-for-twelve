using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Aws.Api.models;

[UsedImplicitly] 
public record Game(string Word, IEnumerable<GameQuestion> Questions);