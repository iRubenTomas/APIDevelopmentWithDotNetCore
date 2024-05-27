using MediatR;

namespace CarInventory.Application.Queries.External
{
    public class ImportCarsQuery : IRequest<string>
    {
        public string RequestUrl { get; set; }

        public ImportCarsQuery(string requestUrl)
        {
            RequestUrl = requestUrl;
        }
    }
}
