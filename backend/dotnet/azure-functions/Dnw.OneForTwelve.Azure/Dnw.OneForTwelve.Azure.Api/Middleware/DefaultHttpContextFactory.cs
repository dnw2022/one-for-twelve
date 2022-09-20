using Microsoft.AspNetCore.Http;

namespace Dnw.OneForTwelve.Azure_Api.Middleware;

public interface IDefaultHttpContextFactory
{
    DefaultHttpContext Create();
}

public class DefaultHttpContextFactory : IDefaultHttpContextFactory
{
    public DefaultHttpContext Create()
    {
        return new DefaultHttpContext();
    }
}