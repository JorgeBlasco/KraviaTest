using KraviaTest.Models;
using KraviaTest.Services.Interfaces;
using KraviaTest.Templates;

namespace KraviaTest.Handlers.Interfaces
{
    public interface IPostenFailureHandler
    {
        void HandleFailure(PostenFailureInfoModel data);

        void SendEmail(PostenFailureInfoModel data);
    }
}
