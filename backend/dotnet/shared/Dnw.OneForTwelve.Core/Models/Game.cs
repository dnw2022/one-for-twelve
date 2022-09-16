using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Core.Models;

[UsedImplicitly] 
public record Game(string Word, IEnumerable<GameQuestion> Questions);