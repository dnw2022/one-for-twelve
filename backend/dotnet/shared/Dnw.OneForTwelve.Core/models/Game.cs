using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Core.models;

[UsedImplicitly] 
public record Game(string Word, IEnumerable<GameQuestion> Questions);