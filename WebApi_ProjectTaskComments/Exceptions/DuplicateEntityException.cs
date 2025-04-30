namespace WebApi_ProjectTaskComments.Exceptions
{
    //C#12.0
    //public class DuplicateEntityException(string? message = null) : Exception(message)
    //{
    //}

    public class DuplicateEntityException:Exception
    {
        public  DuplicateEntityException(string? message = null)
            : base (message)
        {

        }
    }
}
