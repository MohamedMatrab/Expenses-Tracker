namespace Expenses.Domain.Shared;

public record Result
{
    public bool IsSuccess { get; }
    public IEnumerable<Error> Errors { get; }
    public object? Response { get; }
    private Result(bool isSuccess,List<Error> errors,object? response)
    {
        if ((isSuccess && errors.Any(e => e!=Error.None)) 
            || (!isSuccess && errors.All(e => e==Error.None)))
        {
            throw new ArgumentException("Invalid Parameter Error",nameof(errors));
        }
        IsSuccess = isSuccess;
        Errors = errors.Where(e=>e!=Error.None);
        Response = response;
    }
    
    #region Additions
    public static implicit operator Result(Error error)
    {
        return error == Error.None ? Success() : Failure(error);
    }
    
    public static implicit operator Result(List<Error> errors)
    {
        return errors.Any(e=>e!=Error.None) ? Failure(errors) : Success();
    }
    
    public static Result Failure(List<Error> errors) => new(false,errors,null);
    public static Result Failure(Error error) => new(false,[error],null);
    public static Result Success(object? response=null) => new(true,[],response);

    #endregion
}