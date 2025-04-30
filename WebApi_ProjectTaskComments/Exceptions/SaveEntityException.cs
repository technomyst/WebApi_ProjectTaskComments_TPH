namespace WebApi_ProjectTaskComments.Exceptions
{
    public class SaveEntityException:Exception
    {
        public SaveEntityException(string? message = null)
            : base(message)
        {

        }
    }
}
