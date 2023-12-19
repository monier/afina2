namespace Afina2.Server.WebApp.Tests.TestInfrastructure.Web
{
    public class CreatedResource<T> where T : class
    {
        public string Location { get; set; } = null!;
        public T? Resource { get; set; }
    }
}
