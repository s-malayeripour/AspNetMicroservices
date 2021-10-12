namespace OrderingApplication.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, string key) :
            base($"Entity \"{name}\" ({key}) not found.") { }
    }
}
