namespace WebApi_ProjectTaskComments.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message = null)
            : base(message)
        {

        }
    }
}
