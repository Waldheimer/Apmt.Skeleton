namespace appointmenting.AspNetCore;

public class ApiResult<TEntity>
{
    public string ResultType => typeof(TEntity).Name!;
    public bool Success { get; set; } = true;
    public string? Message { get; set; } = null!;
    public TEntity? Result { get; set; } = default!;

    public bool OK => Success;
    public string? ErrorCode { get; set; } = null!;
}
